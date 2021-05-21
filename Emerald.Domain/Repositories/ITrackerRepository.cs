﻿using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public interface ITrackerRepository
    {
        Task Add(Tracker tracker);
        Task Update(Tracker tracker);

        Task<Tracker> Get(ObjectId id);
    }
}
