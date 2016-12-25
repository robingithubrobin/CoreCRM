using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace CoreCRM.IntegrationTest.Controllers
{
    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dotnet-core.html
    public class AccountControllerTests : IClassFixture<IClassFixture<TestStarting>>
    {
        private readonly HttpClient _client;
        public AccountControllerTests(TestFixture<TestStartup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async void Login_WithEmail_RedirectToUri()
        {
            // Arrange
            var dict = new Dictionary<string, string>();
            dict.Add("Account", "admin@163.com");
            dict.Add("Password", "123456");
            dict.Add("RememberMe", "1");
            var content = new FormUrlEncodedContent(dict);

            // Act
            var response = await _client.PostAsync("/Account/Login?redirecturl=/", content);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/", response.Headers.Location.ToString());
        }
    }
}
