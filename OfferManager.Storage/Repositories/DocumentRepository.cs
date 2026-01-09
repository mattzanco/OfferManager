using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        public Task<IEnumerable<Document>> GetAllAsync() => Task.FromResult<IEnumerable<Document>>(new List<Document>());
        public Task<Document?> GetByIdAsync(System.Guid id) => Task.FromResult<Document?>(null);
        public Task<System.Guid> AddAsync(Document document) => Task.FromResult(System.Guid.Empty);
        public Task<bool> UpdateAsync(Document document) => Task.FromResult(false);
        public Task<bool> DeleteAsync(System.Guid id) => Task.FromResult(false);
    }
}
