using GpCore.Model.Common;
using GpCore.Model.Domain;
using GpCore.Model.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GpCore.Tests.Integrated.SqlRepositories
{
    public class SqlVarietyRepositoryFixture: BaseSqlRepositoryFixture
    {

        private SqlVarietyRepository repo;
        public SqlVarietyRepositoryFixture(): base()
        {
            repo = new SqlVarietyRepository(UnitOfWork);
        }

        [Fact]
        public void SavingVarietyResetsIsNewFlag()
        {
            Variety newVariety = new Variety(EntityId.ForNewEntity(), "plum tomatoes");

            repo.Save(newVariety);

            Assert.False(newVariety.Id.IsNew);
        }

        [Fact]
        public void GetVarietyAfterSaving()
        {
            Variety newVariety = new Variety(EntityId.ForNewEntity(), "plum tomatoes");

            repo.Save(newVariety);

            Variety savedVariety = repo.Get(newVariety.Id);

            Assert.NotNull(savedVariety);
            Assert.Equal("plum tomatoes", savedVariety.Name);
        }

    }
}
