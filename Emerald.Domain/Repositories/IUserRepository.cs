using Emerald.Domain.Models.UserAggregate;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emerald.Infrastructure.Repositories
{
    // user can be add by 
    public interface IUserRepository
    {
        Task<User> Get(ObjectId id);
        Task<List<User>> All();
    }
}
