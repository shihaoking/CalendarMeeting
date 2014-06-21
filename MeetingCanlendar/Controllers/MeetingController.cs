using System;
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

        public ActionResult GetMeetingsByMonth(int year, int month)
        {
            MeetingModel metModel = new MeetingModel();
            DateTime startTime = new DateTime(year, month, 1);
            DateTime endTime = startTime.AddMonths(1).AddDays(-1);

            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(User.Identity.Name);

            List<meeting_info> source = metModel.GetMeetings(startTime, endTime).ToList();

            var result = source.OrderBy(r => r.mi_start_time).Select(r => new
            {
                id = r.id,
                title = r.mi_title,
                start = r.mi_start_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = r.mi_end_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                people = r.mi_people,
                memo = r.mi_memo,
                position = r.mi_position,
                creator = r.user_infoReference.Value.ui_name,
                level = r.mi_level,
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
                return Json(new { type = 0, msg = "该时间段内已经有会议，请更改会议时间。" }, JsonRequestBehavior.AllowGet);
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

                if(metInfo.mi_creator != userInfo.id)
                {
                    return Json(new { type = 0, msg = "该会议不是您创建的，无法修改。" }, JsonRequestBehavior.AllowGet);
                }
            }

            metInfo.mi_start_time = metData.mi_start_time;
            metInfo.mi_end_time = metData.mi_end_time;
            metInfo.mi_level = metData.mi_level;
            metInfo.mi_people = metData.mi_people;
            metInfo.mi_position = metData.mi_position;
            metInfo.mi_title = metData.mi_title;
            metInfo.mi_memo = metData.mi_memo;

            try
            {
                if(metData.id == -1)
                {
                    metModel.AddMeeting(metInfo);
                }
                else
                {
                    metModel.SaveChange();
                }
            }
            catch(Exception ex)
            {
                return Json(new { type = 0, msg = "添加失败，请联系管理员。\n错误信息：" + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { type = 1, msg = "添加成功", 
                data = new {
                    id = metInfo.id,
                    title = metInfo.mi_title,
                    start = metInfo.mi_start_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = metInfo.mi_end_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                    people = metInfo.mi_people,
                    memo = metInfo.mi_memo,
                    position = metInfo.mi_position,
                    creator = metInfo.user_infoReference.Value.ui_name,
                    level = metInfo.mi_level,
                    createTime = metInfo.mi_create_time
                } }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMeeting(int id)
        {
            MeetingModel metModel = new MeetingModel();
            meeting_info metInfo = metModel.GetMeeting(id);
            
            UserModel userModel = new UserModel();
            user_info userInfo = userModel.GetUserInfo(User.Identity.Name);

            if(metInfo.mi_creator != userInfo.id)
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
