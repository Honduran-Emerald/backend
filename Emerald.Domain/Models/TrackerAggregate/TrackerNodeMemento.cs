using Emerald.Domain.Models.ModuleAggregate.Modules;
using MongoDB.Bson.Serialization.Attributes;

namespace Emerald.Domain.Models.TrackerAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(
        typeof(ChoiceModuleMemento))]
    public abstract class TrackerNodeMemento
    {
    }
}
