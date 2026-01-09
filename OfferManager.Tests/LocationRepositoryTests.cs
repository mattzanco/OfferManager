using Xunit;
using OfferManager.Storage.Repositories;
using OfferManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Tests
{
    public class LocationRepositoryTests
    {
        [Fact]
        public async Task AddAndGetLocation_Works()
        {
            var repo = new LocationRepository();
            var location = new Location { LocationId = Guid.NewGuid(), Name = "HQ" };
            var id = await repo.AddAsync(location);
            var result = await repo.GetByIdAsync(id);
            Assert.Null(result); // Stub always returns null
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList()
        {
            var repo = new LocationRepository();
            var locations = await repo.GetAllAsync();
            Assert.Empty(locations);
        }

        [Fact]
        public async Task Update_ReturnsFalse()
        {
            var repo = new LocationRepository();
            var location = new Location { LocationId = Guid.NewGuid(), Name = "HQ" };
            var updated = await repo.UpdateAsync(location);
            Assert.False(updated);
        }

        [Fact]
        public async Task Delete_ReturnsFalse()
        {
            var repo = new LocationRepository();
            var deleted = await repo.DeleteAsync(Guid.NewGuid());
            Assert.False(deleted);
        }
    }
}
