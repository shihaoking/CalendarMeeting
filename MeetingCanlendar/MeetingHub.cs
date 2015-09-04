using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DBEntity;
using MeetingCanlendar.Models;
using System.Threading.Tasks;

namespace MeetingCanlendar
{
    public class MeetingHub : Hub
    {
        public void AddMeeting(int userId, meeting_info metData)
        {
            MeetingModel metModel = new MeetingModel();
            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(userId);

            if (metModel.CheckMeetingAvailable(metData.id, metData.mi_start_time, metData.mi_end_time) == false)
            {
                Clients.Caller.broadcastMeetingEdit(new { type = 0, msg = "该时间段内已经有会议，请更改会议时间。", data = new { id = metData.id } });
                return;
            }

            meeting_info metInfo;
            int editType = 1;
            if (metData.id == -1)
            {
                metInfo = new meeting_info();
                metInfo.mi_status = true;
                metInfo.mi_create_time = DateTime.Now;
                metInfo.mi_creator = userInfo.id;
            }
            else
            {
                editType = 2;
                metInfo = metModel.GetMeeting(metData.id);

                if (metInfo.mi_creator != userInfo.id && metInfo.meeting_level_catg.ml_level != 9)
                {
                    Clients.Caller.broadcastMeetingEdit(new { type = 0, msg = "该会议不是您创建的，无法修改。", data = new { id = metData.id } });
                    return;
                }

                if (metModel.DeleteMeetingPeopleByMeetingId(metData.id) < 0)
                {
                    Clients.Caller.broadcastMeetingEdit(new { type = 0, msg = "修改会议失败，无法删除与会人员。", data = new { id = metData.id } });
                    return;
                }
            }

            metInfo.mi_start_time = metData.mi_start_time;
            metInfo.mi_end_time = metData.mi_end_time;
            metInfo.mi_level_id = metData.mi_level_id;
            metInfo.mi_people = metData.mi_people;
            metInfo.mi_position_id = metData.mi_position_id;
            metInfo.mi_title = metData.mi_title;
            metInfo.mi_memo = metData.mi_memo;

            string[] people = metData.mi_people.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> userIds = new List<int>();
            foreach (string pep in people)
            {
                userIds.Add(int.Parse(pep));
            }
            var userInfos = userModel.GetUserInfos().Where(r => userIds.Contains(r.id)).Select(r => new { userId = r.id, userName = r.ui_name });
            metInfo.mi_people_name = string.Join(", ", userInfos.Select(r => r.userName).ToArray());

            try
            {

                if (metData.id == -1)
                {
                    metModel.AddMeeting(metInfo);
                    metModel.SaveChange();
                }

                foreach (short pep in userIds)
                {
                    meeting_people mp = new meeting_people();
                    mp.mp_meeting_id = metInfo.id;
                    mp.mp_user_id = pep;
                    metModel.AddMeetingPeople(mp);
                }

                if (metModel.SaveChange() < 0)
                {
                    Clients.Caller.broadcastMeetingEdit(new { type = 0, msg = "修改会议失败。", data = new { id = metData.id } });
                    return;
                }

            }
            catch (Exception ex)
            {
                Clients.Caller.broadcastMeetingEdit(new { type = 0, msg = "添加失败，请联系管理员。\n错误信息：" + ex.Message, data = new { id = metData.id } });
                return;
            }


            MeetingResultModel result = new MeetingResultModel()
            {
                type = editType,
                msg = "添加成功",
                data = new MeetingResultDataModel()
                {
                    id = metInfo.id,
                    title = metInfo.mi_title,
                    start = metInfo.mi_start_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = metInfo.mi_end_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                    people = metInfo.mi_people,
                    peopleName = metInfo.mi_people_name,
                    memo = metInfo.mi_memo,
                    position = metInfo.mi_position_id,
                    positionName = metInfo.meeting_positionReference.Value.mp_name,
                    creator = metInfo.mi_creator,
                    creatorName = metInfo.user_infoReference.Value.ui_name,
                    level = metInfo.mi_level_id,
                    levelName = metInfo.meeting_level_catgReference.Value.ml_name,
                    createTime = metInfo.mi_create_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                    className = "fc-event-mine",
                    editable = 1,
                    isMine = 1
                }
            };

            Clients.Caller.broadcastMeetingEdit(result);

            result.data.className = "";
            result.data.isMine = 0;
            result.data.editable = 0;

            Clients.Others.broadcastMeetingEdit(result);
        }

        public void DeleteMeeting(int userId, int id)
        {
            MeetingModel metModel = new MeetingModel();
            meeting_info metInfo = metModel.GetMeeting(id);

            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(userId);

            if (metInfo.mi_creator != userInfo.id && userInfo.user_grade_catg.gc_level != 9)
            {
                Clients.Caller.broadcastMeetingDelete(new { type = 0, msg = "该会议不是您创建的，无法删除。" });
            }

            try
            {
                metModel.DeleteMeeting(id);
            }
            catch (Exception ex)
            {
                Clients.Caller.broadcastMeetingDelete(new { type = 0, msg = "删除失败：" + ex.Message });
            }

            Clients.All.broadcastMeetingDelete(new { type = 1, data = new { id = id }, msg = "删除成功" });
        }
    }
}