using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Component
{
    public class ImageComponentModel : ComponentModel
    {
        public string ImageId { get; set; }

        public ImageComponentModel()
        {
            ImageId = default!;
        }

        public ImageComponentModel(string componentId, string imageId) : base(componentId, ComponentType.Image)
        {
            ImageId = imageId;
        }
    }
}
