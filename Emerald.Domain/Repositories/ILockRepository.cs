using Emerald.Domain.Models.LockAggregate;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Repositories
{
    public interface ILockRepository
    {
        Task<Lock> Get(ObjectId id);
        Task Add(Lock @lock);
        Task Remove(ObjectId id);
    }
}
