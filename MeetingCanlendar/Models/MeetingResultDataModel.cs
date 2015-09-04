using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingCanlendar.Models
{
    public class MeetingResultDataModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string people { get; set; }
        public string peopleName { get; set; }
        public string memo { get; set; }
        public int position { get; set; }
        public string positionName { get; set; }
        public int creator { get; set; }
        public string creatorName { get; set; }
        public int level { get; set; }
        public string levelName { get; set; }
        public string createTime { get; set; }
        public string className { get; set; }
        public int editable { get; set; }
        public int isMine { get; set; }
    }
}