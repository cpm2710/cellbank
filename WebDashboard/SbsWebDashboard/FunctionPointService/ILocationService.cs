using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace FunctionPointService
{
    [DataContract]
    public class MachineLocation
    {
        [DataMember]
        public string MachineName;
        [DataMember]
        public string Latitude;
        [DataMember]
        public string Longitude;
    }
   [CollectionDataContract(Name = "MachineLocations", Namespace = "")]
    public class MachineLocationList: List<MachineLocation>
    {

    }
    [DataContract]
    public class UserLocation
    {
        [DataMember]
        public string UserName;
        [DataMember]
        public double Latitude;
        [DataMember]
        public double Longitude;
    }
    
    [CollectionDataContract(Name = "UserLocations", Namespace = "")]
    public class UserLocationList : List<UserLocation>
    {

    }
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILocationService" in both code and config file together.
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/userlocations", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        UserLocation reportUserLocation(UserLocation UserLocation);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/machinelocations", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        MachineLocation reportMachineLocation(MachineLocation MachineLocation);

        [OperationContract]
        [WebGet(UriTemplate = "/machinelocations", ResponseFormat = WebMessageFormat.Json)]
        MachineLocationList getMachineLocations();

        [OperationContract]
        [WebGet(UriTemplate = "/userlocations", ResponseFormat = WebMessageFormat.Json)]
        UserLocationList getUserLocations();

    }
}
