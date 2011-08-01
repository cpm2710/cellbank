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
              // List<Bitmap> changes= new List<Bitmap>();
              //  changes.Add(_mirror.GetScreen());
               foreach (Bitmap change in changes)
               {
                   System.IO.MemoryStream m = new System.IO.MemoryStream();
                   change.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                   change.Save("d:\\abc.png", System.Drawing.Imaging.ImageFormat.Png);
               }
                //if(changes.Count>0)
               //changes[0].Save("d:\\abc.png", System.Drawing.Imaging.ImageFormat.Png);
               //Console.WriteLine("changes count:"+changes.Count);
                
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
