using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpCore.Model.Common
{
    public class InvalidSystemStateException : Exception
    {
        public InvalidSystemStateException(string message) : base(message)
        {

        }

    }
}
