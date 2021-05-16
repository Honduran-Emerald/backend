using Emerald.Domain.Models.TrackerAggregate;

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
