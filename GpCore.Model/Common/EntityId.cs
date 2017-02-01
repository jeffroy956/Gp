using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Common
{
    public class EntityId
    {
        private EntityId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public bool IsNew { get; private set; }

        public static EntityId ForNewEntity()
        {
            return new EntityId(Guid.NewGuid())
            {
                IsNew = true
            };
        }

        public void AcceptChanges()
        {
            IsNew = false;
        }

        public static EntityId ForExistingEntity(Guid guid)
        {
            return new EntityId(guid);
        }
    }
}
