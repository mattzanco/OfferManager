using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAllAsync();
        Task<Document?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(Document document);
        Task<bool> UpdateAsync(Document document);
        Task<bool> DeleteAsync(Guid id);
    }
}
