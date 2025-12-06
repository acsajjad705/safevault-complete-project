using System.Net;
using System.Net.Http.Headers;
using Xunit;

namespace SafeVault.Tests
{
    public class AuthorizationTests
    {
        [Fact]
        public async Task AddItem_RequiresManagerOrAdmin()
        {
            using var app = new TestServerBuilder().Build();
            var client = app.CreateClient();

            var res1 = await client.PostAsJsonAsync("/api/vaults/00000000-0000-0000-0000-000000000001/items", new { Name = "X" });
            Assert.Equal(HttpStatusCode.Unauthorized, res1.StatusCode);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Tokens.User);
            var res2 = await client.PostAsJsonAsync("/api/vaults/00000000-0000-0000-0000-000000000001/items", new { Name = "X" });
            Assert.Equal(HttpStatusCode.Forbidden, res2.StatusCode);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Tokens.Manager);
            var res3 = await client.PostAsJsonAsync("/api/vaults/00000000-0000-0000-0000-000000000001/items", new { Name = "X" });
            Assert.Equal(HttpStatusCode.OK, res3.StatusCode);
        }
    }

    // Minimal placeholders to make tests compile in assignment context:
    internal static class Tokens
    {
        public static string User => "user-token";
        public static string Manager => "manager-token";
    }

    internal class TestServerBuilder
    {
        public Microsoft.AspNetCore.TestHost.TestServer Build()
        {
            var app = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder().Build();
            return new Microsoft.AspNetCore.TestHost.TestServer(app.Services);
        }
    }
}
