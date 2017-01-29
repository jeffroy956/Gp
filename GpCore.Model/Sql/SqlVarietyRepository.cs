using GpCore.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GpCore.Model.Domain;

namespace GpCore.Model.Sql
{
    public class SqlVarietyRepository : VarietyRepository
    {
        private SqlUnitOfWork _unitOfWork;


        public SqlVarietyRepository(SqlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Variety Get(Guid varietyId)
        {
            throw new NotImplementedException();
        }
        public void Insert(Variety variety)
        {
            throw new NotImplementedException();
        }
    }
}
