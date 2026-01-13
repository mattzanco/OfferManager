using Xunit;
using OfferManager.Storage.Repositories;
using OfferManager.Domain.Models;
using System.Threading.Tasks;
using System;

namespace OfferManager.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task AddAndGetUser_Works()
        {
            var mockLogger = new Moq.Mock<Microsoft.Extensions.Logging.ILogger<UserRepository>>();
            var repo = new UserRepository(mockLogger.Object);
            var user = new User { Id = 1, Username = "test", Email = "test@example.com" };
            await repo.AddAsync(user);
            var result = await repo.GetByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal("test", result!.Username);
        }
    }
}
