using GpCore.Model.Common;
using GpCore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GpCore.Tests.Unit.Domain
{
    public class VarietyFixture
    {
        [Fact]
        public void CreateNewVarietyThatIsNotYetPersisted()
        {
            Variety variety = new Variety("Greens");

            Assert.True(variety.IsNew);
            Assert.Equal("Greens", variety.Name);
        }


    }
}
