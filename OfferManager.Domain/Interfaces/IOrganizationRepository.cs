using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAllAsync();
        Task<Organization?> GetByIdAsync(int id);
        Task<int> AddAsync(Organization organization);
        Task<bool> UpdateAsync(Organization organization);
        Task<bool> DeleteAsync(int id);
    }
}

