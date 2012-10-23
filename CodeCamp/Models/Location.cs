using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace CodeCamp.Models
{
    public class Location
    {
        public string City { get; set; }
        public GeoCoordinate Coordinates { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
    }
}
