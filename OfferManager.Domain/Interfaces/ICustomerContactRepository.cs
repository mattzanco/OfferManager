using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface ICustomerContactRepository
    {
        Task<IEnumerable<CustomerContact>> GetAllAsync();
        Task<CustomerContact?> GetByIdAsync(int id);
        Task<int> AddAsync(CustomerContact contact);
        Task<bool> UpdateAsync(CustomerContact contact);
        Task<bool> DeleteAsync(int id);
    }
}

