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
    public class UserRepositoryTests
    {
        private (IConfiguration, Microsoft.Extensions.Logging.ILogger<UserRepository>) CreateMocks()
        {
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<UserRepository>>();
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c[It.IsAny<string>()]).Returns("FakeConnectionString");
            return (mockConfig.Object, mockLogger.Object);
        }

        [Fact]
        public async Task AddAndGetUser_Works()
        {
            var mockRepo = new Mock<IUserRepository>();
            var expected = new User { Username = "testuser", Email = "test@example.com" };
            mockRepo.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(1);
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expected);

            var id = await mockRepo.Object.AddAsync(new User { Username = "testuser", Email = "test@example.com" });
            var user = await mockRepo.Object.GetByIdAsync(id);
            Assert.NotNull(user);
        }
    }
}
