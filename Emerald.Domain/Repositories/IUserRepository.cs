using Emerald.Domain.Models.UserAggregate;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    // user can be add by 
    public interface IUserRepository
    {
        Task Add(User user);
        Task Update(User user);

        Task<User> Get(ObjectId id);
        Task<User> GetByEmail(string email);
        Task<List<User>> All();
    }
}
