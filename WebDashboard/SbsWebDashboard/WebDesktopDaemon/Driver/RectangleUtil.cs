using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MirrSharp.Driver;

namespace MirrSharp.Driver
{
    public class RectangleUtil
    {
        public static List<System.Drawing.Rectangle> getRectangle(ChangesRecord[] pointrect,long from,uint to)
        {
            Console.WriteLine("numbers:" + (to-from));
            List<System.Drawing.Rectangle> toReturn = new List<System.Drawing.Rectangle>();
            System.Drawing.Rectangle tempRect = new System.Drawing.Rectangle();
            for (long j = from; j < to; j++)
            {
                ChangesRecord cr = pointrect[j];
                if (cr.rect.x1 == cr.rect.x2 || cr.rect.y1 == cr.rect.y2)
                {
                    continue;
                }
                {
                    Console.Write("rec:" + cr.rect.x1 + "   " + cr.rect.y1);
                    Console.WriteLine("" + cr.rect.x2 + "   " + cr.rect.y2);

                    tempRect.X = cr.rect.x1;
                    tempRect.Y = cr.rect.y1;
                    tempRect.Width = (cr.rect.x2 - cr.rect.x1);
                    tempRect.Height = (cr.rect.y2 - cr.rect.y1);
                    bool intersect = false;
                    for (int i = 0; i < toReturn.Count; i++)
                    {
                        System.Drawing.Rectangle rect = toReturn[i];
                        if (rect.IntersectsWith(tempRect))
                        {
                            rect = System.Drawing.Rectangle.Union(rect, tempRect);
                            intersect = true;
                        }
                    } if (!intersect)
                    {

                        System.Drawing.Rectangle toAdd =
                            new System.Drawing.Rectangle(tempRect.X, tempRect.Y, tempRect.Width, tempRect.Height);
                        toReturn.Add(toAdd);
                    }
                }
            }
            //foreach (ChangesRecord cr in pointrect)
            
            return toReturn;
        }
    }
}
