using GpCore.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GpCore.Model.Domain;
using GpCore.Model.Common;
using System.Data.SqlClient;

namespace GpCore.Model.Sql
{
    public class SqlVarietyRepository : VarietyRepository
    {
        private SqlUnitOfWork _unitOfWork;


        public SqlVarietyRepository(SqlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Variety Get(Guid id)
        {
            Variety variety = null;
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandText = "Select VarietyId, Name, CreateDate, LastModifiedDate From dbo.Varieties Where VarietyId = @VarietyId";
            cmd.Parameters.AddWithValue("@VarietyId", id);

            using (SqlDataReader infoReader = cmd.ExecuteReader())
            {
                if (infoReader.Read())
                {
                    int idxVarietyId = infoReader.GetOrdinal("VarietyId");
                    int idxName = infoReader.GetOrdinal("Name");
                    int idxCreateDate = infoReader.GetOrdinal("CreateDate");
                    int idxLastModifiedDate = infoReader.GetOrdinal("LastModifiedDate");

                    variety = new Variety(
                        EntityId.ForExistingEntity(infoReader.GetGuid(idxVarietyId)),
                        new TimeStamp(infoReader.GetDateTimeOffset(idxCreateDate).DateTime,
                        infoReader.GetSafeDateTimeOffset(idxLastModifiedDate)),
                        infoReader.GetString(idxName));
                }
            }

            return variety;
        }

        public void Save(Variety variety)
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();

            if (variety.IsNew)
            {
                cmd.CommandText = "insert into dbo.Varieties(VarietyId, Name, CreateDate) values(@VarietyId, @Name, @CreateDate)";
                cmd.Parameters.AddWithValue("@VarietyId", variety.Id);
                cmd.Parameters.AddWithValue("@Name", variety.Name);
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.UtcNow);
            }
            else
            {
                cmd.CommandText = "update dbo.Varieties Set Name = @Name, LastModifiedDate = @LastModifiedDate Where VarietyId = @VarietyId";
                cmd.Parameters.AddWithValue("@VarietyId", variety.Id);
                cmd.Parameters.AddWithValue("@Name", variety.Name);

                variety.UpdateTimeStamp();

                cmd.Parameters.AddWithValue("@LastModifiedDate", variety.TimeStamp.LastModifiedDate);
            }

            cmd.ExecuteNonQuery();

            variety.AcceptChanges();
        }
    }
}
