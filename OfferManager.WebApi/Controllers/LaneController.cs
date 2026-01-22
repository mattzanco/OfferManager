using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/Lane")]
    public class LaneController : ControllerBase
    {
        private readonly ILaneRepository _repository;
        private readonly ILogger<LaneController> _logger;
        public LaneController(ILaneRepository repository, ILogger<LaneController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("Getting all lanes");
            var lanes = await _repository.GetAllAsync();
            _logger.LogInformation("Returned {Count} lanes", lanes?.Count() ?? 0);
            return Ok(lanes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogDebug("Getting lane by id: {Id}", id);
            var lane = await _repository.GetByIdAsync(id);
            if (lane is null)
            {
                _logger.LogWarning("Lane not found: {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returned lane: {Id}", id);
            return Ok(lane);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lane lane)
        {
            _logger.LogDebug("Creating new lane");
            var id = await _repository.AddAsync(lane);
            _logger.LogInformation("Created lane with id: {Id}", id);
            return CreatedAtAction(nameof(GetById), new { id }, lane);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Lane lane)
        {
            if (id != lane.LaneId)
            {
                _logger.LogWarning("Update failed: route id {RouteId} does not match lane id {LaneId}", id, lane.LaneId);
                return BadRequest();
            }
            _logger.LogDebug("Updating lane: {Id}", id);
            var updated = await _repository.UpdateAsync(lane);
            if (updated)
            {
                _logger.LogInformation("Updated lane: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Lane not found for update: {Id}", id);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogDebug("Deleting lane: {Id}", id);
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
            {
                _logger.LogInformation("Deleted lane: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Lane not found for delete: {Id}", id);
            return NotFound();
        }
    }
}

