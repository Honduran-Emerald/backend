using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Models.Quest.Component
{
    public class TextComponentModel : ComponentModel
    {
        public string Text { get; set; }

        public TextComponentModel(string text) : base(ComponentType.Text)
        {
            Text = text;
        }
    }
}
