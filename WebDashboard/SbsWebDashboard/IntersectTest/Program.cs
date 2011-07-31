using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IntersectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Drawing.Rectangle REC = new System.Drawing.Rectangle(0, 0, 100, 100);
            System.Drawing.Rectangle REC2 = new System.Drawing.Rectangle(50, 50, 100, 100);
            Rectangle rr3=Rectangle.Union(REC, REC2);
            Console.WriteLine(rr3.X + " " + rr3.Y + " " + rr3.Width + " " + rr3.Height);
        }
    }
}
