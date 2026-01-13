using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerRepository repository, ILogger<CustomerController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("Getting all customers");
            var customers = await _repository.GetAllAsync();
            _logger.LogInformation("Returned {Count} customers", customers?.Count() ?? 0);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogDebug("Getting customer by id: {Id}", id);
            var customer = await _repository.GetByIdAsync(id);
            if (customer is null)
            {
                _logger.LogWarning("Customer not found: {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returned customer: {Id}", id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            _logger.LogDebug("Creating new customer");
            var id = await _repository.AddAsync(customer);
            _logger.LogInformation("Created customer with id: {Id}", id);
            return CreatedAtAction(nameof(GetById), new { id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                _logger.LogWarning("Update failed: route id {RouteId} does not match customer id {CustomerId}", id, customer.CustomerId);
                return BadRequest();
            }
            _logger.LogDebug("Updating customer: {Id}", id);
            var updated = await _repository.UpdateAsync(customer);
            if (updated)
            {
                _logger.LogInformation("Updated customer: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Customer not found for update: {Id}", id);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogDebug("Deleting customer: {Id}", id);
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
            {
                _logger.LogInformation("Deleted customer: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("Customer not found for delete: {Id}", id);
            return NotFound();
        }
    }
}
