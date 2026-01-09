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
        public DocumentController(IDocumentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var document = await _repository.GetByIdAsync(id);
            return document is null ? NotFound() : Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Document document)
        {
            var id = await _repository.AddAsync(document);
            return CreatedAtAction(nameof(GetById), new { id }, document);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Document document)
        {
            if (id != document.DocumentId) return BadRequest();
            var updated = await _repository.UpdateAsync(document);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
