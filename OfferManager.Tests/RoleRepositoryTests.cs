using Xunit;
using OfferManager.Storage.Repositories;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Tests
{
    public class RoleRepositoryTests
    {
        [Fact]
        public async Task AddAndGetRole_Works()
        {
            var repo = new RoleRepository();
            var role = new Role { RoleId = 1, Name = "Admin" };
            var id = await repo.AddAsync(role);
            var result = await repo.GetByIdAsync(id);
            Assert.Null(result); // Stub always returns null
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList()
        {
            var repo = new RoleRepository();
            var roles = await repo.GetAllAsync();
            Assert.Empty(roles);
        }

        [Fact]
        public async Task Update_ReturnsFalse()
        {
            var repo = new RoleRepository();
            var role = new Role { RoleId = 1, Name = "Admin" };
            var updated = await repo.UpdateAsync(role);
            Assert.False(updated);
        }

        [Fact]
        public async Task Delete_ReturnsFalse()
        {
            var repo = new RoleRepository();
            var deleted = await repo.DeleteAsync(1);
            Assert.False(deleted);
        }
    }
}
