using Moq;
using Xunit;
using Microsoft.AspNetCore.Identity;
using CoreCRM.Models;
using System.Threading.Tasks;
using CoreCRM.Controllers;
using CoreCRM.ViewModels.AccountViewModels;

namespace CoreCRM.UnitTest.Controllers
{
    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dotnet-core.html
    public class AccountControllerTests
    {
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
