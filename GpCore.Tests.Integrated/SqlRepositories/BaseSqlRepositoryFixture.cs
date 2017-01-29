using GpCore.Model.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Tests.Integrated.SqlRepositories
{
    public class BaseSqlRepositoryFixture : IDisposable
    {
        public SqlUnitOfWork UnitOfWork { get; private set; }

        public BaseSqlRepositoryFixture()
        {
            //UnitOfWork = new SqlUnitOfWork("GP");
        }
        public void Dispose()
        {
            //UnitOfWork.Dispose();
        }

    }
}
