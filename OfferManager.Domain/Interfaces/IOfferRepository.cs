using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface IOfferRepository
    {
        Task<IEnumerable<Offer>> GetAllAsync();
        Task<Offer?> GetByIdAsync(int id);
        Task<int> AddAsync(Offer offer);
        Task<bool> UpdateAsync(Offer offer);
        Task<bool> DeleteAsync(int id);
    }
}
