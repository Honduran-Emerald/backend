using Emerald.Domain.Models.ModuleAggregate.Modules;
using MongoDB.Bson.Serialization.Attributes;

namespace Emerald.Domain.Models.TrackerAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(
        typeof(ChoiceModuleMemento),
        typeof(TextModuleMemento))]
    public abstract class TrackerNodeMemento
    {
    }
}
