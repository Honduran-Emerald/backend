using Emerald.Domain.Models.UserAggregate;
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

        public AuthenticationController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            User user = await userManager.FindByNameAsync(username);
            if (await userManager.CheckPasswordAsync(user, password))
            {

            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            string username,
            string email, 
            string password)
        {
            User user = new User(username);
            user.Email = email;

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return Ok(User.Identity.Name);
            }
            else
            {
                return BadRequest(result.Errors.First());
            }
        }

        private string GenerateToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("");
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(
                tokenHandler.CreateToken(tokenDescriptor)); 
        }
    }
}
