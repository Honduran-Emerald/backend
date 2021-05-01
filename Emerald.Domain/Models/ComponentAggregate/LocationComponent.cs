using System;
using System.Collections.Generic;
using System.Text;

namespace Emerald.Domain.Models.ComponentAggregate
{
    public class LocationComponent : Component
    {
        public Location Location { get; set; }

        public LocationComponent(Location location)
        {
            Location = location;
        }
    }
}
