using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models
{
    public class LocationModel
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public LocationModel(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
