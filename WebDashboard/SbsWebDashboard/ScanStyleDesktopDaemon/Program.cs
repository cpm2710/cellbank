using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MirrSharp.Driver;
using System.Drawing;
namespace ScanStyleDesktopDaemon
{
    class Program
    {
        static void Main(string[] args)
        {
            DesktopMirror _mirror = new DesktopMirror();
            _mirror.Load();
            _mirror.Connect();
            long now = System.DateTime.Now.Ticks / 10000;
            
            while (true)
            {
                now = System.DateTime.Now.Ticks / 10000;
               List<Bitmap> changes= _mirror.getDifference();
               
               Console.WriteLine(changes.Count);
                
                //Bitmap screen = _mirror.GetScreen();

                //System.IO.MemoryStream m = new System.IO.MemoryStream();

                //screen.Save(m, System.Drawing.Imaging.ImageFormat.Png);

                Console.WriteLine((System.DateTime.Now.Ticks / 10000 - now) + "ms");
            }
            
            _mirror.Disconnect();
            _mirror.Unload();
        }
    }
}
