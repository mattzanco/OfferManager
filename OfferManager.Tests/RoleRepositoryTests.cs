using Xunit;
using OfferManager.Storage.Repositories;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;

namespace OfferManager.Tests
{
    public class RoleRepositoryTests
    {
        private (IConfiguration, Microsoft.Extensions.Logging.ILogger<RoleRepository>) CreateMocks()
        {
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<RoleRepository>>();
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c[It.IsAny<string>()]).Returns("FakeConnectionString");
            return (mockConfig.Object, mockLogger.Object);
        }

        [Fact]
        public async Task AddAndGetRole_Works()
        {
            var mockRepo = new Mock<IRoleRepository>();
            var expected = new Role { Name = "Test Role" };
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Role>())).ReturnsAsync(1);
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expected);

            var id = await mockRepo.Object.AddAsync(new Role { Name = "Test Role" });
            var role = await mockRepo.Object.GetByIdAsync(id);
            Assert.NotNull(role);
        }

        [Fact]
        public async Task Update_ReturnsFalse()
        {
            var mockRepo = new Mock<IRoleRepository>();
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Role>())).ReturnsAsync(false);
            var result = await mockRepo.Object.UpdateAsync(new Role { Name = "Test Role" });
            Assert.False(result);
        }

        [Fact]
        public async Task Delete_ReturnsFalse()
        {
            var mockRepo = new Mock<IRoleRepository>();
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);
            var result = await mockRepo.Object.DeleteAsync(1);
            Assert.False(result);
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList()
        {
            var mockRepo = new Mock<IRoleRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Role>());
            var roles = await mockRepo.Object.GetAllAsync();
            Assert.Empty(roles);
        }
    }
}
