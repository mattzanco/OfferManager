using Microsoft.Extensions.Logging;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly Microsoft.Extensions.Logging.ILogger<OrganizationRepository> _logger;

        public OrganizationRepository(Microsoft.Extensions.Logging.ILogger<OrganizationRepository> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<Organization>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all organizations");
            return Task.FromResult<IEnumerable<Organization>>(new List<Organization>());
        }

        public Task<Organization?> GetByIdAsync(System.Guid id)
        {
            _logger.LogDebug("Fetching organization by id: {Id}", id);
            _logger.LogWarning("Organization not found: {Id}", id);
            return Task.FromResult<Organization?>(null);
        }

        public Task<System.Guid> AddAsync(Organization organization)
        {
            _logger.LogInformation("Added organization: {Id}", organization.OrganizationId);
            return Task.FromResult(System.Guid.Empty);
        }

        public Task<bool> UpdateAsync(Organization organization)
        {
            _logger.LogWarning("Update failed, organization not found: {Id}", organization.OrganizationId);
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(System.Guid id)
        {
            _logger.LogWarning("Delete failed, organization not found: {Id}", id);
            return Task.FromResult(false);
        }
    }
}
