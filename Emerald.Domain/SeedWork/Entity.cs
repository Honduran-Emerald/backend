using Emerald.Domain.SeedWork;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vitamin.Value.Domain.SeedWork
{
    public class Entity : GenericEntity<ObjectId>
    {
        public Entity(ObjectId id)
            : base(id)
        {
        }

        public Entity()
            : base(ObjectId.GenerateNewId())
        {
        }
    }
}
