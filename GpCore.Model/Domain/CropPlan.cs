using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Domain
{
    public class CropPlan
    {
        private CropPlan(Variety variety)
        {
            _variety = variety;
            Plantings = _plantings;
        }

        private List<Planting> _plantings = new List<Planting>();
        private Variety _variety;

        public void AddPlanting(Planting planting)
        {
            _plantings.Add(planting);
        }

        public IReadOnlyList<Planting> Plantings { get; private set; }

        public static CropPlan DirectSeed(Variety variety)
        {
            return new CropPlan(variety);
        }

        public CropPlan StartOn(DateTime dateTime)
        {
            _plantings.Add(new Planting(_variety, dateTime));

            return this;
        }


    }
}
