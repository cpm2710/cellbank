using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MouseController
{
    class Program
    {
        static void Main(string[] args)
        {
            MouseNativeMethod.MoveTo(30000, 400);
            MouseNativeMethod.LeftClick(500, 400);
        }
    }
}
