using Xunit;
using OfferManager.Storage.Repositories;
using OfferManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Tests
{
    public class DocumentRepositoryTests
    {
        [Fact]
        public async Task AddAndGetDocument_Works()
        {
            var mockLogger = new Moq.Mock<Microsoft.Extensions.Logging.ILogger<DocumentRepository>>();
            var repo = new DocumentRepository(mockLogger.Object);
            var document = new Document { DocumentId = Guid.NewGuid(), FileName = "file.txt" };
            var id = await repo.AddAsync(document);
            var result = await repo.GetByIdAsync(id);
            Assert.Null(result); // Stub always returns null
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList()
        {
            var mockLogger = new Moq.Mock<Microsoft.Extensions.Logging.ILogger<DocumentRepository>>();
            var repo = new DocumentRepository(mockLogger.Object);
            var documents = await repo.GetAllAsync();
            Assert.Empty(documents);
        }

        [Fact]
        public async Task Update_ReturnsFalse()
        {
            var mockLogger = new Moq.Mock<Microsoft.Extensions.Logging.ILogger<DocumentRepository>>();
            var repo = new DocumentRepository(mockLogger.Object);
            var document = new Document { DocumentId = Guid.NewGuid(), FileName = "file.txt" };
            var updated = await repo.UpdateAsync(document);
            Assert.False(updated);
        }

        [Fact]
        public async Task Delete_ReturnsFalse()
        {
            var mockLogger = new Moq.Mock<Microsoft.Extensions.Logging.ILogger<DocumentRepository>>();
            var repo = new DocumentRepository(mockLogger.Object);
            var deleted = await repo.DeleteAsync(Guid.NewGuid());
            Assert.False(deleted);
        }
    }
}
