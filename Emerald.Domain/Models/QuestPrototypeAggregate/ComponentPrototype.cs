using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Domain.Models.QuestPrototypeAggregate.Components;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.QuestPrototypeAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(
        typeof(ImageComponentPrototype),
        typeof(TextComponentPrototype),
        typeof(AnswerComponentPrototype))]
    public abstract class ComponentPrototype
    {
        public abstract Component ConvertToComponent();
        public abstract void Verify();
    }
}
