using MongoDB.Bson.Serialization.Attributes;

namespace Emerald.Domain.Models.TrackerAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes()]
    public abstract class TrackerPathMemento
    {
    }
}
