using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface IRfqRepository
    {
        Task<IEnumerable<Rfq>> GetAllAsync();
        Task<Rfq?> GetByIdAsync(int id);
        Task<int> AddAsync(Rfq rfq);
        Task<bool> UpdateAsync(Rfq rfq);
        Task<bool> DeleteAsync(int id);
    }
}

