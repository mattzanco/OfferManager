using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _repository;
        private readonly ILogger<LocationController> _logger;
        public LocationController(ILocationRepository repository, ILogger<LocationController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("Getting all locations");
            var locations = await _repository.GetAllAsync();
            _logger.LogInformation("Returned {Count} locations", locations?.Count() ?? 0);
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogDebug("Getting location by id: {Id}", id);
            var location = await _repository.GetByIdAsync(id);
            if (location is null)
            {
                _logger.LogWarning("Location not found: {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returned location: {Id}", id);
            return Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Location location)
        {
            _logger.LogDebug("Creating new location");
            var id = await _repository.AddAsync(location);
            _logger.LogInformation("Created location with id: {Id}", id);
            return CreatedAtAction(nameof(GetById), new { id }, location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Location location)
        {
            if (id != location.LocationId)
            {
                _logger.LogWarning("Update failed: route id {RouteId} does not match location id {LocationId}", id, location.LocationId);
                return BadRequest();
            }
            _logger.LogDebug("Updating location: {Id}", id);
            var updated = await _repository.UpdateAsync(location);
            if (updated)
            {
                _logger.LogInformation("Updated location: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Location not found for update: {Id}", id);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogDebug("Deleting location: {Id}", id);
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
            {
                _logger.LogInformation("Deleted location: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Location not found for delete: {Id}", id);
            return NotFound();
        }
    }
}
