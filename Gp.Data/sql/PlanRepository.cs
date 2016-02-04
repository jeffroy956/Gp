using Gp.Data.Common;
using Gp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Data.Sql
{
    public class PlanRepository : Repository<Plan>
    {
        private SqlUnitOfWork _unitOfWork;
        public PlanRepository(SqlUnitOfWork unitOfWork, Repository<Calendar> calendarRepository, Repository<Variety> varietyRepository)
        {
            _unitOfWork = unitOfWork;
        }

        public Plan Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Plan> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Plan entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Plan entity)
        {
            throw new NotImplementedException();
        }
    }
}
