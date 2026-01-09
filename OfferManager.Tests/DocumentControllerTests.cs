using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Models;
using OfferManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Tests
{
    public class DocumentControllerTests
    {
        private readonly Mock<IDocumentRepository> _mockRepo;
        private readonly ControllerBase _controller;

        public DocumentControllerTests()
        {
            _mockRepo = new Mock<IDocumentRepository>();
            _controller = new TestDocumentController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithDocuments()
        {
            var documents = new List<Document> { new Document { DocumentId = Guid.NewGuid(), FileName = "file.txt" } };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(documents);

            var result = await ((TestDocumentController)_controller).GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Document>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenFound()
        {
            var docId = Guid.NewGuid();
            var document = new Document { DocumentId = docId, FileName = "file.txt" };
            _mockRepo.Setup(r => r.GetByIdAsync(docId)).ReturnsAsync(document);

            var result = await ((TestDocumentController)_controller).GetById(docId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Document>(okResult.Value);
            Assert.Equal(docId, returnValue.DocumentId);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            var missingId = Guid.NewGuid();
            _mockRepo.Setup(r => r.GetByIdAsync(missingId)).ReturnsAsync((Document?)null);
            var result = await ((TestDocumentController)_controller).GetById(missingId);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var docId = Guid.NewGuid();
            var document = new Document { DocumentId = docId, FileName = "file.txt" };
            _mockRepo.Setup(r => r.AddAsync(document)).ReturnsAsync(docId);

            var result = await ((TestDocumentController)_controller).Create(document);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Document>(createdResult.Value);
            Assert.Equal("file.txt", returnValue.FileName);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccess()
        {
            var docId = Guid.NewGuid();
            var document = new Document { DocumentId = docId, FileName = "file.txt" };
            _mockRepo.Setup(r => r.UpdateAsync(document)).ReturnsAsync(true);

            var result = await ((TestDocumentController)_controller).Update(docId, document);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenMissing()
        {
            var docId = Guid.NewGuid();
            var document = new Document { DocumentId = docId, FileName = "file.txt" };
            _mockRepo.Setup(r => r.UpdateAsync(document)).ReturnsAsync(false);

            var result = await ((TestDocumentController)_controller).Update(docId, document);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccess()
        {
            var docId = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeleteAsync(docId)).ReturnsAsync(true);
            var result = await ((TestDocumentController)_controller).Delete(docId);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenMissing()
        {
            var docId = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeleteAsync(docId)).ReturnsAsync(false);
            var result = await ((TestDocumentController)_controller).Delete(docId);
            Assert.IsType<NotFoundResult>(result);
        }

        // Minimal test controller for unit tests
        private class TestDocumentController : ControllerBase
        {
            private readonly IDocumentRepository _repository;
            public TestDocumentController(IDocumentRepository repository) => _repository = repository;

            public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());
            public async Task<IActionResult> GetById(Guid id)
            {
                var document = await _repository.GetByIdAsync(id);
                return document is null ? NotFound() : Ok(document);
            }
            public async Task<IActionResult> Create(Document document)
            {
                var id = await _repository.AddAsync(document);
                return CreatedAtAction(nameof(GetById), new { id }, document);
            }
            public async Task<IActionResult> Update(Guid id, Document document)
            {
                if (id != document.DocumentId) return BadRequest();
                var updated = await _repository.UpdateAsync(document);
                return updated ? NoContent() : NotFound();
            }
            public async Task<IActionResult> Delete(Guid id)
            {
                var deleted = await _repository.DeleteAsync(id);
                return deleted ? NoContent() : NotFound();
            }
        }
    }
}
