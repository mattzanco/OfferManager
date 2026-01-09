using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public Task<IEnumerable<User>> GetAllAsync() => Task.FromResult<IEnumerable<User>>(_users);

        public Task<User?> GetByIdAsync(int id) => Task.FromResult(_users.Find(u => u.Id == id));

        public Task<int> AddAsync(User user)
        {
            _users.Add(user);
            return Task.FromResult(user.Id);
        }

        public Task<bool> UpdateAsync(User user)
        {
            var existing = _users.Find(u => u.Id == user.Id);
            if (existing == null) return Task.FromResult(false);
            existing.Username = user.Username;
            existing.Email = user.Email;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user == null) return Task.FromResult(false);
            _users.Remove(user);
            return Task.FromResult(true);
        }
    }
}
