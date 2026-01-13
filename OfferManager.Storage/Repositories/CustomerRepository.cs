using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new();
        private readonly Microsoft.Extensions.Logging.ILogger<CustomerRepository> _logger;

        public CustomerRepository(Microsoft.Extensions.Logging.ILogger<CustomerRepository> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all customers");
            return Task.FromResult<IEnumerable<Customer>>(_customers);
        }

        public Task<Customer?> GetByIdAsync(System.Guid id)
        {
            _logger.LogDebug("Fetching customer by id: {Id}", id);
            var customer = _customers.Find(c => c.CustomerId == id);
            if (customer == null)
                _logger.LogWarning("Customer not found: {Id}", id);
            else
                _logger.LogInformation("Customer found: {Id}", id);
            return Task.FromResult(customer);
        }

        public Task<System.Guid> AddAsync(Customer customer)
        {
            _customers.Add(customer);
            _logger.LogInformation("Added customer: {Id}", customer.CustomerId);
            return Task.FromResult(customer.CustomerId);
        }

        public Task<bool> UpdateAsync(Customer customer)
        {
            var existing = _customers.Find(c => c.CustomerId == customer.CustomerId);
            if (existing == null)
            {
                _logger.LogWarning("Update failed, customer not found: {Id}", customer.CustomerId);
                return Task.FromResult(false);
            }
            existing.Name = customer.Name;
            _logger.LogInformation("Updated customer: {Id}", customer.CustomerId);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(System.Guid id)
        {
            var customer = _customers.Find(c => c.CustomerId == id);
            if (customer == null)
            {
                _logger.LogWarning("Delete failed, customer not found: {Id}", id);
                return Task.FromResult(false);
            }
            _customers.Remove(customer);
            _logger.LogInformation("Deleted customer: {Id}", id);
            return Task.FromResult(true);
        }
    }
}
