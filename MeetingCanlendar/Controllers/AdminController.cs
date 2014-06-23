using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingCanlendar.Models;

namespace MeetingCanlendar.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            ViewBag.UsersCount = UserModel.GetUsersCount();
            ViewBag.MeetingsCount = MeetingModel.GetMeetintgCount();
            return View();
        }

        public ActionResult UserList()
        {
            return View();
        }
    }
}
