﻿using System;
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

        public static int GetMeetintgCount()
        {

            return staticDb.meeting_info.Count();
        }

        public IQueryable<meeting_info> GetMeetings(DateTime fromTime, DateTime toTime)
        {
            return db.meeting_info.Where(r => r.mi_start_time >= fromTime && r.mi_start_time <= toTime);
        }

        public IQueryable<meeting_position> GetMeetingPositions()
        {
            return db.meeting_position;
        }

        public bool CheckMeetingAvailable(int metId, DateTime startTime, DateTime endTime)
        {
            return !(db.meeting_info.Any(r => r.id != metId &&
                (r.mi_start_time < startTime && startTime < r.mi_end_time ||
                r.mi_start_time < endTime && endTime < r.mi_end_time ||
                r.mi_start_time == startTime && r.mi_end_time == endTime)));
        }

        public void DeleteMeeting(int id)
        {
            meeting_info mi = GetMeeting(id);
            if (mi != null)
            {
                db.meeting_info.DeleteObject(mi);
                SaveChange();
            }
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