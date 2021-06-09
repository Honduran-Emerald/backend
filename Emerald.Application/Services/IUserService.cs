using Emerald.Domain.Models.UserAggregate;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public interface IUserService
    {
        Task<User> CurrentUser();
    }
}
