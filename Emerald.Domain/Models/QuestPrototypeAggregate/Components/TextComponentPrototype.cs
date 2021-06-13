using Emerald.Domain.Models.ComponentAggregate;
using System.Collections.Generic;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestPrototypeAggregate.Components
{
    public class TextComponentPrototype : ComponentPrototype
    {
        public string Text { get; set; }

        public TextComponentPrototype(string text)
        {
            Text = text;
        }

        private TextComponentPrototype()
        {
            Text = default!;
        }

        public override Component ConvertToComponent(IPrototypeContext context)
            => new TextComponent(Text);

        public override void Verify()
        {
            if (Text.Length == 0)
            {
                throw new DomainException("TextComponent text can not be empty");
            }
        }

        public override void AggregateImageReferences(List<int> imageReferences)
        {
        }
    }
}
