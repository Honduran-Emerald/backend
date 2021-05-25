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
        public string Filename { get; }

        private ImageComponentPrototype()
        {
            Filename = default!;
        }

        public override Component ConvertToComponent()
            => new ImageComponent(Filename);
    }
}
