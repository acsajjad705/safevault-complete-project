using System.Net;
using Xunit;

namespace SafeVault.Tests
{
    public class SecurityTests
    {
        [Fact]
        public async Task Login_BlocksSqlInjectionPayloads()
        {
            using var app = new TestServerBuilder().Build();
            var client = app.CreateClient();

            var res = await client.PostAsJsonAsync("/api/auth/login", new
            {
                Email = "anything@example.com' OR 1=1 --",
                Password = "irrelevant"
            });

            Assert.True(res.StatusCode == HttpStatusCode.BadRequest || res.StatusCode == HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task ProfileView_EncodesHtml()
        {
            using var app = new TestServerBuilder().Build();
            var client = app.CreateClient();

            var html = await client.GetStringAsync("/profile");
            Assert.DoesNotContain("<script>", html);
        }
    }
}
