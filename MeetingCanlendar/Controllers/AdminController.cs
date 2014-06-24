using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingCanlendar.Models;
using MeetingCanlendar.Tools;
using DBEntity;

namespace MeetingCanlendar.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            ViewBag.MeetingsCount = MeetingModel.GetMeetintgCount();
            return View();
        }

        public ActionResult UserList(int p = 1)
        {
            UserModel userModel = new UserModel();
            IQueryable<user_info_detail> userInfo = userModel.GetAvaliableUserInfos();
            ViewBag.UsersCount = userInfo.Count();

            userInfo = userInfo.OrderBy(r => r.ui_name);
            int pageSize = 50;
            Paging.ToPaging(p, ViewBag.UsersCount, this, pageSize);
            userInfo = userInfo.Skip((p - 1) * pageSize).Take(pageSize);
            return View(userInfo);
        }
    }
}
