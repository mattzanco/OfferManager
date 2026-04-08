using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/Offer")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferRepository _repository;
        private readonly ILogger<OfferController> _logger;
        public OfferController(IOfferRepository repository, ILogger<OfferController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("Getting all offers");
            var offers = await _repository.GetAllAsync();
            _logger.LogInformation("Returned {Count} offers", offers?.Count() ?? 0);
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogDebug("Getting offer by id: {Id}", id);
            var offer = await _repository.GetByIdAsync(id);
            if (offer is null)
            {
                _logger.LogWarning("Offer not found: {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returned offer: {Id}", id);
            return Ok(offer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Offer offer)
        {
            _logger.LogDebug("Creating new offer");
            var createdId = await _repository.AddAsync(offer);
            offer.Id = createdId;
            _logger.LogInformation("Created offer: {Id}", createdId);
            return CreatedAtAction(nameof(GetById), new { id = createdId }, offer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Offer offer)
        {
            _logger.LogDebug("Updating offer: {Id}", id);
            offer.Id = id;
            var success = await _repository.UpdateAsync(offer);
            if (!success)
                return NotFound();
            _logger.LogInformation("Updated offer: {Id}", id);
            return Ok(offer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogDebug("Deleting offer: {Id}", id);
            var success = await _repository.DeleteAsync(id);
            if (!success)
                return NotFound();
            _logger.LogInformation("Deleted offer: {Id}", id);
            return NoContent();
        }
    }
}
