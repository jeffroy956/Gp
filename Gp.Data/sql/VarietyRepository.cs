using Gp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gp.Data.Common;

namespace Gp.Data.Sql
{
    public class VarietyRepository
    {

        private SqlUnitOfWork _unitOfWork;
        public VarietyRepository(SqlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Variety> GetAll()
        {
            return new List<Variety>();
        }

        public Variety Get(int id)
        {
            Variety rtnFamily = null;

            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = "Select VarietyId, Name, FamilyId From dbo.tblVarieties Where VarietyId = @VarietyId";
            cmd.Parameters.AddWithValue("@VarietyId", id);

            int? familyId = null;
            using (SqlDataReader r = cmd.ExecuteReader())
            {
                if (r.Read())
                {
                    rtnFamily = new Variety()
                    {
                        VarietyId = r.GetInt32(0),
                        Name = r.GetString(1),
                    };

                    familyId = r.GetSafeInt32(2);
                };
            };

            if (familyId != null)
            {
                FamilyRepository familyRepo = new FamilyRepository(_unitOfWork);
                rtnFamily.Family = familyRepo.Get(familyId.Value);
            }

            return rtnFamily;
        }

        public void Insert(Variety variety)
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = "insert into dbo.tblVarieties(VarietyId, Name, FamilyId) values(@VarietyId, @Name, @FamilyId)";
            int varietyId = GetNextVarietyId();
            cmd.Parameters.AddWithValue("@VarietyId", varietyId);
            cmd.Parameters.AddWithValue("@Name", variety.Name);
            if (variety.Family == null)
            {
                cmd.Parameters.AddWithValue("@FamilyId", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FamilyId", variety.Family.FamilyId);
            }
            cmd.ExecuteNonQuery();

            variety.VarietyId = varietyId;
        }

        private int GetNextVarietyId()
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select IsNull(max(VarietyId), 0) from dbo.tblVarieties";
            return ((int)cmd.ExecuteScalar()) + 1;
        }


        public void Update(Variety variety)
        {

        }

    }
}
