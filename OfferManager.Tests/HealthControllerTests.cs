using Xunit;
using OfferManager.WebApi.Controllers;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OfferManager.Tests
{
    public class HealthControllerTests
    {
        [Fact]
        public void Ping_ReturnsOkTrue()
        {
            var mockLogger = new Moq.Mock<Microsoft.Extensions.Logging.ILogger<OfferManager.WebApi.Controllers.HealthController>>();
            var controller = new HealthController(mockLogger.Object);
            var result = controller.Ping();
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value!);
        }
    }
}
