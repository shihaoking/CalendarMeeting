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
            ViewBag.MeetingsCount = MeetingModel.GetMeetintgCount();
            return View();
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
