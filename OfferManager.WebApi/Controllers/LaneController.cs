using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaneController : ControllerBase
    {
        private readonly ILaneRepository _repository;
        public LaneController(ILaneRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var lane = await _repository.GetByIdAsync(id);
            return lane is null ? NotFound() : Ok(lane);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lane lane)
        {
            var id = await _repository.AddAsync(lane);
            return CreatedAtAction(nameof(GetById), new { id }, lane);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Lane lane)
        {
            if (id != lane.LaneId) return BadRequest();
            var updated = await _repository.UpdateAsync(lane);
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
