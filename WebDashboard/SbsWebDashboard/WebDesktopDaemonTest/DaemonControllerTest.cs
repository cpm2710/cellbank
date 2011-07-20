using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Json;
using WebDesktopDaemon;
using System.Threading;

namespace WebDesktopDaemonTest
{
    class DaemonControllerTest
    {
        public void Test()
        {
            WebDesktopTCPClient client = new WebDesktopTCPClient("andy-PC", 3390, 3391);
            ControlCommand cc = new ControlCommand();
            cc.CommandType = CommandType.mouse;
            cc.MouseCommandType = MouseCommandType.move;
            cc.x = 0;
            cc.y = 0;
            client.SendControl(cc);

            Thread.Sleep(5000);

            cc = new ControlCommand();
            cc.CommandType = CommandType.mouse;
            cc.MouseCommandType = MouseCommandType.leftdown;
            cc.x = 0;
            cc.y = 0;
            client.SendControl(cc);

            cc = new ControlCommand();
            cc.CommandType = CommandType.mouse;
            cc.MouseCommandType = MouseCommandType.leftup;
            cc.x = 0;
            cc.y = 0;
            client.SendControl(cc);


            cc = new ControlCommand();
            cc.CommandType = CommandType.mouse;
            cc.MouseCommandType = MouseCommandType.move;
            cc.x = 0.5;
            cc.y = 0.5;
            client.SendControl(cc);

            Thread.Sleep(5000);
            cc = new ControlCommand();
            cc.CommandType = CommandType.mouse;
            cc.MouseCommandType = MouseCommandType.rightdown;
            cc.x = 0.5;
            cc.y = 0.5;
            client.SendControl(cc);

            cc = new ControlCommand();
            cc.CommandType = CommandType.mouse;
            cc.MouseCommandType = MouseCommandType.rightup;
            cc.x = 0.5;
            cc.y = 0.5;
            client.SendControl(cc);
        }
    }
}
