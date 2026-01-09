using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class CustomerContactRepository : ICustomerContactRepository
    {
        public Task<IEnumerable<CustomerContact>> GetAllAsync() => Task.FromResult<IEnumerable<CustomerContact>>(new List<CustomerContact>());
        public Task<CustomerContact?> GetByIdAsync(System.Guid id) => Task.FromResult<CustomerContact?>(null);
        public Task<System.Guid> AddAsync(CustomerContact contact) => Task.FromResult(System.Guid.Empty);
        public Task<bool> UpdateAsync(CustomerContact contact) => Task.FromResult(false);
        public Task<bool> DeleteAsync(System.Guid id) => Task.FromResult(false);
    }
}
