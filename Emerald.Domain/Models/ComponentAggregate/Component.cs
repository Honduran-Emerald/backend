using MongoDB.Bson.Serialization.Attributes;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.ComponentAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(
        typeof(TextComponent),
        typeof(ImageComponent),
        typeof(AnswerComponent))]
    public class Component : Entity
    {
    }
}
