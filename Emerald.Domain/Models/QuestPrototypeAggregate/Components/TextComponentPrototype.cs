using Emerald.Domain.Models.ComponentAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override Component ConvertToComponent()
            => new TextComponent(Text);
    }
}
