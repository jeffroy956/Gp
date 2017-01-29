using GpCore.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Repositories
{
    public interface VarietyRepository
    {
        Variety Get(Guid varietyId);

        void Insert(Variety variety);
    }
}
