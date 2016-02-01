using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Data.Entities
{
    public class Variety
    {
        public int? VarietyId { get; set; }
        public string Name { get; set; }
        public Family Family { get; set; }
    }
}
