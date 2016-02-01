using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Data.Entities
{
    public class Family
    {
        public int? FamilyId { get; set; }
        public string Name { get; set; }

        private List<Family> _companions = new List<Family>();
        public List<Family> Companions {
            get
            {
                return _companions;
            }
        }

        private List<Family> _enemies = new List<Family>();
        public List<Family> Enemies
        {
            get
            {
                return _enemies;
            }
        }

    }
}
