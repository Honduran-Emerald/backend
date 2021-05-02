using Emerald.Domain.Models.TrackerAggregate;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emerald.Infrastructure.Repositories
{
    public class TrackerRepository : ITrackerRepository
    {
        private IMongoCollection<Tracker> collection;

        public TrackerRepository(IMongoDbContext dbContext)
        {
            collection = dbContext.Emerald.GetCollection<Tracker>("Trackers");
        }


    }
}
