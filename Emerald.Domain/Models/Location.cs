using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models
{
    public class Location : ValueObject
    {
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public double Distance(Location location)
            => new GeoCoordinate(Latitude, Longitude).GetDistanceTo(
               new GeoCoordinate(location.Latitude, location.Longitude));

        public override bool Equals(object obj)
            => obj is Location location
            && location.Distance(this) < 5;
    }
}
