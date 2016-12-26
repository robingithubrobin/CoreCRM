using System.Collections.Generic;
using System.Net;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using CoreCRM.Data;

namespace CoreCRM.IntegrationTest.Controllers
{
    // ===================================================
    // ALWAYS REMEMBER THE DATABASE IS SHARED CROSS TESTS.
    // ===================================================

    public class AccountControllerTestStartup : TestStartup
    {
        public AccountControllerTestStartup(IHostingEnvironment env) : base(env)
        {

        }

        protected override void DatabaseFactory(IApplicationBuilder app, ApplicationDbContext dbContext)
        {
            base.DatabaseFactory(app, dbContext);
        }
    }

    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dotnet-core.html
    public class AccountControllerTests : AbstractIntegrationTest<AccountControllerTestStartup>
    {
        public AccountControllerTests(TestFixture<AccountControllerTestStartup> fixture) : base(fixture)
        {

        }

        public override void Dispose()
        {

        }

        [Fact]
        public async void Login_WithUserName_RedirectToUri()
        {
            // Arrange
            // Fetch AntiForgeryToken
            var response = await _client.GetAsync("/Account/Login");
            response.EnsureSuccessStatusCode();

            // Extract token
            string antiForgeryToken = await ExtractAntiForgeryToken(response);

            // Fill form
            var formPostBodyData = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", antiForgeryToken}, // Add token
                {"Account", "admin"},
                {"Password", "123abC_"},
                {"RememberMe", "true"}
            };
            var requestMessage = CreateWithCookiesFromResponse("/Account/Login?returnUrl=%2F", formPostBodyData, response);

            // Act
            response = await _client.SendAsync(requestMessage);

            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/", response.Headers.Location.ToString());
        }

        [Fact]
        public async void Login_WithEmail_RedirectToUri()
        {
            // Arrange
            // Fetch AntiForgeryToken
            var response = await _client.GetAsync("/Account/Login");
            response.EnsureSuccessStatusCode();

            // Extract token
            string antiForgeryToken = await ExtractAntiForgeryToken(response);

            // Fill form
            var formPostBodyData = new Dictionary<string, string>
            {
                {"__RequestVerificationToken", antiForgeryToken}, // Add token
                {"Account", "admin@example.com"},
                {"Password", "123abC_"},
                {"RememberMe", "true"}
            };
            var requestMessage = CreateWithCookiesFromResponse("/Account/Login?returnUrl=%2F", formPostBodyData, response);

            // Act
            response = await _client.SendAsync(requestMessage);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/", response.Headers.Location.ToString());
        }
    }
}
