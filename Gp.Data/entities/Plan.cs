using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Data.Entities
{
    public class Plan
    {
        public Guid PlanId { get; set; }
        public string EventDescription { get; set; }

        public Calendar Calendar { get; set; }
        public Variety Variety { get; set; }
        public DateTime? PlanDate { get; set; }
        public DateTime? ActualDate { get; set; }
    }
}
