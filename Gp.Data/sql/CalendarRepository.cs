using Gp.Data.Common;
using Gp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Data.Sql
{
    public class CalendarRepository : Repository<Calendar>
    {
        private SqlUnitOfWork _unitOfWork;
        public CalendarRepository(SqlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Calendar Get(int id)
        {
            Calendar rtnCalendar = null;

            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = "Select CalendarId, Description, Year From dbo.tblCalendar Where CalendarId = @CalendarId";
            cmd.Parameters.AddWithValue("@CalendarId", id);

            using (SqlDataReader r = cmd.ExecuteReader())
            {
                if (r.Read())
                {
                    rtnCalendar = new Calendar()
                    {
                        CalendarId = r.GetInt32(0),
                        Description = r.GetString(1),
                        Year = r.GetInt32(2)
                    };
                }
            }

            return rtnCalendar;
        }

        public List<Calendar> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Calendar entity)
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = "insert into dbo.tblCalendar(CalendarId, Description, Year) values(@CalendarId, @Description, @Year)";
            int calendarId = GetNextCalendarId();
            cmd.Parameters.AddWithValue("@CalendarId", calendarId);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            cmd.Parameters.AddWithValue("@Year", entity.Year);

            cmd.ExecuteNonQuery();

            entity.CalendarId = calendarId;
        }

        private int GetNextCalendarId()
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select IsNull(max(CalendarId), 0) from dbo.tblCalendar";
            return ((int)cmd.ExecuteScalar()) + 1;
        }


        public void Update(Calendar entity)
        {
            throw new NotImplementedException();
        }
    }
}
