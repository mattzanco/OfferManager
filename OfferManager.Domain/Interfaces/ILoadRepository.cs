using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface ILoadRepository
    {
        Task<IEnumerable<Load>> GetAllAsync();
        Task<Load?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(Load load);
        Task<bool> UpdateAsync(Load load);
        Task<bool> DeleteAsync(Guid id);
    }
}
