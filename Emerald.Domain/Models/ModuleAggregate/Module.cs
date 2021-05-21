using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.ModuleAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(
        typeof(ChoiceModule))]
    public abstract class Module : Entity
    {
        public List<ObjectId> ComponentIds { get; private set; }
        public string Objective { get; private set; }

        public Module(string objective)
            : this()
        {
            Objective = objective;
        }

        public Module()
        {
            ComponentIds = new List<ObjectId>();
        }

        public abstract ResponseEventCollection ProcessEvent(TrackerNodeMemento memento, RequestEvent requestEvent);

        public void SetObjective(string objective)
        {
            Objective = objective;
        }

        public void AddComponent(Component component)
        {
            if (ComponentIds.Contains(component.Id))
            {
                throw new DomainException("Can not add already existing component");
            }

            ComponentIds.Add(component.Id);
        }

        public void RemoveComponent(Component component)
        {
            if (ComponentIds.Contains(component.Id) == false)
            {
                throw new DomainException("Can not remove missing component");
            }

            ComponentIds.Remove(component.Id);
        }
    }
}
