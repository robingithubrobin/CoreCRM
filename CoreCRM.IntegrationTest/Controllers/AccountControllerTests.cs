using System.Net.Http;
using Xunit;

namespace CoreCRM.IntegrationTest.Controllers
{
    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dotnet-core.html
    public class AccountControllerTests : IClassFixture<TestFixture<TestStartup>>
    {
        private readonly HttpClient _client;
        public AccountControllerTests(TestFixture<TestStartup> fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task Login_WhenFindByNameFailed_FindByEmailCalled()
        {
            // Arrange
            var mockRepo = new Mock<UserManager<ApplicationUser>>();
            mockRepo.Setup(repo => repo.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult((ApplicationUser)null));
            var controller = new AccountController(mockRepo.Object, null, null, null, null);
            var model = new LoginViewModel();

            // Verify
            mockRepo.Verify(repo => repo.FindByEmailAsync(It.IsAny<string>()));

            // Act
            var result = await controller.Login(model);

            // Assert
        }
    }
}
