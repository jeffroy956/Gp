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
        public void CreateNewIdSetsCreationTimeStamp()
        {
            EntityId id = EntityId.ForNewEntity();

            Assert.NotEqual(DateTime.MinValue, id.CreateDate);
            Assert.Equal(DateTimeKind.Utc, id.CreateDate.Kind);
        }

        [Fact]
        public void CreateNewIdSetsIsNewFlag()
        {
            EntityId id = EntityId.ForNewEntity();

            Assert.True(id.IsNew);
        }

        [Fact]
        public void IsNowPersistedClearsNewFlag()
        {
            EntityId id = EntityId.ForNewEntity();

            id.IsNowPersisted();

            Assert.False(id.IsNew);

        }
    }
}
