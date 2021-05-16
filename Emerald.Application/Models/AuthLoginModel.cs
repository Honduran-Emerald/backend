using System.ComponentModel.DataAnnotations;

namespace Emerald.Application.Models.Bindings
{
    public class AuthLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
