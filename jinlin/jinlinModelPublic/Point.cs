using jinlinModelPublic;
using System;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;

namespace jinlinModel
{
    [DataContract]
    public class Point
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public JinLinGeoCoordinate GeoCoordinate { get; set; }
    }
}
