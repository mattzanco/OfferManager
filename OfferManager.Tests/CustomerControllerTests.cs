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
    public class CustomerControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Customer> { new Customer { CustomerId = Guid.NewGuid(), Name = "Acme" } });
            var mockLogger = new Moq.Mock<Microsoft.Extensions.Logging.ILogger<OfferManager.WebApi.Controllers.CustomerController>>();
            var controller = new CustomerController(mockRepo.Object, mockLogger.Object);
            var result = await controller.GetAll();
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
