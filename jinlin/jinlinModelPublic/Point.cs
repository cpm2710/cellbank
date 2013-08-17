using jinlinModelPublic;
using System;
using System.ComponentModel;
using System.Device.Location;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;

namespace jinlinModel
{
    [DataContract]
    public class Point
    {
        public Point()
        {
            this.Visibility = true;
        }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public bool Visibility { get; set; }

        //[TypeConverter(typeof(GeoCoordinateConverter))]
        public GeoCoordinate GeoCoordinate
        {
            get
            {
                return new GeoCoordinate(this.JinLinGeoCoordinate.Latitude, this.JinLinGeoCoordinate.Longitude);
            }
        }

        [DataMember]
        //[TypeConverter(typeof(GeoCoordinateConverter))]
        public JinLinGeoCoordinate JinLinGeoCoordinate { get; set; }
    }
}
