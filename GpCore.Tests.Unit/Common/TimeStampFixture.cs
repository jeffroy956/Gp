using GpCore.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GpCore.Tests.Unit.Common
{
    public class TimeStampFixture
    {
        [Fact]
        public void CreateForNew()
        {
            var timestamp = TimeStamp.ForNewRecord();

            Assert.NotEqual(DateTime.MinValue, timestamp.CreateDate);
        }

        [Fact]
        public void UpdateTimestamp()
        {
            var timestamp = new TimeStamp(DateTime.UtcNow.AddDays(-1), null);

            timestamp.Update();

            Assert.NotEqual(null, timestamp.LastModifiedDate);
        }

    }
}
