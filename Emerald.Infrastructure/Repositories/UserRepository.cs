using Emerald.Domain.Models.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IMongoCollection<User> users;
        private UserManager<User> userManager;
        private Mediator mediator;

        public UserRepository(MongoDbContext dbContext, UserManager<User> userManager, Mediator mediator)
        {
            this.userManager = userManager;
            users = dbContext.Emerald.GetCollection<User>("Users");
            this.mediator = mediator;
        }

        public async Task<User> Get(ObjectId id)
        {
            return await users
                .Find(user => user.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(User user)
        {
            await userManager.UpdateAsync(user);
        }

        public async Task Add(User user)
        {
            await userManager.CreateAsync(user);
        }

        public async Task<List<User>> All()
        {
            return await users
                .AsQueryable()
                .ToListAsync();
        }
    }
}
