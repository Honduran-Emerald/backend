namespace Emerald.Application.Models.Quest.ModuleMemento
{
    public abstract class MementoModel
    {
        public MementoType Type { get; set; }

        public MementoModel(MementoType type)
        {
            Type = type;
        }

        public MementoModel()
        {
            Type = default!;
        }
    }
}
