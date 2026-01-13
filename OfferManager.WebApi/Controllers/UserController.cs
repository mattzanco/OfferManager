using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepository repository, ILogger<UserController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogDebug("Getting all users");
            var users = await _repository.GetAllAsync();
            _logger.LogInformation("Returned {Count} users", users?.Count() ?? 0);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogDebug("Getting user by id: {Id}", id);
            var user = await _repository.GetByIdAsync(id);
            if (user is null)
            {
                _logger.LogWarning("User not found: {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returned user: {Id}", id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            _logger.LogDebug("Creating new user");
            var id = await _repository.AddAsync(user);
            _logger.LogInformation("Created user with id: {Id}", id);
            return CreatedAtAction(nameof(GetById), new { id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.Id)
            {
                _logger.LogWarning("Update failed: route id {RouteId} does not match user id {UserId}", id, user.Id);
                return BadRequest();
            }
            _logger.LogDebug("Updating user: {Id}", id);
            var updated = await _repository.UpdateAsync(user);
            if (updated)
            {
                _logger.LogInformation("Updated user: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("User not found for update: {Id}", id);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogDebug("Deleting user: {Id}", id);
            var deleted = await _repository.DeleteAsync(id);
            if (deleted)
            {
                _logger.LogInformation("Deleted user: {Id}", id);
                return NoContent();
            }
            _logger.LogWarning("User not found for delete: {Id}", id);
            return NotFound();
        }
    }
}
