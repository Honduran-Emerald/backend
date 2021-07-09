using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.ModuleMemento
{
    public class TextMementoModel : MementoModel
    {
        public string Text { get; set; }

        public TextMementoModel(string text)
            : base(MementoType.Choice)
        {
            Text = text;
        }
    }
}
