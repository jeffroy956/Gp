using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Common
{
    public class Entity
    {
        public Entity(EntityId id, TimeStamp timeStamp)
        {
            _id = id;
            _timeStamp = timeStamp;
        }

        private EntityId _id;
        private TimeStamp _timeStamp;

        public Guid Id
        {
            get
            {
                return _id.Id;
            }
        }

        public TimeStamp TimeStamp
        {
            get
            {
                return _timeStamp;
            }
        }

        public void UpdateTimeStamp()
        {
            _timeStamp.Update();
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
