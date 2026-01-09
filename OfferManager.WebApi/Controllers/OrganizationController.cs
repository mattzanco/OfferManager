using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _repository;
        public OrganizationController(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var organization = await _repository.GetByIdAsync(id);
            return organization is null ? NotFound() : Ok(organization);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Organization organization)
        {
            var id = await _repository.AddAsync(organization);
            return CreatedAtAction(nameof(GetById), new { id }, organization);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Organization organization)
        {
            if (id != organization.OrganizationId) return BadRequest();
            var updated = await _repository.UpdateAsync(organization);
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
