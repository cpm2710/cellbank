using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FunctionPointService
{
    public class LocationSingleton
    {
        private static volatile LocationSingleton instance;
        private static object syncObject = new object();
        private UserLocationList userLocations;

        private Dictionary<string, UserLocation> userLocationDictionary;

        public Dictionary<string, UserLocation> UserLocationDictionary
        {
            get { return userLocationDictionary; }
            set { userLocationDictionary = value; }
        }

        public UserLocationList UserLocations
        {
            get { return userLocations; }
            set { userLocations = value; }
        }
        private MachineLocationList machineLocations;

        private Dictionary<string, MachineLocation> machineLocationDictionary;

        public Dictionary<string, MachineLocation> MachineLocationDictionary
        {
            get { return machineLocationDictionary; }
            set { machineLocationDictionary = value; }
        }

        public MachineLocationList MachineLocations
        {
            get { return machineLocations; }
            set { machineLocations = value; }
        }

        private LocationSingleton()
        {
            userLocations = new UserLocationList();
        }
        public void AddOrUpdateMachineLocation(MachineLocation machineLocation)
        {
            MachineLocation gotMachineLocatoin = null;
            machineLocationDictionary.TryGetValue(machineLocation.MachineName, out gotMachineLocatoin);
            if (gotMachineLocatoin != null)
            {
                gotMachineLocatoin.Latitude = machineLocation.Latitude;
                gotMachineLocatoin.Longitude = machineLocation.Longitude;
            }
            else
            {
                machineLocations.Add(machineLocation);
                machineLocationDictionary.Add(machineLocation.MachineName, machineLocation);
            }
        }
        public void AddOrUpdateUserLocation(UserLocation userLocation)
        {
            UserLocation gotUserLocation=null;
            userLocationDictionary.TryGetValue(userLocation.UserName, out gotUserLocation);
            if (userLocation != null)
            {
                gotUserLocation.Latitude = userLocation.Latitude;
                gotUserLocation.Longitude = userLocation.Longitude;
            }
            else
            {
                userLocations.Add(userLocation);
                userLocationDictionary.Add(userLocation.UserName, userLocation);
            }
        }
        public void removeUserLocation(string userName)
        {
            UserLocation userLocation=null;
            userLocationDictionary.TryGetValue(userName, out userLocation);
            if (userLocation != null)
            {
                userLocationDictionary.Remove(userName);
                userLocations.Remove(userLocation);
            }            
        }
        public static LocationSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncObject)
                    {
                        instance = new LocationSingleton();
                    }
                }
                return instance;
            }
        }
    }
}