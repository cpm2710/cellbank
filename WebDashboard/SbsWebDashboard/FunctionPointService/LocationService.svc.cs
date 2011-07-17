using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;

namespace FunctionPointService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LocationService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class LocationService : ILocationService
    {
        public UserLocation reportUserLocation(UserLocation UserLocation)
        {
            LocationSingleton.Instance.AddOrUpdateUserLocation(UserLocation);
            return UserLocation;
        }
        public MachineLocation reportMachineLocation(MachineLocation MachineLocation)
        {
            return MachineLocation;
        }
        public UserLocationList getUserLocations()
        {
            return LocationSingleton.Instance.UserLocations;
        }
        public MachineLocationList getMachineLocations()
        {
            return LocationSingleton.Instance.MachineLocations;
        }
    }
}
