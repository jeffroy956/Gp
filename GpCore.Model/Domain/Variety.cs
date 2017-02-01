using GpCore.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpCore.Model.Domain
{
    public class Variety : Entity
    {

        public Variety(string name): this(EntityId.ForNewEntity(), TimeStamp.ForNewRecord(), name)
        {

        }
        public Variety(EntityId id, TimeStamp timeStamp, string name): base(id, timeStamp)
        {
            Name = name;
        }
        public string Name { get; private set; }

        public void Rename(string newName)
        {
            Name = newName;
        }

    }
}
