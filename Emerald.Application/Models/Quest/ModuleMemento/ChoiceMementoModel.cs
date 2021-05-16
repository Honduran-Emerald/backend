namespace Emerald.Application.Models.Quest.ModuleMemento
{
    public class ChoiceMementoModel : MementoModel
    {
        public int Choice { get; set; }

        public ChoiceMementoModel(int choice) : base(MementoType.Choice)
        {
            Choice = choice;
        }
    }
}
