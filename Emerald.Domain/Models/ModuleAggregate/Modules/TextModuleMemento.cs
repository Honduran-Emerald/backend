using Emerald.Domain.Models.TrackerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.ModuleAggregate.Modules
{
    public class TextModuleMemento : TrackerNodeMemento
    {
        public string Text { get; set; }

        public TextModuleMemento()
        {
            Text = default!;
        }

        public TextModuleMemento(string text)
        {
            Text = text;
        }
    }
}
