using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class LaneRepository : ILaneRepository
    {
        public Task<IEnumerable<Lane>> GetAllAsync() => Task.FromResult<IEnumerable<Lane>>(new List<Lane>());
        public Task<Lane?> GetByIdAsync(System.Guid id) => Task.FromResult<Lane?>(null);
        public Task<System.Guid> AddAsync(Lane lane) => Task.FromResult(System.Guid.Empty);
        public Task<bool> UpdateAsync(Lane lane) => Task.FromResult(false);
        public Task<bool> DeleteAsync(System.Guid id) => Task.FromResult(false);
    }
}
