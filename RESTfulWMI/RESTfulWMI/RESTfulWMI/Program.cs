using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Instrumentation;

namespace RESTfulWMI
{
    class Program
    {
        public static void Main(string[] args)
        {
            new SBS9User("andy", "andypswd", "andliu@microsoft.com");
            InstrumentationManager.RegisterType(typeof(SBS9User));
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
            InstrumentationManager.UnregisterType(typeof(SBS9User));

        }
    }
}
