using System;
using System.Collections.Generic;
using System.Text;

namespace Emerald.Domain.Models.ComponentAggregate
{
    public class ImageComponent : Component
    {
        public string Filename { get; set; }

        public ImageComponent(string filename)
        {
            Filename = filename;
        }
    }
}
