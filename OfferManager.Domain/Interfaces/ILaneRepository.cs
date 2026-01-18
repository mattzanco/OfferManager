using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface ILaneRepository
    {
        Task<IEnumerable<Lane>> GetAllAsync();
        Task<Lane?> GetByIdAsync(int id);
        Task<int> AddAsync(Lane lane);
        Task<bool> UpdateAsync(Lane lane);
        Task<bool> DeleteAsync(int id);
    }
}

