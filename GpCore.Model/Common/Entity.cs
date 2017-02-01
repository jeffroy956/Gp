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
            _id = id;
        }

        private EntityId _id;

        public Guid Id
        {
            get
            {
                return Id;
            }
        }

        public bool IsNew
        {
            get
            {
                return _id.IsNew;
            }
        }
        
        public void AcceptChanges()
        {
            _id.AcceptChanges();
        }

    }
}
