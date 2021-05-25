using System.ComponentModel.DataAnnotations;

namespace Emerald.Application.Models.Bindings
{
    public class AuthenticationLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public AuthenticationLoginRequest()
        {
            Email = default!;
            Password = default!;
        }
    }
}
