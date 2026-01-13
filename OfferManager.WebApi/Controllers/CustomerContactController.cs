using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerContactController : ControllerBase
    {
        private readonly ICustomerContactRepository _repository;
        private readonly ILogger<CustomerContactController> _logger;
        public CustomerContactController(ICustomerContactRepository repository, ILogger<CustomerContactController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("Getting all customer contacts");
            var contacts = await _repository.GetAllAsync();
            _logger.LogInformation("Returned {Count} customer contacts", contacts?.Count() ?? 0);
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogDebug("Getting customer contact by id: {Id}", id);
            var contact = await _repository.GetByIdAsync(id);
            if (contact is null)
            {
                _logger.LogWarning("Customer contact not found: {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returned customer contact: {Id}", id);
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerContact contact)
        {
            _logger.LogDebug("Creating new customer contact");
            var id = await _repository.AddAsync(contact);
            _logger.LogInformation("Created customer contact with id: {Id}", id);
            return CreatedAtAction(nameof(GetById), new { id }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CustomerContact contact)
        {
            if (id != contact.ContactId)
            {
                _logger.LogWarning("Update failed: route id {RouteId} does not match contact id {ContactId}", id, contact.ContactId);
                return BadRequest();
            }
            _logger.LogDebug("Updating customer contact: {Id}", id);
            var updated = await _repository.UpdateAsync(contact);
            if (updated)
            {
                _logger.LogInformation("Updated customer contact: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Customer contact not found for update: {Id}", id);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogDebug("Deleting customer contact: {Id}", id);
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
            {
                _logger.LogInformation("Deleted customer contact: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Customer contact not found for delete: {Id}", id);
            return NotFound();
        }
    }
}
