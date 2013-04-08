// author: andyliuliming@outlook.com

using RotorsGui;
using RotorsWorkFlow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotors
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                App app = new App();

                app.MainWindow = new MainWindow();
                app.MainWindow.Show();
                app.Run();
            }
            else
            {
                // now we came into command line mode.
                if (args[0] == "/?" || args[0] == "/h")
                {

                }
            }
        }

        static void ParseParameter(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "":
                        break;
                    default:
                        break;
                }
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine("");
        }
    }
}
