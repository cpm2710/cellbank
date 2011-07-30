using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using WebDesktopDaemon;

namespace FunctionPointService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRemoteDesktopService" in both code and config file together.
    
    [ServiceContract]
    public interface IRemoteDesktopService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/desktops/{MachineName}", ResponseFormat = WebMessageFormat.Json)]
        DesktopSnapshot getDesktopSnapshot(string MachineName);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/desktops", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        DesktopSnapshot sendControlCommand(ControlCommand ControlCommand);
        
    }
}
