using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetById(Guid id)
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
            var createdOffer = await _repository.AddAsync(offer);
            _logger.LogInformation("Created offer: {Id}", createdOffer.Id);
            return CreatedAtAction(nameof(GetById), new { id = createdOffer.Id }, createdOffer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Offer offer)
        {
            _logger.LogDebug("Updating offer: {Id}", id);
            offer.Id = id;
            var updatedOffer = await _repository.UpdateAsync(offer);
            _logger.LogInformation("Updated offer: {Id}", id);
            return Ok(updatedOffer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogDebug("Deleting offer: {Id}", id);
            await _repository.DeleteAsync(id);
            _logger.LogInformation("Deleted offer: {Id}", id);
            return NoContent();
        }
    }
}
