using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface ILaneRepository
    {
        Task<IEnumerable<Lane>> GetAllAsync();
        Task<Lane?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(Lane lane);
        Task<bool> UpdateAsync(Lane lane);
        Task<bool> DeleteAsync(Guid id);
    }
}
