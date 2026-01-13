using Microsoft.Extensions.Logging;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly Microsoft.Extensions.Logging.ILogger<RoleRepository> _logger;

        public RoleRepository(Microsoft.Extensions.Logging.ILogger<RoleRepository> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<Role>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all roles");
            return Task.FromResult<IEnumerable<Role>>(new List<Role>());
        }

        public Task<Role?> GetByIdAsync(int id)
        {
            _logger.LogDebug("Fetching role by id: {Id}", id);
            _logger.LogWarning("Role not found: {Id}", id);
            return Task.FromResult<Role?>(null);
        }

        public Task<int> AddAsync(Role role)
        {
            _logger.LogInformation("Added role: {RoleId}", role.RoleId);
            return Task.FromResult(0);
        }

        public Task<bool> UpdateAsync(Role role)
        {
            _logger.LogWarning("Update failed, role not found: {RoleId}", role.RoleId);
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(int id)
        {
            _logger.LogWarning("Delete failed, role not found: {Id}", id);
            return Task.FromResult(false);
        }
    }
}
