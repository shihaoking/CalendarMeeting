using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DBEntity;
using MeetingCanlendar.Models;

namespace MeetingCanlendar
{
    public class MeetingHub : Hub
    {
        public void AddMeeting(string userName, meeting_info metData)
        {
            MeetingModel metModel = new MeetingModel();
            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(userName);
            object result;
            
            if (metModel.CheckMeetingAvailable(metData.id, metData.mi_start_time, metData.mi_end_time) == false)
            {
                result = new { type = 0, msg = "该时间段内已经有会议，请更改会议时间。" };
                Clients.Caller.broadcastMeetingEdit(result);
                return;
            }

            meeting_info metInfo;
            int editType = 1;
            if (metData.id == -1)
            {
                metInfo = new meeting_info();
                metInfo.mi_create_time = DateTime.Now;
                metInfo.mi_creator = userInfo.id;
            }
            else
            {
                editType = 2;
                metInfo = metModel.GetMeeting(metData.id);

                if (metInfo.mi_creator != userInfo.id && metInfo.meeting_level_catg.ml_level != 9)
                {
                    result = new { type = 0, msg = "该会议不是您创建的，无法修改。" };
                    Clients.Caller.broadcastMeetingEdit(result);
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
            
            try
            {
                if (metData.id == -1)
                {
                    metModel.AddMeeting(metInfo);
                }
                else
                {
                    metModel.SaveChange();
                }
            }
            catch (Exception ex)
            {
                result = new { type = 0, msg = "添加失败，请联系管理员。\n错误信息：" + ex.Message };
                Clients.Caller.broadcastMeetingEdit(result);
                return;
            }

            
            result = new
            {
                type = editType,
                msg = "添加成功",
                data = new
                {
                    id = metInfo.id,
                    title = metInfo.mi_title,
                    start = metInfo.mi_start_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = metInfo.mi_end_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                    people = metInfo.mi_people,
                    memo = metInfo.mi_memo,
                    position = metInfo.mi_position_id,
                    positionName = metInfo.meeting_positionReference.Value.mp_name,
                    creator = metInfo.user_infoReference.Value.ui_name,
                    level = metInfo.mi_level_id,
                    levelName = metInfo.meeting_level_catgReference.Value.ml_name,
                    createTime = metInfo.mi_create_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                    className = metInfo.mi_creator == userInfo.id ? "fc-event-mine" : "",
                    editable = metInfo.mi_creator == userInfo.id || userInfo.user_grade_catg.gc_level == 9 ? 1 : 0,
                    isMine = metInfo.mi_creator == userInfo.id ? 1 : 0
                }
            };

            Clients.All.broadcastMeetingEdit(result);
        }

        public void DeleteMeeting(string userName, int id)
        {
            MeetingModel metModel = new MeetingModel();
            meeting_info metInfo = metModel.GetMeeting(id);

            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(userName);

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