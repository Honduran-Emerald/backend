using Emerald.Domain.Models.TrackerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.ModuleAggregate.Modules
{
    public class ChoiceModuleMemento : TrackerPathMemento
    {
        public int Choice { get; }

        public ChoiceModuleMemento(int choice)
        {
            Choice = choice;
        }

        public ChoiceModuleMemento() : base()
        {
        }
    }
}
