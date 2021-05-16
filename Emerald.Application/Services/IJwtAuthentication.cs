using Emerald.Domain.Models.UserAggregate;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public interface IJwtAuthentication
    {
        Task<string> GenerateToken(User user);
    }
}
