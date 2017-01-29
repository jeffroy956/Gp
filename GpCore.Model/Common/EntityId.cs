using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Common
{
    public class EntityId
    {
        public EntityId(Guid id, DateTime createDate)
        {
            Id = id;
            CreateDate = createDate;
        }

        public Guid Id { get; private set; }
        public DateTime CreateDate { get; private set; }

        public bool IsNew { get; private set; }

        public static EntityId NewId()
        {
            return new EntityId(Guid.NewGuid(), DateTime.UtcNow)
            {
                IsNew = true
            };
        }

        public void IsNowPersisted()
        {
            IsNew = false;
        }
    }
}
