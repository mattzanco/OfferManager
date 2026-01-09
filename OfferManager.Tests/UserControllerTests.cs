using Xunit;
using OfferManager.WebApi.Controllers;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User> { new User { Id = 1, Username = "test", Email = "test@example.com" } });
            var controller = new UserController(mockRepo.Object);
            var result = await controller.GetAll();
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
