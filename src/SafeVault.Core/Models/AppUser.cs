using Microsoft.AspNetCore.Identity;

namespace SafeVault.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string? DisplayName { get; set; }
    }
}
