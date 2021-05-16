using Emerald.Application.Models.Binding;
using Emerald.Application.Models.Bindings;
using Emerald.Application.Services;
using Emerald.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private SignInManager<User> signInManager;
        private UserManager<User> userManager;
        private IJwtAuthentication authentication;

        public AuthenticationController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IJwtAuthentication authentication)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.authentication = authentication;
        }

        [HttpPost("login")]
        public async Task<ActionResult<NewTokenBinding>> Login(
            [FromBody] AuthLoginModel binding)
        {
            User user = await userManager.FindByEmailAsync(binding.Email);

            if (await userManager.CheckPasswordAsync(user, binding.Password))
            {
                return Ok(new NewTokenBinding
                {
                    Token = await authentication.GenerateToken(user)
                });
            }

            return BadRequest();
        }

        [HttpPost("create")]
        public async Task<ActionResult<NewTokenBinding>> Create(
            [FromBody] AuthRegisterModel binding)
        {
            User user = new User(binding.Username);
            user.Email = binding.Email;

            var result = await userManager.CreateAsync(user, binding.Password);

            if (result.Succeeded)
            {
                return Ok(new NewTokenBinding
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
        public async Task<ActionResult<NewTokenBinding>> Renew()
        {
            return Ok(new NewTokenBinding
            {
                Token = await authentication.GenerateToken(await userManager.GetUserAsync(User))
            });
        }
    }
}
