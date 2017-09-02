﻿using GomelRectorCouncil.Controllers;
using GomelRectorCouncil.Data;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace GomelRectorCouncil.Tests
{
    public class HomeControllerTests
    {
        private CouncilDbContext _context;

        //Получение контекста базы данных для тестирования
        public HomeControllerTests() {
            var optionsBuilder = new DbContextOptionsBuilder<CouncilDbContext>();
            var options = optionsBuilder
                .UseSqlite("DataSource=.\\GomelRectorCouncil.db")
                .Options;
            _context = new CouncilDbContext(options);
            // инициализация базы данных по университетам
            DbInitializerTests.Initialize(_context);
        }

        [Fact]
        public void IndexViewDataMessage()
        {
            // Arrange
            HomeController controller = new HomeController(_context);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.Equal(expected:"СРГО",actual: result?.ViewData["Title"]);
        }

        [Fact]
        public void IndexViewResultNotNull()
        {
            // Arrange
            HomeController controller = new HomeController(_context);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            // Arrange
            HomeController controller = new HomeController(_context);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.Equal(expected: "Index", actual: result?.ViewName);
        }
        [Fact]
        public void IndexViewModeNotNull()
        {
            // Arrange
            HomeController controller = new HomeController(_context);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result.Model);
        }

    }
}
