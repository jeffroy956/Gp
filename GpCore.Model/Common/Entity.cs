using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Common
{
    public class Entity
    {
        public Entity(EntityId id)
        {
            Id = id;
        }

        public EntityId Id { get; private set; }

        public bool IsNew
        {
            get
            {
                return Id.IsNew;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return Id.CreateDate;
            }
        }

        public void AcceptChanges()
        {
            Id.AcceptChanges();
        }

    }
}
