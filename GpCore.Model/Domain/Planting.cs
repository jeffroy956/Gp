using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Domain
{
    public class Planting
    {
        public Planting(Variety variety, DateTime plantDate)
        {
            Variety = variety;
            PlanDate = plantDate;
        }

        public Variety Variety { get; private set; }
        public DateTime PlanDate { get; set; }
    }
}
