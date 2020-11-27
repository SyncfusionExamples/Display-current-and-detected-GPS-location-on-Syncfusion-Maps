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
    public partial class GeolocationPage : ContentPage
    {
        Location geoLocation = null;
        public GeolocationPage()
        {
            InitializeComponent();
            GetCurrentGeolocation();
            SetMarkerInLocation(geoLocation);
        }

        private async void GetCurrentGeolocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    location = await Geolocation.GetLocationAsync(request);
                    if (location != null)
                    {
                        geoLocation = location;
                    }
                }
                else
                {
                    geoLocation = location;
                }
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
        private void SetMarkerInLocation(Location location)
        {
            if (location != null)
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
}