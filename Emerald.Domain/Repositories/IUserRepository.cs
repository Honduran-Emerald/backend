using Emerald.Domain.Models.UserAggregate;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task Update(User user);

        Task<User> Get(ObjectId id);
        Task<User> Get(ClaimsPrincipal claim);
        Task<User> GetByEmail(string email);
        Task<List<User>> All();
        IMongoQueryable<User> GetQueryable();
    }
}
