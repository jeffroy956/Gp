using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Data.Entities
{
    public class Calendar
    {
        public int? CalendarId { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
    }
}
