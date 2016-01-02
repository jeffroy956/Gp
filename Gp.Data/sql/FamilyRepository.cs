using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gp.Data.entities;
using System.Data.SqlClient;
using System.Data;

namespace Gp.Data.sql
{
    public class FamilyRepository
    {
        private SqlUnitOfWork _unitOfWork;
        public FamilyRepository(SqlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Insert(Family newFamily)
        {
            SqlCommand familyCommand = _unitOfWork.CreateCommand();
            familyCommand.CommandType = System.Data.CommandType.Text;

            familyCommand.CommandText = "insert into dbo.tblFamilies(FamilyId, Name) values(@FamilyId, @Name)";
            int familyId = GetNextFamilyId();
            familyCommand.Parameters.AddWithValue("@FamilyId", familyId);
            familyCommand.Parameters.AddWithValue("@Name", newFamily.Name);
            familyCommand.ExecuteNonQuery();
            newFamily.FamilyId = familyId;

            if (newFamily.Companions.Count > 0)
            {
                SyncRelatedFamilies(newFamily.FamilyId.Value, newFamily.Companions, "tblCompanions");
            }

            if (newFamily.Enemies.Count > 0)
            {
                SyncRelatedFamilies(newFamily.FamilyId.Value, newFamily.Enemies, "tblEnemies");
            }
        }
        private int GetNextFamilyId()
        {
            SqlCommand cmd = _unitOfWork.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select IsNull(max(FamilyId), 0) from dbo.tblFamilies";
            return ((int)cmd.ExecuteScalar()) + 1;
        }

        public void Update(Family family)
        {
            SqlCommand familyCmd = _unitOfWork.CreateCommand();
            familyCmd.CommandText = "update dbo.tblFamilies Set Name = @Name Where FamilyId = @FamilyId";
            familyCmd.Parameters.AddWithValue("Name", family.Name);
            familyCmd.Parameters.AddWithValue("FamilyId", family.FamilyId.Value);

            familyCmd.ExecuteNonQuery();

            SyncRelatedFamilies(family.FamilyId.Value, family.Companions, "tblCompanions");
            SyncRelatedFamilies(family.FamilyId.Value, family.Enemies, "tblEnemies");
        }

        private void SyncRelatedFamilies(int familyId, IEnumerable<Family> relations, string relationTableName)
        {
            StringBuilder inClause = new StringBuilder();
            int i = 0;
            foreach(Family relation in relations)
            {
                inClause.Append(relation.FamilyId.Value + ",");
                i++;
            }

            string deleteCriteria = string.Empty;
            if (inClause.Length > 0)
            {
                inClause.Length -= 1;
                deleteCriteria = " and RelatedFamilyId not in (" + inClause.ToString() + ")";
            }

            SqlCommand deleteCmd = _unitOfWork.CreateCommand();
            deleteCmd.CommandType = CommandType.Text;
            deleteCmd.CommandText = "delete from dbo." + relationTableName + " where FamilyId = @FamilyId" + deleteCriteria;
            deleteCmd.Parameters.AddWithValue("FamilyId", familyId);
            deleteCmd.ExecuteNonQuery();

            SqlCommand existsCmd = _unitOfWork.CreateCommand();
            existsCmd.CommandType = CommandType.Text;
            existsCmd.CommandText = "select 1 from dbo." + relationTableName + " where FamilyId = @FamilyId And RelatedFamilyId = @RelatedFamilyId";
            var existsFamilyId = existsCmd.Parameters.Add("FamilyId", SqlDbType.Int);
            var existsRelatedFamilyId = existsCmd.Parameters.Add("RelatedFamilyId", SqlDbType.Int);
            existsCmd.Prepare();

            SqlCommand insertCmd = _unitOfWork.CreateCommand();
            insertCmd.CommandType = System.Data.CommandType.Text;
            insertCmd.CommandText = "insert into dbo." + relationTableName + "(FamilyId, RelatedFamilyId) values(@FamilyId, @RelatedFamilyId)";
            var familyIdParm = insertCmd.Parameters.Add("FamilyId", SqlDbType.Int);
            var relatedFamilyIdParm = insertCmd.Parameters.Add("RelatedFamilyId", SqlDbType.Int);
            insertCmd.Prepare();

            familyIdParm.Value = familyId;
            existsFamilyId.Value = familyId;

            foreach (Family relation in relations)
            {
                existsRelatedFamilyId.Value = relation.FamilyId.Value;

                object rtnValue = existsCmd.ExecuteScalar();
                if (rtnValue == null || rtnValue == DBNull.Value)
                {
                    relatedFamilyIdParm.Value = relation.FamilyId.Value;
                    insertCmd.ExecuteNonQuery();
                }
            }
        }

        public Family Get(int familyId)
        {
            Family family = GetFamilyInternal(familyId);

            GetFamilyRelations(familyId, family.Companions, "tblCompanions");
            GetFamilyRelations(familyId, family.Enemies, "tblEnemies");

            return family;
        }

        private void GetFamilyRelations(int familyId, List<Family> relations, string relationTableName)
        {
            SqlCommand companionCmd = _unitOfWork.CreateCommand();
            companionCmd.CommandType = System.Data.CommandType.Text;
            companionCmd.CommandText = "Select RelatedFamilyId From dbo." + relationTableName + " Where FamilyId = @FamilyId";
            companionCmd.Parameters.AddWithValue("@FamilyId", familyId);

            List<int> relatedFamilies = new List<int>();
            using (SqlDataReader r = companionCmd.ExecuteReader())
            {
                while (r.Read())
                {
                    relatedFamilies.Add(r.GetInt32(0));
                }
            };

            foreach (int relatedFamilyId in relatedFamilies)
            {
                relations.Add(GetFamilyInternal(relatedFamilyId));
            }
        }

        private Dictionary<int, Family> _familyCache = new Dictionary<int, Family>();

        private Family GetFamilyInternal(int familyId)
        {
            Family family = null;

            if (!_familyCache.TryGetValue(familyId, out family))
            {
                SqlCommand familyCmd = _unitOfWork.CreateCommand();
                familyCmd.CommandType = System.Data.CommandType.Text;
                familyCmd.CommandText = "Select FamilyId, Name From dbo.tblFamilies Where FamilyId = @FamilyId";
                familyCmd.Parameters.AddWithValue("@FamilyId", familyId);

                using (SqlDataReader r = familyCmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        family = new Family()
                        {
                            FamilyId = r.GetInt32(r.GetOrdinal("FamilyId")),
                            Name = r.GetString(r.GetOrdinal("Name"))
                        };

                        _familyCache.Add(familyId, family);
                    }
                }


            }

            return family;
        }
    }
}
