using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.ComponentAggregate
{
    public class AnswerComponent : Component
    {
        public string Text { get; set; }

        public AnswerComponent(string text)
        {
            Text = text;
        }

        private AnswerComponent()
        {
            Text = default!;
        }
    }
}
