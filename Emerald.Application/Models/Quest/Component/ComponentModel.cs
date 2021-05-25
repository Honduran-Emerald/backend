namespace Emerald.Application.Models.Quest.Component
{
    public abstract class ComponentModel
    {
        public string ComponentId { get; set; }
        public ComponentType ComponentType { get; set; }

        public ComponentModel(string componentId, ComponentType componentType)
        {
            ComponentId = componentId;
            ComponentType = componentType;
        }

        public ComponentModel()
        {
            ComponentId = default!;
            ComponentType = default!;
        }
    }
}
