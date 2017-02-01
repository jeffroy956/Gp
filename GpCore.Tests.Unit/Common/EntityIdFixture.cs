using GpCore.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GpCore.Tests.Unit.Common
{
    public class EntityIdFixture
    {
        [Fact]
        public void CreateNewIdInitializesGuid()
        {
            EntityId id = EntityId.ForNewEntity();

            Assert.NotNull(id);
            Assert.NotEqual(Guid.Empty, id.Id);
        }

        [Fact]
        public void CreateNewIdSetsIsNewFlag()
        {
            EntityId id = EntityId.ForNewEntity();

            Assert.True(id.IsNew);
        }

        [Fact]
        public void AcceptChangesClearsNewFlag()
        {
            EntityId id = EntityId.ForNewEntity();

            id.AcceptChanges();

            Assert.False(id.IsNew);
        }

        [Fact]
        public void CreateEntityIdForExistingEntity()
        {
            EntityId id = EntityId.ForExistingEntity(Guid.NewGuid());

            Assert.NotNull(id);
            Assert.NotEqual(Guid.Empty, id.Id);
        }
    }
}
