using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsLib.Exceptions
{
    public class RotorsException : Exception
    {
        public RotorsException(string message)
            : base(message)
        {

        }
    }
}
