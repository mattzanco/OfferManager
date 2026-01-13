using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class CustomerContactRepository : ICustomerContactRepository
    {
        private readonly Microsoft.Extensions.Logging.ILogger<CustomerContactRepository> _logger;

        public CustomerContactRepository(Microsoft.Extensions.Logging.ILogger<CustomerContactRepository> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<CustomerContact>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all customer contacts");
            return Task.FromResult<IEnumerable<CustomerContact>>(new List<CustomerContact>());
        }

        public Task<CustomerContact?> GetByIdAsync(System.Guid id)
        {
            _logger.LogDebug("Fetching customer contact by id: {Id}", id);
            _logger.LogWarning("Customer contact not found: {Id}", id);
            return Task.FromResult<CustomerContact?>(null);
        }

        public Task<System.Guid> AddAsync(CustomerContact contact)
        {
            _logger.LogInformation("Added customer contact: {Id}", contact.ContactId);
            return Task.FromResult(System.Guid.Empty);
        }

        public Task<bool> UpdateAsync(CustomerContact contact)
        {
            _logger.LogWarning("Update failed, customer contact not found: {Id}", contact.ContactId);
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(System.Guid id)
        {
            _logger.LogWarning("Delete failed, customer contact not found: {Id}", id);
            return Task.FromResult(false);
        }
    }
}
