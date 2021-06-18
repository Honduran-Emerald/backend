using Emerald.Application.Models.Bindings;
using Emerald.Application.Models.Response;
using Emerald.Application.Services;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// Get a new token by credentials
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get new token by old token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("renew")]
        public async Task<ActionResult<AuthenticationTokenResponse>> Renew()
        {
            return Ok(new AuthenticationTokenResponse
            {
                Token = await authentication.GenerateToken(await userManager.GetUserAsync(User))
            });
        }


        /// <summary>
        /// Remove a messaging token from current containing user
        /// </summary>
        /// <param name="messagingToken"></param>
        /// <returns></returns>
        [HttpPost("invalidatemessagingtoken")]
        public async Task<IActionResult> InvalidateMessagingToken(
            [FromBody] string messagingToken,
            [FromServices] IUserRepository userRepository)
        {
            User? user = await userRepository.GetQueryable()
                .Where(u => u.MessagingToken == messagingToken)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "Token not found"
                });
            }

            user.MessagingToken = null;
            await userRepository.Update(user);

            return Ok();
        }
    }
}
