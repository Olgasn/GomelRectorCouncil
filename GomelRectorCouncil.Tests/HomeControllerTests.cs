using GomelRectorCouncil.Controllers;
using GomelRectorCouncil.Data;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GomelRectorCouncil.Tests
{
    public class HomeControllerTests
    {

        [Fact]
        public void IndexViewDataMessage(CouncilDbContext context)
        {
            // Arrange
            HomeController controller = new HomeController(context);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("Hello world!", result?.ViewData["Message"]);
        }

        [Fact]
        public void IndexViewResultNotNull(CouncilDbContext context)
        {
            // Arrange
            HomeController controller = new HomeController(context);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexViewNameEqualIndex(CouncilDbContext context)
        {
            // Arrange
            HomeController controller = new HomeController(context);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.Equal("Index", result?.ViewName);
        }
    }
}
