using System.ComponentModel.DataAnnotations;

namespace SafeVault.Core.Dto
{
    public class RegisterDto
    {
        [Required, EmailAddress] public string Email { get; set; } = "";
        [Required, StringLength(100, MinimumLength = 12)]
        public string Password { get; set; } = "";
        [Required, RegularExpression("^[a-zA-Z0-9_]{3,30}$")]
        public string Username { get; set; } = "";
    }
}
