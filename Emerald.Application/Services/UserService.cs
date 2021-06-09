using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Services
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor httpContextAccessor;
        private IUserRepository userRepository;

        public UserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
        }

        public async Task<User> CurrentUser()
        {
            if (httpContextAccessor.HttpContext == null)
            {
                throw new Exception("Unable to get current user if HttpContext is null");
            }

            return await userRepository.Get(httpContextAccessor.HttpContext.User);
        }
    }
}
