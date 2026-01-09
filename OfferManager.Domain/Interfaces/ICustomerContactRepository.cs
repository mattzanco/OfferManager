using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface ICustomerContactRepository
    {
        Task<IEnumerable<CustomerContact>> GetAllAsync();
        Task<CustomerContact?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(CustomerContact contact);
        Task<bool> UpdateAsync(CustomerContact contact);
        Task<bool> DeleteAsync(Guid id);
    }
}
