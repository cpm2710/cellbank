﻿using jinlinService;
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

            return points;

        }

        public System.Collections.Generic.List<jinlinModel.Antennule> GetAntennules(System.Collections.Generic.List<jinlinModel.Point> points)
        {
            throw new NotImplementedException();
        }
    }
}
