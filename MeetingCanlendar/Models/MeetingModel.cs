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

        public static int GetMeetintgCount()
        {

            return staticDb.meeting_info.Count();
        }

        public IQueryable<meeting_info_detail> GetMeetingsDetail(params int[] ids)
        {
            return db.meeting_info_detail.Where(r => ids.Contains(r.id));
        }

        public IQueryable<meeting_info_detail> GetMeetings(params DateTime[] dates)
        {
            string dateFilter = "";
            foreach(DateTime dt in dates)
            {
                dateFilter += string.Format(" Or (mi_start_time >= '{0}' And mi_start_time < '{1}')", dt.ToString("yyyy-MM-01"), dt.AddMonths(1).ToString("yyyy-MM-01"));
            }

            if(!string.IsNullOrWhiteSpace(dateFilter))
            {
               dateFilter = dateFilter.Substring(4);
            }

            return db.ExecuteStoreQuery<meeting_info_detail>("Select * From meeting_info_detail Where " + dateFilter).AsQueryable();
        }

        public IQueryable<meeting_info_detail> GetMeetingsDetail(DateTime fromTime, DateTime toTime, int? creator)
        {
            IQueryable<meeting_info_detail> result = db.meeting_info_detail.Where(r => r.mi_start_time >= fromTime && r.mi_start_time <= toTime);
            if(creator.HasValue)
            {
                result = result.Where(r => r.mi_creator == creator.Value);
            }

            return result;
        }

        public IQueryable<meeting_position> GetMeetingPositions()
        {
            return db.meeting_position;
        }

        public bool CheckMeetingAvailable(int metId, DateTime startTime, DateTime endTime)
        {
            return !(db.meeting_info.Any(r => r.id != metId && r.mi_status == true &&
                (r.mi_start_time <= startTime && startTime < r.mi_end_time ||
                r.mi_start_time < endTime && endTime <= r.mi_end_time ||
                r.mi_start_time == startTime && r.mi_end_time == endTime)));
        }

        public int UpdateMeetingStatus(string ids, short status)
        {
            return db.ExecuteStoreCommand("Update meeting_info Set mi_status=" + status + " Where id In(" + ids + ")");
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
        }

        public int DeleteMeetingPeopleByMeetingId(int meetingId)
        {
            return db.ExecuteStoreCommand("Delete From meeting_people Where mp_meeting_id = {0}", meetingId);
        }

        public void AddMeetingPeople(meeting_people mp)
        {
            db.meeting_people.AddObject(mp);
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