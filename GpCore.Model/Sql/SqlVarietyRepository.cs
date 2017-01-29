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

        public Variety Get(EntityId id)
        {
            Variety variety = null;
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandText = "Select VarietyId, Name, CreateDate From dbo.Varieties Where VarietyId = @VarietyId";
            cmd.Parameters.AddWithValue("@VarietyId", id.Id);

            using (SqlDataReader infoReader = cmd.ExecuteReader())
            {
                if (infoReader.Read())
                {
                    int idxVarietyId = infoReader.GetOrdinal("VarietyId");
                    int idxName = infoReader.GetOrdinal("Name");
                    int idxCreateDate = infoReader.GetOrdinal("CreateDate");

                    variety = new Variety(
                        EntityId.ForExistingEntity(infoReader.GetGuid(idxVarietyId),
                        infoReader.GetDateTimeOffset(idxCreateDate).DateTime),
                        infoReader.GetString(idxName));
                }
            }

            return variety;
        }

        public void Save(Variety variety)
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();

            cmd.CommandText = "insert into dbo.Varieties(VarietyId, Name, CreateDate) values(@VarietyId, @Name, @CreateDate)";
            cmd.Parameters.AddWithValue("@VarietyId", variety.Id.Id);
            cmd.Parameters.AddWithValue("@Name", variety.Name);
            cmd.Parameters.AddWithValue("@CreateDate", variety.Id.CreateDate);

            cmd.ExecuteNonQuery();

            variety.Id.AcceptChanges();
        }
    }
}
