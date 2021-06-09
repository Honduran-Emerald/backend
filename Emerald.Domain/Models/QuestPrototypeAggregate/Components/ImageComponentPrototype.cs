using Emerald.Domain.Models.ComponentAggregate;
using System;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Components
{
    public class ImageComponentPrototype : ComponentPrototype
    {
        public string ImageId { get; set; }

        private ImageComponentPrototype()
        {
            ImageId = default!;
        }

        public override Component ConvertToComponent()
            => new ImageComponent(ImageId);

        public override void Verify()
        {
            throw new NotImplementedException();
        }
    }
}
