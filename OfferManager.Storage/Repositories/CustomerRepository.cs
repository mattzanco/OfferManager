using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetAllAsync() => Task.FromResult<IEnumerable<Customer>>(new List<Customer>());
        public Task<Customer?> GetByIdAsync(System.Guid id) => Task.FromResult<Customer?>(null);
        public Task<System.Guid> AddAsync(Customer customer) => Task.FromResult(System.Guid.Empty);
        public Task<bool> UpdateAsync(Customer customer) => Task.FromResult(false);
        public Task<bool> DeleteAsync(System.Guid id) => Task.FromResult(false);
    }
}
