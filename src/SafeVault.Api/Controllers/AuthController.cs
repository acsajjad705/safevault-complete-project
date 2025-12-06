using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SafeVault.Core.Dto;
using SafeVault.Core.Models;

namespace SafeVault.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new AppUser { UserName = dto.Username, Email = dto.Email, EmailConfirmed = false };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "User");
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterDto dto)
        {
            // Using identity methods avoids raw SQL and is safe against SQLi
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null) return Unauthorized();

            var check = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
            if (!check.Succeeded) return Unauthorized();

            // Issue JWT (implementation in Program.cs)
            var token = JwtTokenFactory.CreateToken(user);
            return Ok(new { token });
        }
    }
}
