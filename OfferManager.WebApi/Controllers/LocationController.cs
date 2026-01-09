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
        public LocationController(ILocationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var location = await _repository.GetByIdAsync(id);
            return location is null ? NotFound() : Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Location location)
        {
            var id = await _repository.AddAsync(location);
            return CreatedAtAction(nameof(GetById), new { id }, location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Location location)
        {
            if (id != location.LocationId) return BadRequest();
            var updated = await _repository.UpdateAsync(location);
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
