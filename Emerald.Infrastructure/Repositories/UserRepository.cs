using Emerald.Domain.Models.UserAggregate;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IMongoCollection<User> users;

        public UserRepository(IMongoDbContext dbContext)
        {
            users = dbContext.Emerald.GetCollection<User>("Users");
        }

        public async Task<User> Get(ObjectId id)
        {
            return await users
                .Find(user => user.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<List<User>> All()
        {
            return await users
                .AsQueryable()
                .ToListAsync();
        }
    }
}
