using Xunit;

namespace CoreCRM.UnitTest.Controllers
{
    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dotnet-core.html
    public class AccountControllerTests
    {
        [Fact]
        public void Login_WhenFindByNameFailed_FindByEmailCalled()
        {
            // Arrange
            var mockRepo = new Mock<UserManager>();
            mockRepo.Setup(repo => repo.ListAsync()).Returns(Task.FromResult(GetTestSessions()));
            var controller = new HomeController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
