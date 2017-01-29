using GpCore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GpCore.Tests.Integrated.SqlRepositories
{
    public class SqlVarietyRepositoryFixture: BaseSqlRepositoryFixture
    {

        [Fact]
        public void SaveVariety()
        {
            Variety newVariety = new Variety("plum tomatoes");


            Assert.True(true);
        }

    }
}
