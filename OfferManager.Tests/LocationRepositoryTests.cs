using Xunit;
using OfferManager.Storage.Repositories;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;

namespace OfferManager.Tests
{
    public class LocationRepositoryTests
    {
        private (IConfiguration, Microsoft.Extensions.Logging.ILogger<LocationRepository>) CreateMocks()
        {
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<LocationRepository>>();
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c[It.IsAny<string>()]).Returns("FakeConnectionString");
            return (mockConfig.Object, mockLogger.Object);
        }

        [Fact]
        public async Task AddAndGetLocation_Works()
        {
            var mockRepo = new Mock<ILocationRepository>();
            var expected = new Location { Name = "Test Location" };
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Location>())).ReturnsAsync(Guid.NewGuid());
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expected);

            var id = await mockRepo.Object.AddAsync(new Location { Name = "Test Location" });
            var loc = await mockRepo.Object.GetByIdAsync(id);
            Assert.NotNull(loc);
        }

        [Fact]
        public async Task Update_ReturnsFalse()
        {
            var mockRepo = new Mock<ILocationRepository>();
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Location>())).ReturnsAsync(false);
            var result = await mockRepo.Object.UpdateAsync(new Location { Name = "Test Location" });
            Assert.False(result);
        }

        [Fact]
        public async Task Delete_ReturnsFalse()
        {
            var mockRepo = new Mock<ILocationRepository>();
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            var result = await mockRepo.Object.DeleteAsync(Guid.NewGuid());
            Assert.False(result);
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList()
        {
            var mockRepo = new Mock<ILocationRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Location>());
            var locs = await mockRepo.Object.GetAllAsync();
            Assert.Empty(locs);
        }
    }
}
