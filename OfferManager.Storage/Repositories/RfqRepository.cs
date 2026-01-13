using Microsoft.Extensions.Logging;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class RfqRepository : IRfqRepository
    {
        private readonly Microsoft.Extensions.Logging.ILogger<RfqRepository> _logger;

        public RfqRepository(Microsoft.Extensions.Logging.ILogger<RfqRepository> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<Rfq>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all RFQs");
            return Task.FromResult<IEnumerable<Rfq>>(new List<Rfq>());
        }

        public Task<Rfq?> GetByIdAsync(System.Guid id)
        {
            _logger.LogDebug("Fetching RFQ by id: {Id}", id);
            _logger.LogWarning("RFQ not found: {Id}", id);
            return Task.FromResult<Rfq?>(null);
        }

        public Task<System.Guid> AddAsync(Rfq rfq)
        {
            _logger.LogInformation("Added RFQ: {Id}", rfq.RfqId);
            return Task.FromResult(System.Guid.Empty);
        }

        public Task<bool> UpdateAsync(Rfq rfq)
        {
            _logger.LogWarning("Update failed, RFQ not found: {Id}", rfq.RfqId);
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(System.Guid id)
        {
            _logger.LogWarning("Delete failed, RFQ not found: {Id}", id);
            return Task.FromResult(false);
        }
    }
}
