using Emerald.Application.Models.Response;
using Emerald.Application.Models.Bindings;
using Emerald.Application.Services;
using Emerald.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Emerald.Infrastructure.Repositories;

namespace Emerald.Application.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private UserManager<User> userManager;
        private IUserRepository userRepository;
        private IJwtAuthentication authentication;

        public AuthenticationController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IJwtAuthentication authentication)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.authentication = authentication;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationTokenResponse>> Login(
            [FromBody] AuthenticationLoginRequest binding)
        {
            User user = await userRepository.GetByEmail(binding.Email);

            if (await userManager.CheckPasswordAsync(user, binding.Password))
            {
                return Ok(new AuthenticationTokenResponse
                {
                    Token = await authentication.GenerateToken(user)
                });
            }

            return BadRequest(new
            {
                message = "Login failed"
            });
        }

        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationTokenResponse>> Create(
            [FromBody] AuthenticationRegisterRequest binding)
        {
            User user = new User(binding.Username);
            user.Email = binding.Email;

            var result = await userManager.CreateAsync(user, binding.Password);

            if (result.Succeeded)
            {
                return Ok(new AuthenticationTokenResponse
                {
                    Token = await authentication.GenerateToken(user)
                });
            }
            else
            {
                return BadRequest(new
                {
                    message = result.Errors.First()
                });
            }
        }

        [Authorize]
        [HttpPost("renew")]
        public async Task<ActionResult<AuthenticationTokenResponse>> Renew()
        {
            return Ok(new AuthenticationTokenResponse
            {
                Token = await authentication.GenerateToken(await userManager.GetUserAsync(User))
            });
        }
    }
}
