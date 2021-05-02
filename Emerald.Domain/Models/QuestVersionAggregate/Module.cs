using Emerald.Domain.Models.ComponentAggregate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestVersionAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes()]
    public abstract class Module : Entity
    {
        public override ObjectId Id { get; protected set; }

        public List<ObjectId> ComponentIds { get; private set; }
        public string Title { get; private set; }
        
        [BsonIgnore]
        public QuestVersion QuestVersion { get; set; }

        public Module(string title)
            : this()
        {
            Title = title;
        }

        private Module()
        {
            ComponentIds = new List<ObjectId>();
        }

        public void SetTitle(string title)
        {

        }
    }
}
