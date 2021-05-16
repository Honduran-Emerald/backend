namespace Emerald.Application.Models.Quest.ModuleMemento
{
    public class MementoModel
    {
        public MementoType Type { get; set; }

        public MementoModel(MementoType type)
        {
            Type = type;
        }
    }
}
