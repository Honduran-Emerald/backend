using Emerald.Domain.Models.ComponentAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.QuestionVersionAggregate
{
    public class Module : Entity
    {
        public override ObjectId Id { get; protected set; }

        public List<ObjectId> ComponentIds { get; private set; }
    }
}
