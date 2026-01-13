using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        private readonly Microsoft.Extensions.Logging.ILogger<UserRepository> _logger;

        public UserRepository(Microsoft.Extensions.Logging.ILogger<UserRepository> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all users");
            return Task.FromResult<IEnumerable<User>>(_users);
        }

        public Task<User?> GetByIdAsync(int id)
        {
            _logger.LogDebug("Fetching user by id: {Id}", id);
            var user = _users.Find(u => u.Id == id);
            if (user == null)
                _logger.LogWarning("User not found: {Id}", id);
            else
                _logger.LogInformation("User found: {Id}", id);
            return Task.FromResult(user);
        }

        public Task<int> AddAsync(User user)
        {
            _users.Add(user);
            _logger.LogInformation("Added user: {Id}", user.Id);
            return Task.FromResult(user.Id);
        }

        public Task<bool> UpdateAsync(User user)
        {
            var existing = _users.Find(u => u.Id == user.Id);
            if (existing == null)
            {
                _logger.LogWarning("Update failed, user not found: {Id}", user.Id);
                return Task.FromResult(false);
            }
            existing.Username = user.Username;
            existing.Email = user.Email;
            _logger.LogInformation("Updated user: {Id}", user.Id);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user == null)
            {
                _logger.LogWarning("Delete failed, user not found: {Id}", id);
                return Task.FromResult(false);
            }
            _users.Remove(user);
            _logger.LogInformation("Deleted user: {Id}", id);
            return Task.FromResult(true);
        }
    }
}
