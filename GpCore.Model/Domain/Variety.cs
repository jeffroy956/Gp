﻿using GpCore.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpCore.Model.Domain
{
    public class Variety : Entity
    {
        public Variety(EntityId id, string name): base(id)
        {
            Name = name;
        }
        public string Name { get; private set; }
        
    }
}
