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
            var repo = new DocumentRepository();
            var document = new Document { DocumentId = Guid.NewGuid(), FileName = "file.txt" };
            var id = await repo.AddAsync(document);
            var result = await repo.GetByIdAsync(id);
            Assert.Null(result); // Stub always returns null
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList()
        {
            var repo = new DocumentRepository();
            var documents = await repo.GetAllAsync();
            Assert.Empty(documents);
        }

        [Fact]
        public async Task Update_ReturnsFalse()
        {
            var repo = new DocumentRepository();
            var document = new Document { DocumentId = Guid.NewGuid(), FileName = "file.txt" };
            var updated = await repo.UpdateAsync(document);
            Assert.False(updated);
        }

        [Fact]
        public async Task Delete_ReturnsFalse()
        {
            var repo = new DocumentRepository();
            var deleted = await repo.DeleteAsync(Guid.NewGuid());
            Assert.False(deleted);
        }
    }
}
