using Emerald.Application.Models.Bindings;
using Emerald.Application.Services;
using Emerald.Domain.Models.UserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        [Authorize]
        [HttpGet("authorized")]
        public IActionResult IsAuthorized()
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromForm] AuthLoginBinding binding)
        {
            User user = await userManager.FindByEmailAsync(binding.Email);
            
            if (await userManager.CheckPasswordAsync(user, binding.Password))
            {
                return Ok(await authentication.GenerateToken(user));
            }

            return BadRequest();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromForm] AuthRegisterBinding binding)
        {
            User user = new User(binding.Username);
            user.Email = binding.Email;

            var result = await userManager.CreateAsync(user, binding.Password);

            if (result.Succeeded)
            {
                return Ok(User.Identity.Name);
            }
            else
            {
                return BadRequest(result.Errors.First());
            }
        }
    }
}
