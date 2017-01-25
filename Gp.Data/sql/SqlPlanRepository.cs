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
    public class SqlPlanRepository
    {


        private SqlUnitOfWork _unitOfWork;

        public SqlPlanRepository(SqlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Plan> GetAll()
        {
            throw new NotImplementedException();
        }

        private class PlanMapper
        {
            private int idxPlanId;
            private int idxCalendarId;
            private int idxRecurrenceId;
            private int idxEventDescription;
            private int idxVarietyId;
            private int idxPlanDate;
            private int idxActualDate;
            private int idxNotes;
            private Repository<Calendar> calendarRepository;
            private Repository<Variety> varietyRepository;

            private SqlDataReader r;
            public PlanMapper(SqlDataReader r, Repository<Calendar> calendarRepository, Repository<Variety> varietyRepository)
            {
                this.r = r;
                this.calendarRepository = calendarRepository;
                this.varietyRepository = varietyRepository;

                idxPlanId = r.GetOrdinal("PlanId");
                idxCalendarId = r.GetOrdinal("CalendarId");
                idxRecurrenceId = r.GetOrdinal("RecurrenceId");
                idxEventDescription = r.GetOrdinal("EventDescription");
                idxVarietyId = r.GetOrdinal("VarietyId");
                idxPlanDate = r.GetOrdinal("PlanDate");
                idxActualDate = r.GetOrdinal("ActualDate");
                idxNotes = r.GetOrdinal("Notes");
            }

            public Plan Map()
            {
                Plan plan = new Plan()
                {
                    PlanId = r.GetInt32(idxPlanId),
                    EventDescription = r.GetString(idxEventDescription),
                    PlanDate = r.GetSafeDateTime(idxPlanDate),
                    ActualDate = r.GetSafeDateTime(idxActualDate),
                    Notes = r.GetSafeString(idxNotes),
                    r.GetDecimal
                };

                plan.Calendar = calendarRepository.Get(r.GetInt32(idxCalendarId));
                int? varietyId = r.GetSafeInt32(idxVarietyId);
                if (varietyId != null)
                {
                    plan.Variety = varietyRepository.Get(varietyId.Value);
                }

                return plan;
            }
        }

        public void Insert(Plan entity)
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = "insert into dbo.tblPlan(PlanId, CalendarId, RecurrenceId, EventDescription, VarietyId, PlanDate, ActualDate, Notes) values(@PlanId, @CalendarId, @RecurrenceId, @EventDescription, @VarietyId, @PlanDate, @ActualDate, @Notes)";
            int planId = GetNextPlanId();

            cmd.Parameters.AddWithValue("@PlanId", planId);
            cmd.Parameters.AddWithValue("@CalendarId", entity.Calendar.CalendarId);
            cmd.Parameters.AddWithValue("@RecurrenceId", DBNull.Value);
            cmd.Parameters.AddWithValue("@EventDescription", DbUtil.GetDbParamValue(entity.EventDescription));
            if (entity.Variety != null)
            {
                cmd.Parameters.AddWithValue("@VarietyId", entity.Variety.VarietyId);
            }
            else
            {
                cmd.Parameters.AddWithValue("@VarietyId", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@PlanDate", DbUtil.GetDbParamValue(entity.PlanDate));
            cmd.Parameters.AddWithValue("@ActualDate", DbUtil.GetDbParamValue(entity.ActualDate));
            cmd.Parameters.AddWithValue("@Notes", entity.Notes);

            cmd.ExecuteNonQuery();

            entity.PlanId = planId;
        }

        private int GetNextPlanId()
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select IsNull(max(PlanId), 0) from dbo.tblPlan";
            return ((int)cmd.ExecuteScalar()) + 1;
        }

        public void Update(Plan entity)
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = "update dbo.tblPlan Set CalendarId = @CalendarId, RecurrenceId = @RecurrenceId, EventDescription = @EventDescription, VarietyId = @VarietyId, PlanDate = @PlanDate, ActualDate = @ActualDate, Notes = @Notes Where PlanId = @PlanId";

            cmd.Parameters.AddWithValue("@PlanId", entity.PlanId);
            cmd.Parameters.AddWithValue("@CalendarId", entity.Calendar.CalendarId);
            cmd.Parameters.AddWithValue("@RecurrenceId", DBNull.Value);
            cmd.Parameters.AddWithValue("@EventDescription", DbUtil.GetDbParamValue(entity.EventDescription));
            if (entity.Variety != null)
            {
                cmd.Parameters.AddWithValue("@VarietyId", entity.Variety.VarietyId);
            }
            else
            {
                cmd.Parameters.AddWithValue("@VarietyId", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@PlanDate", DbUtil.GetDbParamValue(entity.PlanDate));
            cmd.Parameters.AddWithValue("@ActualDate", DbUtil.GetDbParamValue(entity.ActualDate));
            cmd.Parameters.AddWithValue("@Notes", entity.Notes);

            cmd.ExecuteNonQuery();
        }
    }
}
