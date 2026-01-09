using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        public Task<IEnumerable<Location>> GetAllAsync() => Task.FromResult<IEnumerable<Location>>(new List<Location>());
        public Task<Location?> GetByIdAsync(System.Guid id) => Task.FromResult<Location?>(null);
        public Task<System.Guid> AddAsync(Location location) => Task.FromResult(System.Guid.Empty);
        public Task<bool> UpdateAsync(Location location) => Task.FromResult(false);
        public Task<bool> DeleteAsync(System.Guid id) => Task.FromResult(false);
    }
}
