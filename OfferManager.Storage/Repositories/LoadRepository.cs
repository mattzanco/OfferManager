using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class LoadRepository : ILoadRepository
    {
        public Task<IEnumerable<Load>> GetAllAsync() => Task.FromResult<IEnumerable<Load>>(new List<Load>());
        public Task<Load?> GetByIdAsync(System.Guid id) => Task.FromResult<Load?>(null);
        public Task<System.Guid> AddAsync(Load load) => Task.FromResult(System.Guid.Empty);
        public Task<bool> UpdateAsync(Load load) => Task.FromResult(false);
        public Task<bool> DeleteAsync(System.Guid id) => Task.FromResult(false);
    }
}
