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
        public LoadController(ILoadRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var load = await _repository.GetByIdAsync(id);
            return load is null ? NotFound() : Ok(load);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Load load)
        {
            var id = await _repository.AddAsync(load);
            return CreatedAtAction(nameof(GetById), new { id }, load);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Load load)
        {
            if (id != load.LoadId) return BadRequest();
            var updated = await _repository.UpdateAsync(load);
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
