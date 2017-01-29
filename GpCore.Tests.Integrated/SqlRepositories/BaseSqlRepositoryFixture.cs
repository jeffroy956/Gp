using GpCore.Model.Sql;
using GpCore.Tests.Integrated.Common;
using Microsoft.Extensions.Configuration;
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

            UnitOfWork = new SqlUnitOfWork(ConfigManager.AppSettings.GetConnectionString("gp"));
        }
        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

    }
}
