using GpCore.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Domain
{
    public abstract class PlantingMethod : Enumeration
    {

        public static readonly PlantingMethod DirectSeed = new DirectSeedMethod();

        protected PlantingMethod()
        {
        }

        protected PlantingMethod(int value, string displayName) : base(value, displayName)
        {

        }

        private class DirectSeedMethod : PlantingMethod
        {
            public DirectSeedMethod():base(0, "Direct Seed")
            {

            }
        }
    }
}
