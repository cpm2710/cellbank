using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WebDesktopDaemon
{
    public enum MouseCommandType
    {
        move = 0,
        leftdown = 1,        
        leftup = 2,
        rightdown=3,
        rightup=4,
        dbclick = 5
    }
    public enum CommandType{
        mouse=0,
        keyboard=1
    }
    [DataContract]
    public class ControlCommand
    {
        [DataMember]
        public string MachineName;
        [DataMember]
        public CommandType CommandType;
        [DataMember]
        public string Content;
        [DataMember]
        public double x;
        [DataMember]
        public double y;
        [DataMember]
        public MouseCommandType MouseCommandType;
    }
}
