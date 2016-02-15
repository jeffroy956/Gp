using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Data.Entities
{
    public class VarietyEditViewModel
    {
        public Variety Variety { get; set; }
        public IEnumerable<Family> AvailableFamilies { get; set; }
    }
}
