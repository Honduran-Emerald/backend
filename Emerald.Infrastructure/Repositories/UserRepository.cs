using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IMongoCollection<User> users;
        private UserManager<User> userManager;
        private IMediator mediator;

        public UserRepository(IMongoDbContext dbContext, UserManager<User> userManager, IMediator mediator)
        {
            this.userManager = userManager;
            users = dbContext.Emerald.GetCollection<User>("Users");
            this.mediator = mediator;
        }

        public async Task<User> Get(ObjectId id)
        {
            var user = await users
                .Find(user => user.Id == id)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new MissingElementException();
            }

            return user;
        }

        public async Task Update(User user)
        {
            await userManager.UpdateAsync(user);
            await mediator.PublishEntity(user);
        }

        public async Task Add(User user)
        {
            await userManager.CreateAsync(user);
            await mediator.PublishEntity(user);
        }

        public async Task<List<User>> All()
        {
            return await users
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public IMongoQueryable<User> GetQueryable()
        {
            return users.AsQueryable();
        }

        public async Task<User> Get(ClaimsPrincipal claim)
        {
            return await userManager.GetUserAsync(claim);
        }
    }
}
