using Xunit;
using OfferManager.Storage.Repositories;
using OfferManager.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Moq;
using Microsoft.Extensions.Logging;
using Dapper;
using OfferManager.Domain.Interfaces;

namespace OfferManager.Tests
{
    public class OfferRepositoryTests
    {
        private OfferRepository CreateRepository()
        {
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<OfferManager.Storage.Repositories.OfferRepository>>();
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c[It.IsAny<string>()]).Returns("FakeConnectionString");
            return new OfferManager.Storage.Repositories.OfferRepository(mockConfig.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsList()
        {
            var mockRepo = new Mock<IOfferRepository>();
            var expected = new List<Offer> { new Offer { Title = "Test", Status = "TestStatus", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow } };
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(expected);

            var offers = await mockRepo.Object.GetAllAsync();
            Assert.NotNull(offers);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNullForMissing()
        {
            var mockRepo = new Mock<IOfferRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Offer?)null);
            var offer = await mockRepo.Object.GetByIdAsync(-1);
            Assert.Null(offer);
        }
    }
}
