using GpCore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GpCore.Tests.Unit.Domain
{
    public class CropPlanFixture
    {

        [Fact]
        public void DirectSeedingCropCreatesSinglePlanting()
        {
            Variety corn = new Variety("corn");

            CropPlan cropPlan = CropPlan.DirectSeed(corn).StartOn(new DateTime(2017, 4, 5));

            Assert.Equal(1, cropPlan.Plantings.Count);
            Assert.Equal(new DateTime(2017, 4, 5), cropPlan.Plantings[0].PlanDate);
        }
    }
}
