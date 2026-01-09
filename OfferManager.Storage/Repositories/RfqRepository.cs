using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class RfqRepository : IRfqRepository
    {
        public Task<IEnumerable<Rfq>> GetAllAsync() => Task.FromResult<IEnumerable<Rfq>>(new List<Rfq>());
        public Task<Rfq?> GetByIdAsync(System.Guid id) => Task.FromResult<Rfq?>(null);
        public Task<System.Guid> AddAsync(Rfq rfq) => Task.FromResult(System.Guid.Empty);
        public Task<bool> UpdateAsync(Rfq rfq) => Task.FromResult(false);
        public Task<bool> DeleteAsync(System.Guid id) => Task.FromResult(false);
    }
}
