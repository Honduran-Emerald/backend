using Emerald.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Domain.Models.LockAggregate
{
    public interface ILockableEntity<T> : IEntity<T>
    {
        List<Lock> Locks { get; set; }
    }

    public static class LocalableEntityExtensions
    {
        public static bool IsLocked<T>(this ILockableEntity<T> entity)
            => entity.Locks.Count > 0;
    }
}
