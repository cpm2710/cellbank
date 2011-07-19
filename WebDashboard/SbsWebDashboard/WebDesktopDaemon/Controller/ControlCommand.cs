using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WebDesktopDaemon
{
    public enum MouseCommandType{
        click=0,
        move=1,
        up=2,
        dbclick=3
    }
    public enum CommandType{
        mouse=0,
        keyboard=1
    }
    [DataContract]
    public class ControlCommand
    {
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
