using System.ComponentModel.DataAnnotations;
using SafeVault.Core.Dto;
using Xunit;

namespace SafeVault.Tests
{
    public class RegisterDtoTests
    {
        [Fact]
        public void Password_MustBeAtLeast12Chars()
        {
            var dto = new RegisterDto { Email = "a@b.com", Password = "short", Username = "user_1" };
            var ctx = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(dto, ctx, results, true);

            Assert.False(valid);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(RegisterDto.Password)));
        }
    }
}
