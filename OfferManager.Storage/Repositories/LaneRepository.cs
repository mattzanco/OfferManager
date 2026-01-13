using Microsoft.Extensions.Logging;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class LaneRepository : ILaneRepository
    {
        private readonly Microsoft.Extensions.Logging.ILogger<LaneRepository> _logger;

        public LaneRepository(Microsoft.Extensions.Logging.ILogger<LaneRepository> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<Lane>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all lanes");
            return Task.FromResult<IEnumerable<Lane>>(new List<Lane>());
        }

        public Task<Lane?> GetByIdAsync(System.Guid id)
        {
            _logger.LogDebug("Fetching lane by id: {Id}", id);
            _logger.LogWarning("Lane not found: {Id}", id);
            return Task.FromResult<Lane?>(null);
        }

        public Task<System.Guid> AddAsync(Lane lane)
        {
            _logger.LogInformation("Added lane: {Id}", lane.LaneId);
            return Task.FromResult(System.Guid.Empty);
        }

        public Task<bool> UpdateAsync(Lane lane)
        {
            _logger.LogWarning("Update failed, lane not found: {Id}", lane.LaneId);
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(System.Guid id)
        {
            _logger.LogWarning("Delete failed, lane not found: {Id}", id);
            return Task.FromResult(false);
        }
    }
}
