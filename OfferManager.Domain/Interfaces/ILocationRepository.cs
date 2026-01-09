using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(Location location);
        Task<bool> UpdateAsync(Location location);
        Task<bool> DeleteAsync(Guid id);
    }
}
