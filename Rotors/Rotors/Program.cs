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


            App app = new App();

            app.MainWindow = new MainWindow();
            app.MainWindow.Show();
            app.Run();
            
        }
    }
}
