using Gp.Data.entities;
using Gp.Data.sql;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gp.Controllers
{
    public class FamiliesController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Family> Get()
        {
            List<Family> allFamilies;

            using (SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                FamilyRepository familyRepo = new FamilyRepository(unitOfWork);
                allFamilies = familyRepo.GetAll();

                unitOfWork.Commit();
            }

            return allFamilies;
        }

        // GET api/<controller>/5
        public Family Get(int id)
        {
            Family rtnFamily;
            using (SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                FamilyRepository familyRepo = new FamilyRepository(unitOfWork);
                rtnFamily = familyRepo.Get(id);

                unitOfWork.Commit();
            }

            return rtnFamily;
        }

        // POST api/<controller>
        public void Post([FromBody]List<Family> families)
        {
            using(SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                FamilyRepository familyRepo = new FamilyRepository(unitOfWork);

                foreach(Family family in families)
                {
                    if (family.FamilyId == null)
                    {
                        familyRepo.Insert(family);
                    }
                    else
                    {
                        familyRepo.Update(family);
                    }
                }

                unitOfWork.Commit();
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}