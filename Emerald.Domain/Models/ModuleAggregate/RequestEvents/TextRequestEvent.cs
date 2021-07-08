using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.ModuleAggregate.RequestEvents
{
    public class TextRequestEvent : RequestEvent
    {
        public string Text { get; set; }

        public TextRequestEvent(string text)
        {
            Text = text;
        }
    }
}
