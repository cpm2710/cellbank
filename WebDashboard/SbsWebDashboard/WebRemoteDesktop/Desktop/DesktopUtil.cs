using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Dashboard365Service
{
    class DesktopUtil
    {
        public static Bitmap getDesktopBitmap()
        {
            IntPtr dc1 = DesktopNativeMethod.CreateDC("DISPLAY", null, null, IntPtr.Zero);
            //创建显示器的DC
            Graphics g1 = Graphics.FromHdc(dc1);
            //由一个指定设备的句柄创建一个新的Graphics对象
            Bitmap MyImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, g1);
            return MyImage;
        }
        public static string getDesktopInBase64()
        {
            System.IO.MemoryStream m = new System.IO.MemoryStream();
            System.Drawing.Bitmap bp = getDesktopBitmap();//new System.Drawing.Bitmap(@“c:/demo.GIF”);
            bp.Save(m, System.Drawing.Imaging.ImageFormat.Gif);
            byte[]b= m.GetBuffer();
            string base64string=Convert.ToBase64String(b);
            return base64string;
        }
    }
}
