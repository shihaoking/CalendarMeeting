using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBEntity;

namespace MeetingCanlendar.Models
{
    public class MeetingModel : MySqlDBModel
    {
        public IQueryable<meeting_info> GetMeetings()
        {
            return db.meeting_info;
        }

        public meeting_info GetMeeting(int id)
        {
            return db.meeting_info.FirstOrDefault(r => r.id == id);
        }

        public IQueryable<meeting_info> GetMeetings(DateTime fromTime, DateTime toTime)
        {
            return db.meeting_info.Where(r => r.m_start_time >= fromTime && r.m_start_time <= toTime);
        }

        public IQueryable<meeting_position> GetMeetingPositions()
        {
            return db.meeting_position;
        }

        public void AddMeeting(meeting_info mi)
        {
            db.meeting_info.AddObject(mi);
            SaveChange();
        }
    }

    public class MeetingEvent
    {
        public int id { get; set; }
        public string title { get; set; }
        public int position { get; set; }
        public DateTime start { get; set; }
    }
}