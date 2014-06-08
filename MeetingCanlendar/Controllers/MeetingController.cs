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
            var result = source.Select(r => new { value = r.id, text = r.p_name});

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMeetingsByMonth(int year, int month)
        {
            MeetingModel metModel = new MeetingModel();
            DateTime startTime = new DateTime(year, month, 1);
            DateTime endTime = startTime.AddMonths(1).AddDays(-1);

            List<meeting_info> source = metModel.GetMeetings(startTime, endTime).ToList();

            var result = source.Select(r => new { 
                id = r.id, 
                title = r.m_title, 
                start = r.m_start_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = r.m_end_time.ToString("yyyy-MM-ddTHH:mm:ss"),
                people = r.m_people, 
                memo = r.m_memo, 
                position = r.m_position,
                creator = r.user_infoReference.Value.u_name, 
                level = r.m_level,
                createTime = r.m_create_time});

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditMeeting(string source)
        {
            MeetingModel metModel = new MeetingModel();
            JavaScriptSerializer jser = new JavaScriptSerializer();
            meeting_info metData = jser.Deserialize<meeting_info>(source);


            meeting_info metInfo;
            if(metData.id == -1)
            {
                metInfo = new meeting_info();
                metInfo.m_create_time = DateTime.Now;
                metInfo.m_creator = 1;
            }
            else
            {
                metInfo = metModel.GetMeeting(metData.id);
            }

            metInfo.m_start_time = metData.m_start_time;
            metInfo.m_end_time = metData.m_end_time;
            metInfo.m_level = metData.m_level;
            metInfo.m_people = metData.m_people;
            metInfo.m_position = metData.m_position;
            metInfo.m_title = metData.m_title;
            metInfo.m_memo = metData.m_memo;

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

            return Json(new { type = 1, msg = "添加成功" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
