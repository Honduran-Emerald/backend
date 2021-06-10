using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Domain.Models.LockAggregate
{
    public class LockableEntity : Entity, ILockableEntity
    {
        public List<Lock> Locks { get; set; }

        public LockableEntity()
        {
            Locks = new List<Lock>();
        }

        public LockableEntity(ObjectId id) : base(id)
        {
            Locks = new List<Lock>();
        }
    }
}
