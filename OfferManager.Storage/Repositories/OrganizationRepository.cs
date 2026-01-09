using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        public Task<IEnumerable<Organization>> GetAllAsync() => Task.FromResult<IEnumerable<Organization>>(new List<Organization>());
        public Task<Organization?> GetByIdAsync(System.Guid id) => Task.FromResult<Organization?>(null);
        public Task<System.Guid> AddAsync(Organization organization) => Task.FromResult(System.Guid.Empty);
        public Task<bool> UpdateAsync(Organization organization) => Task.FromResult(false);
        public Task<bool> DeleteAsync(System.Guid id) => Task.FromResult(false);
    }
}
