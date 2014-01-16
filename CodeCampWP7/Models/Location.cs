using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Device.Location;

namespace CodeCamp.Models
{
    public class Location
    {
        public string City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public GeoCoordinate Coorinates { get { return new GeoCoordinate(Latitude, Longitude); } }
    }
}
