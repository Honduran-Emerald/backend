using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Infrastructure
{
    public interface IMongoDbContext
    {
        IMongoDatabase Emerald { get; }
    }
}
