using Syncfusion.SfMaps.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SfMaps_Geolocation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeocodingPage : ContentPage
    {
        public GeocodingPage()
        {
            InitializeComponent();
            GetGeocodingLocation();
        }

        private async void GetGeocodingLocation()
        {
            try
            {
                var address = "Microsoft Building 25 Redmond WA USA";
                var locations = await Geocoding.GetLocationsAsync(address);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    SetMarkerLocation(location);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }
        }
        private void SetMarkerLocation(Location location)
        {
            MapMarker marker = new MapMarker();
            marker.Latitude = location.Latitude.ToString();
            marker.Longitude = location.Longitude.ToString();
            this.imageryLayer.Markers = new ObservableCollection<MapMarker> { marker };

            this.imageryLayer.GeoCoordinates = new Point(location.Latitude, location.Longitude);
            this.imageryLayer.Radius = 50;
            this.imageryLayer.DistanceType = DistanceType.KiloMeter;
        }
    }
}
