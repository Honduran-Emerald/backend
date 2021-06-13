using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.QuestPrototypeAggregate.Components;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Emerald.Domain.Models.QuestPrototypeAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(
        typeof(ImageComponentPrototype),
        typeof(TextComponentPrototype),
        typeof(AnswerComponentPrototype))]
    public abstract class ComponentPrototype
    {
        public abstract Component ConvertToComponent(IPrototypeContext context);
        public abstract void Verify();
        public abstract void AggregateImageReferences(List<int> imageReferences);
    }
}
