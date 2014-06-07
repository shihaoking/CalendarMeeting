using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingCanlendar.Models;

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
        public ActionResult GetMeetingsByMonth(int year, int month)
        {
            MeetingModel metModel = new MeetingModel();
            DateTime startTime = new DateTime(year, month, 1);
            DateTime endTime = startTime.AddMonths(1).AddDays(-1);

            return Json(metModel.GetMeetings(startTime, endTime).ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
