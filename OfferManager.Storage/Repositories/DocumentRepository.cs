using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly Microsoft.Extensions.Logging.ILogger<DocumentRepository> _logger;

        public DocumentRepository(Microsoft.Extensions.Logging.ILogger<DocumentRepository> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<Document>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all documents");
            return Task.FromResult<IEnumerable<Document>>(new List<Document>());
        }

        public Task<Document?> GetByIdAsync(System.Guid id)
        {
            _logger.LogDebug("Fetching document by id: {Id}", id);
            _logger.LogWarning("Document not found: {Id}", id);
            return Task.FromResult<Document?>(null);
        }

        public Task<System.Guid> AddAsync(Document document)
        {
            _logger.LogInformation("Added document: {Id}", document.DocumentId);
            return Task.FromResult(System.Guid.Empty);
        }

        public Task<bool> UpdateAsync(Document document)
        {
            _logger.LogWarning("Update failed, document not found: {Id}", document.DocumentId);
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(System.Guid id)
        {
            _logger.LogWarning("Delete failed, document not found: {Id}", id);
            return Task.FromResult(false);
        }
    }
}
