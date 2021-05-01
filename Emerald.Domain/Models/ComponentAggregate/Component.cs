using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.ComponentAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(
        typeof(TextComponent), 
        typeof(LocationComponent),
        typeof(ImageComponent))]
    public class Component : Entity
    {
        public override ObjectId Id { get; protected set; }
    }
}
