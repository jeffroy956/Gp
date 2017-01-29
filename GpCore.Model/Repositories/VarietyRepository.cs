using GpCore.Model.Common;
using GpCore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Repositories
{
    public interface VarietyRepository
    {
        Variety Get(EntityId id);

        void Save(Variety variety);
    }
}
