using System;
using System.Globalization;
using System.Windows.Data;
using Model;
using GeoCoordinate = System.Device.Location.GeoCoordinate;

namespace CodeCampWP7.Converters
{
    public class LocationToGeoCoordinateConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var location = (Location) value;
            return new GeoCoordinate(location.Latitude, location.Longitude);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
