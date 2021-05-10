using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.ModuleAggregate.Modules;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.ModuleAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(
        typeof(ChoiceModule))]
    public abstract class Module : Entity
    {
        public override ObjectId Id { get; protected set; }

        public List<ObjectId> ComponentIds { get; private set; }
        public string Title { get; private set; }
        
        public Module(string title)
            : this()
        {
            Title = title;
        }

        private Module()
        {
            ComponentIds = new List<ObjectId>();
        }

        public abstract ResponseEvent ProcessEvent(RequestEvent requestEvent);

        public void SetTitle(string title)
        {
            Title = title;
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
