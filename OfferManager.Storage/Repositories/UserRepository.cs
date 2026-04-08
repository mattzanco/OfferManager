using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

namespace OfferManager.Storage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string? _connectionString;
        private readonly Microsoft.Extensions.Logging.ILogger<UserRepository> _logger;

        public UserRepository(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger<UserRepository> logger)
        {
            _connectionString = configuration["DbConnectionString"];
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.[User]";
            _logger.LogDebug("Executing SQL: {Sql}", sql);
            var users = await connection.QueryAsync<User>(sql);
            _logger.LogInformation("Fetched {Count} users", users.AsList().Count);
            return users;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.[User] WHERE UserId = @Id";
            _logger.LogDebug("Executing SQL: {Sql}, Parameters: {Id}", sql, id);
            var user = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
            if (user == null)
                _logger.LogWarning("User not found: {Id}", id);
            else
                _logger.LogInformation("User found: {Id}", id);
            return user;
        }

        public async Task<int> AddAsync(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"INSERT INTO offermanager.[User] (Username, Email, CreatedDate, UpdatedDate) VALUES (@Username, @Email, @CreatedDate, @UpdatedDate); SELECT CAST(SCOPE_IDENTITY() as int);";
            user.CreatedDate = DateTime.UtcNow;
            user.UpdatedDate = DateTime.UtcNow;
            _logger.LogDebug("Executing SQL: {Sql}, Parameters: {@User}", sql, user);
            var id = await connection.ExecuteScalarAsync<int>(sql, user);
            _logger.LogInformation("Added user: {Id}", id);
            return id;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"UPDATE offermanager.[User] SET Username = @Username, Email = @Email, UpdatedDate = @UpdatedDate WHERE UserId = @Id";
            user.UpdatedDate = DateTime.UtcNow;
            _logger.LogDebug("Executing SQL: {Sql}, Parameters: {@User}", sql, user);
            var rows = await connection.ExecuteAsync(sql, user);
            if (rows > 0)
                _logger.LogInformation("Updated user: {Id}", user.Id);
            else
                _logger.LogWarning("Update failed, user not found: {Id}", user.Id);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM offermanager.[User] WHERE UserId = @Id";
            _logger.LogDebug("Executing SQL: {Sql}, Parameters: {Id}", sql, id);
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            if (rows > 0)
                _logger.LogInformation("Deleted user: {Id}", id);
            else
                _logger.LogWarning("Delete failed, user not found: {Id}", id);
            return rows > 0;
        }
    }
}

