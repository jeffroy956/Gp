﻿using Gp.Data.Common;
using Gp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Data.Sql
{
    public class PlanRepository : Repository<Plan>
    {
        private SqlUnitOfWork _unitOfWork;
        private Repository<Calendar> _calendarRepository;
        private Repository<Variety> _varietyRepository;

        public PlanRepository(SqlUnitOfWork unitOfWork, Repository<Calendar> calendarRepository, Repository<Variety> varietyRepository)
        {
            _unitOfWork = unitOfWork;
            _calendarRepository = calendarRepository;
            _varietyRepository = varietyRepository;
        }

        public Plan Get(int id)
        {
            Plan rtnPlan = null;

            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = "Select PlanId, CalendarId, RecurrenceId, Description, VarietyId, PlanDate, ActualDate, Notes From dbo.tblPlan Where PlanId = @PlanId";
            cmd.Parameters.AddWithValue("@PlanId", id);

            using (SqlDataReader r = cmd.ExecuteReader())
            {
                if (r.Read())
                {
                    PlanMapper mapper = new PlanMapper(r, _calendarRepository, _varietyRepository);

                    rtnPlan = mapper.Map();
                }
            }

            return rtnPlan;
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
            private int idxDescription;
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
                idxDescription = r.GetOrdinal("Description");
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
                    Description = r.GetString(idxDescription),
                    PlanDate = r.GetSafeDateTime(idxPlanDate),
                    ActualDate = r.GetSafeDateTime(idxActualDate),
                    Notes = r.GetSafeString(idxNotes)
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

            cmd.CommandText = "insert into dbo.tblPlan(PlanId, CalendarId, RecurrenceId, Description, VarietyId, PlanDate, ActualDate, Notes) values(@PlanId, @CalendarId, @RecurrenceId, @Description, @VarietyId, @PlanDate, @ActualDate, @Notes)";
            int planId = GetNextPlanId();

            cmd.Parameters.AddWithValue("@PlanId", planId);
            cmd.Parameters.AddWithValue("@CalendarId", entity.Calendar.CalendarId);
            cmd.Parameters.AddWithValue("@RecurrenceId", DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", DbUtil.GetDbParamValue(entity.Description));
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
            throw new NotImplementedException();
        }
    }
}
