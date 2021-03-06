﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingCanlendar.Models;
using DBEntity;
using System.Web.Script.Serialization;

namespace MeetingCanlendar.Controllers
{
    [MCAuthorize]
    public class MeetingController : Controller
    {
        //
        // GET: /Meeting/

        public ActionResult Index()
        {
            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(User.Identity.Name);
            ViewBag.UserId = userInfo.id;
            ViewBag.UserLevel = userInfo.user_grade_catg.gc_level;

            return View();
        }

        #region AJAX
        public ActionResult GetMeetingPositions()
        {
            MeetingModel metModel = new MeetingModel();
            List<meeting_position> source = metModel.GetMeetingPositions().ToList();
            var result = source.Select(r => new { value = r.id, text = r.mp_name});

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMeetingsByIds(string ids)
        {
            JavaScriptSerializer jser = new JavaScriptSerializer();
            MeetingModel metModel = new MeetingModel();
            int[] idList = jser.Deserialize<int[]>(ids);

            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(User.Identity.Name);

            List<meeting_info_detail> source = metModel.GetMeetingsDetail(idList).ToList();

            var result = source.OrderBy(r => r.mi_start_time).Select(r => new
            {
                id = r.id,
                title = r.mi_title,
                start = r.mi_start_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = r.mi_end_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                people = r.mi_people,
                memo = r.mi_memo,
                position = r.mi_position_id,
                creator = r.mi_creator_name,
                level = r.mi_level_id,
                createTime = r.mi_create_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                className = r.mi_creator == userInfo.id ? "fc-event-mine" : "",
                editable = r.mi_creator == userInfo.id || userInfo.user_grade_catg.gc_level == 9 ? 1 : 0,
                isMine = r.mi_creator == userInfo.id ? 1 : 0
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMeetingsByMonths(string months)
        {
            JavaScriptSerializer jser = new JavaScriptSerializer();
            string[] monthList = jser.Deserialize<string[]>(months);
            MeetingModel metModel = new MeetingModel();

            List<DateTime> dataMonths = new List<DateTime>();
            foreach(string monthStr in monthList)
            {
                dataMonths.Add(DateTime.Parse(monthStr));
            }

            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(User.Identity.Name);

            List<meeting_info_detail> source = new List<meeting_info_detail>();
            try
            {
                source = metModel.GetMeetings(dataMonths.ToArray()).ToList();
            }
            catch (Exception ex)
            {

            }

            var result = source.OrderBy(r => r.mi_start_time).Select(r => new
            {
                id = r.id,
                title = r.mi_title,
                start = r.mi_start_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = r.mi_end_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                people = r.mi_people,
                memo = r.mi_memo,
                position = r.mi_position_id,
                creator = r.mi_creator,
                creatorName = r.mi_creator_name,
                level = r.mi_level_id,
                createTime = r.mi_create_time,
                className = r.mi_creator == userInfo.id ? "fc-event-mine" : "",
                editable = r.mi_creator == userInfo.id || userInfo.user_grade_catg.gc_level == 9 ? 1 : 0,
                isMine = r.mi_creator == userInfo.id ? 1 : 0
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditMeeting(string source)
        {
            MeetingModel metModel = new MeetingModel();
            JavaScriptSerializer jser = new JavaScriptSerializer();
            meeting_info metData = jser.Deserialize<meeting_info>(source);

            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(User.Identity.Name);

            if(metModel.CheckMeetingAvailable(metData.id, metData.mi_start_time, metData.mi_end_time) == false)
            {
                return Json(new { type = 0, msg = "该时间段内已经有会议，请更改会议时间。", data = new { id = metData.id } }, JsonRequestBehavior.AllowGet);
            }

            meeting_info metInfo;
            if(metData.id == -1)
            {
                metInfo = new meeting_info();
                metInfo.mi_create_time = DateTime.Now;
                metInfo.mi_creator = userInfo.id;
            }
            else
            {
                metInfo = metModel.GetMeeting(metData.id);

                if(metInfo.mi_creator != userInfo.id && metInfo.meeting_level_catg.ml_level != 9)
                {
                    return Json(new { type = 0, msg = "该会议不是您创建的，无法修改。", data = new { id = metData.id } }, JsonRequestBehavior.AllowGet);
                }

                if (metModel.DeleteMeetingPeopleByMeetingId(metData.id) < 0)
                {
                    return Json(new { type = 0, msg = "修改会议失败，无法删除与会人员。", data = new { id = metData.id } });
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

                }
                foreach (short pep in userIds)
                {
                    meeting_people mp = new meeting_people();
                    mp.mp_meeting_id = metInfo.id;
                    mp.mp_user_id = pep;
                    metModel.AddMeetingPeople(mp);
                }

                metModel.SaveChange();

            }
            catch (Exception ex)
            {
                return Json(new { type = 0, msg = "添加失败，请联系管理员。\n错误信息：" + ex.Message, data = new { id = metData.id } }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { type = 1, msg = "添加成功", 
                data = new {
                    id = metInfo.id,
                    title = metInfo.mi_title,
                    start = metInfo.mi_start_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = metInfo.mi_end_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                    people = metInfo.mi_people,
                    peopleName = metInfo.mi_people_name,
                    memo = metInfo.mi_memo,
                    position = metInfo.mi_position_id,
                    positionName = metInfo.meeting_positionReference.Value.mp_name,
                    creator = metInfo.user_info.ui_name,
                    level = metInfo.mi_level_id,
                    levelName = metInfo.meeting_level_catgReference.Value.ml_name,
                    createTime = metInfo.mi_create_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                    className = metInfo.mi_creator == userInfo.id ? "fc-event-mine" : "",
                    editable = metInfo.mi_creator == userInfo.id || userInfo.user_grade_catg.gc_level == 9 ? 1 : 0,
                    isMine = metInfo.mi_creator == userInfo.id ? 1 : 0
                } }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeMeetingStatus(string ids, short status)
        {
            JavaScriptSerializer jser = new JavaScriptSerializer();
            string[] result = jser.Deserialize<string[]>(ids);
            MeetingModel meetingModel = new MeetingModel();

            int calResult = 0;
            try
            {
                calResult = meetingModel.UpdateMeetingStatus(string.Join(",", result), status);
            }
            catch(Exception ex)
            {
                return Json(new { type = 0, msg = "更改失败：" + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { type = 1, msg = "更改成功" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMeeting(int id)
        {
            MeetingModel metModel = new MeetingModel();
            meeting_info metInfo = metModel.GetMeeting(id);
            
            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(User.Identity.Name);

            if(metInfo.mi_creator != userInfo.id && userInfo.user_grade_catg.gc_level != 9)
            {
                return Json(new { type = 0, msg = "该会议不是您创建的，无法删除。" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                metModel.DeleteMeeting(id);
            }
            catch (Exception ex)
            {
                return Json(new { type = 0, msg = "删除失败：" + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { type = 1, msg = "删除成功"}, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
