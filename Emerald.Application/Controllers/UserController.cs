using Emerald.Application.Models.Response;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private UserModelFactory userModelFactory;
        private UserManager<User> userManager;

        public UserController(UserModelFactory userModelFactory, UserManager<User> userManager)
        {
            this.userModelFactory = userModelFactory;
            this.userManager = userManager;
        }

        /// <summary>
        /// Get profile information about current authorized user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserMeResponse>> Me()
        {
            return Ok(new UserMeResponse(
                await userModelFactory.Create(
                    await userManager.GetUserAsync(User))));
        }
    }
}
