using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoadController : ControllerBase
    {
        private readonly ILoadRepository _repository;
        private readonly ILogger<LoadController> _logger;

        public LoadController(ILoadRepository repository, ILogger<LoadController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("Getting all loads");
            var loads = await _repository.GetAllAsync();
            _logger.LogInformation("Returned {Count} loads", loads?.Count() ?? 0);
            return Ok(loads);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogDebug("Getting load by id: {Id}", id);
            var load = await _repository.GetByIdAsync(id);
            if (load is null)
            {
                _logger.LogWarning("Load not found: {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returned load: {Id}", id);
            return Ok(load);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Load load)
        {
            _logger.LogDebug("Creating new load");
            var id = await _repository.AddAsync(load);
            _logger.LogInformation("Created load with id: {Id}", id);
            return CreatedAtAction(nameof(GetById), new { id }, load);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Load load)
        {
            if (id != load.LoadId)
            {
                _logger.LogWarning("Update failed: route id {RouteId} does not match load id {LoadId}", id, load.LoadId);
                return BadRequest();
            }
            _logger.LogDebug("Updating load: {Id}", id);
            var updated = await _repository.UpdateAsync(load);
            if (updated)
            {
                _logger.LogInformation("Updated load: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Load not found for update: {Id}", id);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogDebug("Deleting load: {Id}", id);
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
            {
                _logger.LogInformation("Deleted load: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Load not found for delete: {Id}", id);
            return NotFound();
        }
    }
}

