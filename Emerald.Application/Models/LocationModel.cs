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

        public LocationModel()
        {
        }
    }
}
