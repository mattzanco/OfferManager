using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Task<IEnumerable<Role>> GetAllAsync() => Task.FromResult<IEnumerable<Role>>(new List<Role>());
        public Task<Role?> GetByIdAsync(int id) => Task.FromResult<Role?>(null);
        public Task<int> AddAsync(Role role) => Task.FromResult(0);
        public Task<bool> UpdateAsync(Role role) => Task.FromResult(false);
        public Task<bool> DeleteAsync(int id) => Task.FromResult(false);
    }
}
