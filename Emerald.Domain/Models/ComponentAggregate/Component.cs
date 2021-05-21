using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
    }
}
