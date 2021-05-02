using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emerald.Domain.Models.TrackerAggregate
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes()]
    public abstract class TrackerPathMemento
    {

    }
}
