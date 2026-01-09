using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<IEnumerable<User>> GetAllAsync() => Task.FromResult<IEnumerable<User>>(new List<User>());
        public Task<User?> GetByIdAsync(int id) => Task.FromResult<User?>(null);
        public Task<int> AddAsync(User user) => Task.FromResult(0);
        public Task<bool> UpdateAsync(User user) => Task.FromResult(false);
        public Task<bool> DeleteAsync(int id) => Task.FromResult(false);
    }
}
