using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Data.Entities
{
    public class Plan
    {
        public int? PlanId { get; set; }
        public string Description { get; set; }
        public Variety Variety { get; set; }
        public DateTime PlanDate { get; set; }
        public DateTime ActualDate { get; set; }
        public string Notes { get; set; }
    }
}
