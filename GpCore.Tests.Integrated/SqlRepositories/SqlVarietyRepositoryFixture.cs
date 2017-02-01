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
            Variety newVariety = new Variety("plum tomatoes");

            repo.Save(newVariety);

            Assert.False(newVariety.IsNew);
        }

        [Fact]
        public void GetVarietyAfterSaving()
        {
            Variety newVariety = new Variety("plum tomatoes");

            repo.Save(newVariety);

            Variety savedVariety = repo.Get(newVariety.Id);

            Assert.NotNull(savedVariety);
            Assert.Equal("plum tomatoes", savedVariety.Name);
        }

        [Fact]
        public void UpdateVarietyWithNewName()
        {
            Variety newVariety = new Variety("plum tomatoes");

            repo.Save(newVariety);

            Variety savedVariety = repo.Get(newVariety.Id);

            savedVariety.Rename("heirloom plum tomatoes");

            repo.Save(savedVariety);

            savedVariety = repo.Get(newVariety.Id);

            Assert.Equal("heirloom plum tomatoes", savedVariety.Name);
        }

    }
}
