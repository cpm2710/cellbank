using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using jinlinService;
using mockDataGenerator;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Maps.Controls;

namespace jinlin.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.MapExtensionsSetup(this.Map);
            //this.DataContext = this.mainViewModel;
            this.Loaded += Page_Loaded;
        }
        /// <summary>
        /// Setup the map extensions objects.
        /// All named objects inside the map extensions will have its references properly set
        /// </summary>
        /// <param name="map">The map that uses the map extensions</param>
        private void MapExtensionsSetup(Map map)
        {
            ObservableCollection<DependencyObject> children = MapExtensions.GetChildren(map);
            var runtimeFields = this.GetType().GetRuntimeFields();

            foreach (DependencyObject i in children)
            {
                var info = i.GetType().GetProperty("Name");

                if (info != null)
                {
                    string name = (string)info.GetValue(i);

                    if (name != null)
                    {
                        foreach (FieldInfo j in runtimeFields)
                        {
                            if (j.Name == name)
                            {
                                j.SetValue(this, i);
                                break;
                            }
                        }
                    }
                }
            }
        }

        ObservableCollection<jinlinModel.Point> Points = new ObservableCollection<jinlinModel.Point>();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IJinLinService jinLinService = new DataGenerator();
            Points.Clear();
            foreach (var a in jinLinService.GetPointsNearBy())
            {
                Points.Add(a);
            }
            
            this.StoresMapItemsControl.ItemsSource = Points;
        }

        /// <summary>
        /// Show the user location in the map
        /// </summary>
        /// <returns>Task that can used to await</returns>
        private async Task ShowUserLocation()
        {
            Geolocator geolocator;
            Geoposition geoposition;

            this.UserLocationMarker = (UserLocationMarker)this.FindName("UserLocationMarker");

            geolocator = new Geolocator();

            geoposition = await geolocator.GetGeopositionAsync();

            this.UserLocationMarker.GeoCoordinate = geoposition.Coordinate.ToGeoCoordinate();
            this.UserLocationMarker.Visibility = System.Windows.Visibility.Visible;
        }
        /// <summary>
        /// Zoom level to be used when showing the user location
        /// </summary>
        private readonly double userLocationMarkerZoomLevel = 16;

        /// <summary>
        /// Event handler for the Me button. It will show the user location marker and set the view on the map
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private async void OnMe(object sender, EventArgs e)
        {
            await this.ShowUserLocation();

            this.Map.SetView(this.UserLocationMarker.GeoCoordinate, this.userLocationMarkerZoomLevel);
        }

        

        private void ZoomUp(object sender, EventArgs e)
        {
            Map.ZoomLevel = Math.Min(Map.ZoomLevel + 1, 20);
        }

        private void ZoomDown(object sender, EventArgs e)
        {
            Map.ZoomLevel = Math.Max(Map.ZoomLevel - 1, 1);
        }

        private void mouseWheeled(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                
            }
        }

        private void pointClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int a = 0;
        }
    }
}