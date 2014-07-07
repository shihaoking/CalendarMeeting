using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingCanlendar.Models;
using MeetingCanlendar.Tools;
using DBEntity;
using System.Web.Script.Serialization;

namespace MeetingCanlendar.Controllers
{
    [MCAuthorize(Roles="8,9")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return RedirectToAction("UserList");
        }

        public ActionResult UserList(int p = 1)
        {
            UserModel userModel = new UserModel();
            IQueryable<user_info_detail> userInfo = userModel.GetUserInfos();
            ViewBag.UsersCount = userInfo.Count();

            userInfo = userInfo.OrderBy(r => r.ui_name);
            int pageSize = 5;
            Paging.ToPaging(p, ViewBag.UsersCount, this, pageSize);
            userInfo = userInfo.Skip((p - 1) * pageSize).Take(pageSize);
            return View(userInfo);
        }

        public ActionResult MeetingList(string startTime, string endTime, int p = 1)
        {
            MeetingModel meetingModel = new MeetingModel();
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);

            if(!string.IsNullOrEmpty(startTime))
            {
                if(DateTime.TryParse(startTime, out startDate) == false)
                {
                    startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                }
            }

            if(!string.IsNullOrEmpty(endTime))
            {
                if(DateTime.TryParse(endTime, out endDate) == false)
                {
                    endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                }
            }

            ViewBag.StartDateFilter = startDate.ToString("yyyy-MM-dd");
            ViewBag.EndDateFilter = endDate.ToString("yyyy-MM-dd");

            IQueryable<meeting_info_detail> meetings = meetingModel.GetMeetingsDetail(startDate, endDate);
            ViewBag.MeetingsCount = meetings.Count();

            var creators = meetings.GroupBy(r => new { r.mi_creator, r.mi_creator_name }).Select(r => new { r.Key.mi_creator, r.Key.mi_creator_name });
            ViewBag.CreatorSelectList = new SelectList(creators, "mi_creator", "mi_creator_name");

            meetings = meetings.OrderByDescending(r => r.mi_start_time);
            int pageSize = 10;
            Paging.ToPaging(p, ViewBag.MeetingsCount, this, pageSize);
            meetings = meetings.Skip((p - 1) * pageSize).Take(pageSize);
            return View(meetings);
        }

        public ActionResult ChangeUserStatus(string ids, short status)
        {
            JavaScriptSerializer jser = new JavaScriptSerializer();
            string[] result = jser.Deserialize<string[]>(ids);
            UserModel userModel = new UserModel();

            int calResult = 0;
            try
            {
                calResult = userModel.UpdateUserStatus(string.Join(",", result), status);
            }
            catch(Exception ex)
            {
                return Json(new { type = 0, msg = "更改失败：" + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { type = 1, msg = "更改成功" },JsonRequestBehavior.AllowGet);
        }
    }
}
