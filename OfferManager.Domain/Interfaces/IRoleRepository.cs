using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(int id);
        Task<int> AddAsync(Role role);
        Task<bool> UpdateAsync(Role role);
        Task<bool> DeleteAsync(int id);
    }
}
