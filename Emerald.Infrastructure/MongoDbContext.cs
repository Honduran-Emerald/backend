using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure
{
    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase emerald;

        public MongoDbContext(IConfiguration configuration)
        {
            IMongoClient mongoClient = new MongoClient(configuration["Mongo:ConnectionString"]);
            emerald = mongoClient.GetDatabase(configuration["Mongo:DatabaseName"]);
        }

        public IMongoDatabase Emerald => emerald;
    }
}
