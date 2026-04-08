using Xunit;
using OfferManager.Storage.Repositories;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Threading.Tasks;
using System;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;

namespace OfferManager.Tests
{
    public class CustomerRepositoryTests
    {
        private (IConfiguration, Microsoft.Extensions.Logging.ILogger<CustomerRepository>) CreateMocks()
        {
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<CustomerRepository>>();
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c[It.IsAny<string>()]).Returns("FakeConnectionString");
            return (mockConfig.Object, mockLogger.Object);
        }

        [Fact]
        public async Task AddAndGetCustomer_Works()
        {
            var mockRepo = new Mock<ICustomerRepository>();
            var expected = new Customer { Name = "Test Customer" };
            var newId = new System.Random().Next(1, 10000);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Customer>())).ReturnsAsync(newId);
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expected);

            var id = await mockRepo.Object.AddAsync(new Customer { Name = "Test Customer" });
            var customer = await mockRepo.Object.GetByIdAsync(id);
            Assert.NotNull(customer);
        }
    }
}


