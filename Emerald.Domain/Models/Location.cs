using GeoCoordinatePortable;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models
{
    public class Location : ValueObject
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public double Distance(Location location)
            => new GeoCoordinate(Latitude, Longitude).GetDistanceTo(
               new GeoCoordinate(location.Latitude, location.Longitude));

        public override bool Equals(object? obj)
            => obj is Location location
            && location.Distance(this) < 5;
    }
}
