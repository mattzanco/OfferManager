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
        private readonly ILogger<OrganizationController> _logger;
        public OrganizationController(IOrganizationRepository repository, ILogger<OrganizationController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("Getting all organizations");
            var organizations = await _repository.GetAllAsync();
            _logger.LogInformation("Returned {Count} organizations", organizations?.Count() ?? 0);
            return Ok(organizations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogDebug("Getting organization by id: {Id}", id);
            var organization = await _repository.GetByIdAsync(id);
            if (organization is null)
            {
                _logger.LogWarning("Organization not found: {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returned organization: {Id}", id);
            return Ok(organization);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Organization organization)
        {
            _logger.LogDebug("Creating new organization");
            var id = await _repository.AddAsync(organization);
            _logger.LogInformation("Created organization with id: {Id}", id);
            return CreatedAtAction(nameof(GetById), new { id }, organization);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Organization organization)
        {
            if (id != organization.OrganizationId)
            {
                _logger.LogWarning("Update failed: route id {RouteId} does not match organization id {OrganizationId}", id, organization.OrganizationId);
                return BadRequest();
            }
            _logger.LogDebug("Updating organization: {Id}", id);
            var updated = await _repository.UpdateAsync(organization);
            if (updated)
            {
                _logger.LogInformation("Updated organization: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Organization not found for update: {Id}", id);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogDebug("Deleting organization: {Id}", id);
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
            {
                _logger.LogInformation("Deleted organization: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Organization not found for delete: {Id}", id);
            return NotFound();
        }
    }
}
