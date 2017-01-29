using System;
using System.Collections.Generic;
using GpCore.Model.Domain;
using GpCore.Model.Common;
using System.Data.SqlClient;

namespace GpCore.Model.Sql
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
        

        public void Insert(Plan entity)
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = "insert into dbo.tblPlan(PlanId, CalendarId, RecurrenceId, EventDescription, VarietyId, PlanDate, ActualDate, Notes) values(@PlanId, @CalendarId, @RecurrenceId, @EventDescription, @VarietyId, @PlanDate, @ActualDate, @Notes)";
            int planId = GetNextPlanId();

            cmd.Parameters.AddWithValue("@PlanId", planId);
            cmd.Parameters.AddWithValue("@RecurrenceId", DBNull.Value);
            cmd.Parameters.AddWithValue("@EventDescription", DbUtil.GetDbParamValue(entity.EventDescription));

            cmd.Parameters.AddWithValue("@PlanDate", DbUtil.GetDbParamValue(entity.PlanDate));
            cmd.Parameters.AddWithValue("@ActualDate", DbUtil.GetDbParamValue(entity.ActualDate));

            cmd.ExecuteNonQuery();

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
            cmd.Parameters.AddWithValue("@RecurrenceId", DBNull.Value);
            cmd.Parameters.AddWithValue("@EventDescription", DbUtil.GetDbParamValue(entity.EventDescription));

            cmd.Parameters.AddWithValue("@PlanDate", DbUtil.GetDbParamValue(entity.PlanDate));
            cmd.Parameters.AddWithValue("@ActualDate", DbUtil.GetDbParamValue(entity.ActualDate));

            cmd.ExecuteNonQuery();
        }
    }
}
