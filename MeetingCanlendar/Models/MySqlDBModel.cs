using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBEntity;

namespace MeetingCanlendar.Models
{
    public class MySqlDBModel
    {
        public mc_dbEntities db = new mc_dbEntities();

        public void SaveChange()
        {
            db.SaveChanges();
        }
    }
}