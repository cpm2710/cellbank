using jinlinService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace mockDataGenerator
{
    public class DataGenerator : IJinLinService
    {
        public System.Collections.Generic.List<jinlinModel.Point> GetPointsNearBy()
        {
            List<jinlinModel.Point> points = new List<jinlinModel.Point>();
            jinlinModel.Point p = new jinlinModel.Point();
            p.JinLinGeoCoordinate = new jinlinModelPublic.JinLinGeoCoordinate();
            p.JinLinGeoCoordinate.Latitude = 47.6050338745117;
            p.JinLinGeoCoordinate.Longitude = -122.334243774414;
            p.Name = "andy";
            points.Add(p);

            p = new jinlinModel.Point();
            p.JinLinGeoCoordinate = new jinlinModelPublic.JinLinGeoCoordinate();
            p.JinLinGeoCoordinate.Latitude = 47.6050338745117;
            p.JinLinGeoCoordinate.Longitude = -90.334243774414;
            p.Name = "andy2";
            points.Add(p);

            p = new jinlinModel.Point();
            p.JinLinGeoCoordinate = new jinlinModelPublic.JinLinGeoCoordinate();
            p.JinLinGeoCoordinate.Latitude = 47.6050338745117;
            p.JinLinGeoCoordinate.Longitude = -95.334243774414;
            p.Name = "andy3";
            points.Add(p);

            p = new jinlinModel.Point();
            p.JinLinGeoCoordinate = new jinlinModelPublic.JinLinGeoCoordinate();
            p.JinLinGeoCoordinate.Latitude = 47.6050338745117;
            p.JinLinGeoCoordinate.Longitude = -89.334243774414;
            p.Name = "andy4";
            points.Add(p);

            return points;
        }

        public System.Collections.Generic.List<jinlinModel.Antennule> GetAntennules(System.Collections.Generic.List<jinlinModel.Point> points)
        {
            throw new NotImplementedException();
        }
    }
}
