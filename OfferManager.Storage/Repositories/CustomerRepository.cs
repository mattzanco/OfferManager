using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new();

        public Task<IEnumerable<Customer>> GetAllAsync() => Task.FromResult<IEnumerable<Customer>>(_customers);

        public Task<Customer?> GetByIdAsync(System.Guid id) => Task.FromResult(_customers.Find(c => c.CustomerId == id));

        public Task<System.Guid> AddAsync(Customer customer)
        {
            _customers.Add(customer);
            return Task.FromResult(customer.CustomerId);
        }

        public Task<bool> UpdateAsync(Customer customer)
        {
            var existing = _customers.Find(c => c.CustomerId == customer.CustomerId);
            if (existing == null) return Task.FromResult(false);
            existing.Name = customer.Name;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(System.Guid id)
        {
            var customer = _customers.Find(c => c.CustomerId == id);
            if (customer == null) return Task.FromResult(false);
            _customers.Remove(customer);
            return Task.FromResult(true);
        }
    }
}
