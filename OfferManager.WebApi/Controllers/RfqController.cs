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
        private readonly ILogger<RfqController> _logger;
        public RfqController(IRfqRepository repository, ILogger<RfqController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("Getting all RFQs");
            var rfqs = await _repository.GetAllAsync();
            _logger.LogInformation("Returned {Count} RFQs", rfqs?.Count() ?? 0);
            return Ok(rfqs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogDebug("Getting RFQ by id: {Id}", id);
            var rfq = await _repository.GetByIdAsync(id);
            if (rfq is null)
            {
                _logger.LogWarning("RFQ not found: {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returned RFQ: {Id}", id);
            return Ok(rfq);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rfq rfq)
        {
            _logger.LogDebug("Creating new RFQ");
            var id = await _repository.AddAsync(rfq);
            _logger.LogInformation("Created RFQ with id: {Id}", id);
            return CreatedAtAction(nameof(GetById), new { id }, rfq);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Rfq rfq)
        {
            if (id != rfq.RfqId)
            {
                _logger.LogWarning("Update failed: route id {RouteId} does not match RFQ id {RfqId}", id, rfq.RfqId);
                return BadRequest();
            }
            _logger.LogDebug("Updating RFQ: {Id}", id);
            var updated = await _repository.UpdateAsync(rfq);
            if (updated)
            {
                _logger.LogInformation("Updated RFQ: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("RFQ not found for update: {Id}", id);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogDebug("Deleting RFQ: {Id}", id);
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
            {
                _logger.LogInformation("Deleted RFQ: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("RFQ not found for delete: {Id}", id);
            return NotFound();
        }
    }
}
