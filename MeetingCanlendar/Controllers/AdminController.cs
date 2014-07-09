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
            IQueryable<user_info_detail> userInfo = userModel.GetUserInfoDetails();
            ViewBag.UsersCount = userInfo.Count();

            userInfo = userInfo.OrderBy(r => r.ui_name);
            int pageSize = 5;
            Paging.ToPaging(p, ViewBag.UsersCount, this, pageSize);
            userInfo = userInfo.Skip((p - 1) * pageSize).Take(pageSize);
            return View(userInfo);
        }

        public ActionResult MeetingList(string startDate, string endDate, int? creator, int p = 1)
        {
            MeetingModel meetingModel = new MeetingModel();
            DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);

            if(!string.IsNullOrEmpty(startDate))
            {
                if(DateTime.TryParse(startDate, out startTime) == false)
                {
                    startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                }
            }

            if(!string.IsNullOrEmpty(endDate))
            {
                if(DateTime.TryParse(endDate, out endTime) == false)
                {
                    endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                }
            }

            ViewBag.StartDateFilter = startTime.ToString("yyyy-MM-dd");
            ViewBag.EndDateFilter = endTime.ToString("yyyy-MM-dd");
            ViewBag.CreatorFilter = creator.HasValue ? creator.Value : -1;

            IQueryable<meeting_info_detail> meetings = meetingModel.GetMeetingsDetail(startTime, endTime, creator);
            ViewBag.MeetingsCount = meetings.Count();

            UserModel userModel = new UserModel();

            var creators = userModel.GetUserInfos().GroupBy(r => new { r.id, r.ui_name }).Select(r => new { r.Key.id, r.Key.ui_name });
            ViewBag.CreatorSelectList = new SelectList(creators, "id", "ui_name");

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
