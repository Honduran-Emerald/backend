using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.ComponentAggregate
{
    public class Component : Entity
    {
        public override ObjectId Id { get; protected set; }
    }
}
