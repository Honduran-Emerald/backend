using Emerald.Domain.Models.ComponentAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Components
{
    public class ImageComponentPrototype : ComponentPrototype
    {
        public string ImageId { get; private set; }

        private ImageComponentPrototype()
        {
            ImageId = default!;
        }

        public override Component ConvertToComponent()
            => new ImageComponent(ImageId);
    }
}
