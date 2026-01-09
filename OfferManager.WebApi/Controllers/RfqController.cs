using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RfqController : ControllerBase
    {
        private readonly IRfqRepository _repository;
        public RfqController(IRfqRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rfq = await _repository.GetByIdAsync(id);
            return rfq is null ? NotFound() : Ok(rfq);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rfq rfq)
        {
            var id = await _repository.AddAsync(rfq);
            return CreatedAtAction(nameof(GetById), new { id }, rfq);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Rfq rfq)
        {
            if (id != rfq.RfqId) return BadRequest();
            var updated = await _repository.UpdateAsync(rfq);
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
