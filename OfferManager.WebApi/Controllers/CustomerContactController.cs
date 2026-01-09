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
        public CustomerContactController(ICustomerContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _repository.GetByIdAsync(id);
            return contact is null ? NotFound() : Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerContact contact)
        {
            var id = await _repository.AddAsync(contact);
            return CreatedAtAction(nameof(GetById), new { id }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CustomerContact contact)
        {
            if (id != contact.ContactId) return BadRequest();
            var updated = await _repository.UpdateAsync(contact);
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
