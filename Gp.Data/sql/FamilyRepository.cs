using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gp.Data.Entities;
using System.Data.SqlClient;
using System.Data;
using Gp.Data.Common;

namespace Gp.Data.Sql
{
    public class FamilyRepository : Repository<Family>
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
            familyCmd.CommandText = "update dbo.tblFamilies Set Name = @Name, LastModified = GetUtcDate() Where FamilyId = @FamilyId";
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
            List<FamilyRelation> relatedFamilies = GetRelationsFromDataStore(familyId, relationTableName);

            foreach (FamilyRelation relatedFamily in relatedFamilies)
            {
                relations.Add(GetFamilyInternal(relatedFamily.RelatedFamilyId));
            }
        }

        public List<Family> GetAll()
        {
            List<Family> rtnFamilies = GetFamiliesFromDataStore(null);
            List<FamilyRelation> allCompanions = GetRelationsFromDataStore(null, "tblCompanions");
            List<FamilyRelation> allEnemies = GetRelationsFromDataStore(null, "tblEnemies");

            foreach(Family family in rtnFamilies)
            {
                foreach(FamilyRelation companion in allCompanions
                    .Where(ac => ac.FamilyId == family.FamilyId))
                {
                    family.Companions.Add(GetFamilyInternal(companion.RelatedFamilyId));
                }

                foreach (FamilyRelation enemy in allEnemies
                    .Where(ac => ac.FamilyId == family.FamilyId))
                {
                    family.Enemies.Add(GetFamilyInternal(enemy.RelatedFamilyId));
                }
            }

            return rtnFamilies;
        }
        private List<FamilyRelation> GetRelationsFromDataStore(int? familyId, string relationTableName)
        {
            SqlCommand relationCmd = _unitOfWork.CreateCommand();
            relationCmd.CommandType = System.Data.CommandType.Text;
            relationCmd.CommandText = "Select FamilyId, RelatedFamilyId From dbo." + relationTableName;
            if (familyId != null)
            {
                relationCmd.CommandText = relationCmd.CommandText + " Where FamilyId = @FamilyId";
                relationCmd.Parameters.AddWithValue("@FamilyId", familyId);
            }

            List<FamilyRelation> relatedFamilies = new List<FamilyRelation>();
            using (SqlDataReader r = relationCmd.ExecuteReader())
            {
                while (r.Read())
                {
                    relatedFamilies.Add(new FamilyRelation()
                    {
                        FamilyId = r.GetInt32(0),
                        RelatedFamilyId = r.GetInt32(1)
                    });
                }
            };
            return relatedFamilies;
        }

        private Dictionary<int, Family> _familyCache = new Dictionary<int, Family>();

        private Family GetFamilyInternal(int familyId)
        {
            Family family = null;

            if (!_familyCache.TryGetValue(familyId, out family))
            {
                List<Family> foundFamilies = GetFamiliesFromDataStore(familyId);
                if (foundFamilies.Count == 1)
                {
                    family = foundFamilies[0];
                }
            }

            return family;
        }

        private List<Family> GetFamiliesFromDataStore(int? familyId)
        {
            List<Family> foundFamilies = new List<Family>();

            SqlCommand familyCmd = _unitOfWork.CreateCommand();
            familyCmd.CommandType = System.Data.CommandType.Text;
            familyCmd.CommandText = "Select FamilyId, Name From dbo.tblFamilies";
            if (familyId != null)
            {
                familyCmd.CommandText = familyCmd.CommandText + " Where FamilyId = @FamilyId";
                familyCmd.Parameters.AddWithValue("@FamilyId", familyId);
            }

            using (SqlDataReader r = familyCmd.ExecuteReader())
            {
                int idxFamilyId = r.GetOrdinal("FamilyId");
                int idxName = r.GetOrdinal("Name");
                while (r.Read())
                {
                    Family loadedFamily = new Family()
                    {
                        FamilyId = r.GetInt32(idxFamilyId),
                        Name = r.GetString(idxName)
                    };
                    foundFamilies.Add(loadedFamily);

                   _familyCache[loadedFamily.FamilyId.Value] = loadedFamily;
                }
            }

            return foundFamilies;
        }
    }
}
