using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Common
{
    public class EntityId
    {
        private EntityId(Guid id, DateTime createDate)
        {
            Id = id;
            CreateDate = createDate;
        }

        public Guid Id { get; private set; }
        public DateTime CreateDate { get; private set; }

        public bool IsNew { get; private set; }

        public static EntityId ForNewEntity()
        {
            return new EntityId(Guid.NewGuid(), DateTime.UtcNow)
            {
                IsNew = true
            };
        }

        public void AcceptChanges()
        {
            IsNew = false;
        }

        public static EntityId ForExistingEntity(Guid guid, DateTime utcNow)
        {
            return new EntityId(guid, utcNow);
        }
    }
}
