using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingCanlendar.Models
{
    public class MeetingResultModel
    {
        public int type { get; set; }
        public string msg { get; set; }

        public MeetingResultDataModel data { get; set; }
    }
}