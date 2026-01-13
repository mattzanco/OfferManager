using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _repository;
        private readonly ILogger<DocumentController> _logger;
        public DocumentController(IDocumentRepository repository, ILogger<DocumentController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("Getting all documents");
            var documents = await _repository.GetAllAsync();
            _logger.LogInformation("Returned {Count} documents", documents?.Count() ?? 0);
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogDebug("Getting document by id: {Id}", id);
            var document = await _repository.GetByIdAsync(id);
            if (document is null)
            {
                _logger.LogWarning("Document not found: {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returned document: {Id}", id);
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Document document)
        {
            _logger.LogDebug("Creating new document");
            var id = await _repository.AddAsync(document);
            _logger.LogInformation("Created document with id: {Id}", id);
            return CreatedAtAction(nameof(GetById), new { id }, document);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Document document)
        {
            if (id != document.DocumentId)
            {
                _logger.LogWarning("Update failed: route id {RouteId} does not match document id {DocumentId}", id, document.DocumentId);
                return BadRequest();
            }
            _logger.LogDebug("Updating document: {Id}", id);
            var updated = await _repository.UpdateAsync(document);
            if (updated)
            {
                _logger.LogInformation("Updated document: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Document not found for update: {Id}", id);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogDebug("Deleting document: {Id}", id);
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
            {
                _logger.LogInformation("Deleted document: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Document not found for delete: {Id}", id);
            return NotFound();
        }
    }
}
