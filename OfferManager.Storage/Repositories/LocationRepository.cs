using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly Microsoft.Extensions.Logging.ILogger<LocationRepository> _logger;

        public LocationRepository(Microsoft.Extensions.Logging.ILogger<LocationRepository> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<Location>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all locations");
            return Task.FromResult<IEnumerable<Location>>(new List<Location>());
        }

        public Task<Location?> GetByIdAsync(System.Guid id)
        {
            _logger.LogDebug("Fetching location by id: {Id}", id);
            _logger.LogWarning("Location not found: {Id}", id);
            return Task.FromResult<Location?>(null);
        }

        public Task<System.Guid> AddAsync(Location location)
        {
            _logger.LogInformation("Added location: {Id}", location.LocationId);
            return Task.FromResult(System.Guid.Empty);
        }

        public Task<bool> UpdateAsync(Location location)
        {
            _logger.LogWarning("Update failed, location not found: {Id}", location.LocationId);
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(System.Guid id)
        {
            _logger.LogWarning("Delete failed, location not found: {Id}", id);
            return Task.FromResult(false);
        }
    }
}
