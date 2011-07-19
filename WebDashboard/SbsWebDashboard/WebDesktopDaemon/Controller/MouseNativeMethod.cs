using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebDesktopDaemon
{
    class MouseNativeMethod
    {
        private const int MAX=65535;
        public static void MoveTo(double x, double y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (int)(x * MAX), (int)(y * MAX),0,0);
        }
        public static void MoveTo(int dx, int dy)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, dx, dy, 0, 0);
        }
        public static void LeftClick(double x, double y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE|MOUSEEVENTF_LEFTDOWN ,(int)(x*MAX), (int)(y*MAX), 0, 0);
        }
        public static void LeftClick(int dx,int dy){
            mouse_event(MOUSEEVENTF_ABSOLUTE|MOUSEEVENTF_LEFTDOWN, dx, dy, 0, 0);
        }
        public static void ClickUp(int dx, int dy)
        {
            mouse_event(MOUSEEVENTF_LEFTUP, dx, dy, 0, 0); 
        }
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        const int MOUSEEVENTF_MOVE = 0x0001;     // 移动鼠标

        const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下

        const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起

        const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下

        const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起

        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;// 模拟鼠标中键按下

        const int MOUSEEVENTF_MIDDLEUP = 0x0040;// 模拟鼠标中键抬起

        const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标
 
    }
}
