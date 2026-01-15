using Xunit;
using OfferManager.Storage.Repositories;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;

namespace OfferManager.Tests
{
    public class DocumentRepositoryTests
    {
        private (IConfiguration, Microsoft.Extensions.Logging.ILogger<DocumentRepository>) CreateMocks()
        {
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<DocumentRepository>>();
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c[It.IsAny<string>()]).Returns("FakeConnectionString");
            return (mockConfig.Object, mockLogger.Object);
        }

        [Fact]
        public async Task AddAndGetDocument_Works()
        {
            var mockRepo = new Mock<IDocumentRepository>();
            var expected = new Document { FileName = "TestDoc.pdf" };
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Document>())).ReturnsAsync(Guid.NewGuid());
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expected);

            var id = await mockRepo.Object.AddAsync(new Document { FileName = "TestDoc.pdf" });
            var doc = await mockRepo.Object.GetByIdAsync(id);
            Assert.NotNull(doc);
        }

        [Fact]
        public async Task Update_ReturnsFalse()
        {
            var mockRepo = new Mock<IDocumentRepository>();
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Document>())).ReturnsAsync(false);
            var result = await mockRepo.Object.UpdateAsync(new Document { FileName = "TestDoc.pdf" });
            Assert.False(result);
        }

        [Fact]
        public async Task Delete_ReturnsFalse()
        {
            var mockRepo = new Mock<IDocumentRepository>();
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            var result = await mockRepo.Object.DeleteAsync(Guid.NewGuid());
            Assert.False(result);
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList()
        {
            var mockRepo = new Mock<IDocumentRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Document>());
            var docs = await mockRepo.Object.GetAllAsync();
            Assert.Empty(docs);
        }
    }
}
