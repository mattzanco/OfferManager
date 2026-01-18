using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAllAsync();
        Task<Document?> GetByIdAsync(int id);
        Task<int> AddAsync(Document document);
        Task<bool> UpdateAsync(Document document);
        Task<bool> DeleteAsync(int id);
    }
}

