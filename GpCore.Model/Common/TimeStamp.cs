using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Common
{
    public class TimeStamp
    {
        public TimeStamp(DateTime createDate, DateTime? lastModifiedDate)
        {
            CreateDate = createDate;
            LastModifiedDate = lastModifiedDate;
        }

        public DateTime CreateDate { get; private set; }
        public DateTime? LastModifiedDate { get; private set; }

        public static TimeStamp ForNewRecord()
        {
            return new TimeStamp(DateTime.UtcNow, null);
        }

        public void Update()
        {
            LastModifiedDate = DateTime.UtcNow;
        }
    }
}
