using System.ComponentModel.DataAnnotations;

namespace Emerald.Application.Models.Bindings
{
    public class AuthRegisterModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
