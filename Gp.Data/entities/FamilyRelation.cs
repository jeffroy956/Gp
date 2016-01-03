using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Data.entities
{
    internal class FamilyRelation
    {
        public int FamilyId { get; set; }
        public int RelatedFamilyId { get; set; }
    }
}
