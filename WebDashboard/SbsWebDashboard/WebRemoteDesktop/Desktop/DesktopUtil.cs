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
        public static Image getDesktopBitmap()
        {
            IntPtr screenDC = DesktopNativeMethod.CreateDC("DISPLAY", null, null, IntPtr.Zero);
            Graphics screenGraph = Graphics.FromHdc(screenDC);

            //以屏幕Ghraph为基础，生成位图
            Image outputImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, screenGraph);

            //获取与位图关联的Graph，并基于此获得位图的DC。
            Graphics imgGraph = Graphics.FromImage(outputImage);
            IntPtr imgDC = imgGraph.GetHdc();

            //使用Win32 API "灌图"
            DesktopNativeMethod.BitBlt(imgDC, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, screenDC, 0, 0, 0xCC0020);

            //保存位图
            imgGraph.ReleaseHdc(imgDC);
            return outputImage;
            //IntPtr dc1 = DesktopNativeMethod.CreateDC("DISPLAY", null, null, IntPtr.Zero);
            ////创建显示器的DC
            //Graphics g1 = Graphics.FromHdc(dc1);
            ////由一个指定设备的句柄创建一个新的Graphics对象
            //Bitmap MyImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, g1);
            //return MyImage;
        }
        public static string getDesktopInBase64()
        {
            System.IO.MemoryStream m = new System.IO.MemoryStream();
            System.Drawing.Image bp = getDesktopBitmap();//new System.Drawing.Bitmap(@“c:/demo.GIF”);
            bp.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            bp.Save("d:\\abc.png", System.Drawing.Imaging.ImageFormat.Png);
            byte[]b= m.GetBuffer();
            string base64string=Convert.ToBase64String(b);
            return base64string;
        }
    }
}
