namespace Emerald.Domain.Models.ModuleAggregate.RequestEvents
{
    public class ChoiceRequestEvent : RequestEvent
    {
        public int Choice { get; private set; }

        public ChoiceRequestEvent(int choice)
        {
            Choice = choice;
        }
    }
}
