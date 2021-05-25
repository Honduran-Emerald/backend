using Emerald.Domain.Models.TrackerAggregate;

namespace Emerald.Domain.Models.ModuleAggregate.Modules
{
    public class ChoiceModuleMemento : TrackerNodeMemento
    {
        public int Choice { get; private set; }

        public ChoiceModuleMemento(int choice)
        {
            Choice = choice;
        }

        public ChoiceModuleMemento() : base()
        {
        }
    }
}
