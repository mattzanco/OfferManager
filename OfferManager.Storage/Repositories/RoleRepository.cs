using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string? _connectionString;
        private readonly Microsoft.Extensions.Logging.ILogger<RoleRepository> _logger;

        public RoleRepository(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger<RoleRepository> logger)
        {
            _connectionString = configuration["DbConnectionString"];
            _logger = logger;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all roles");
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Role";
            return await connection.QueryAsync<Role>(sql);
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            _logger.LogDebug("Fetching role by id: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Role WHERE RoleId = @Id";
            return await connection.QuerySingleOrDefaultAsync<Role>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Role role)
        {
            _logger.LogInformation("Added role: {RoleId}", role.RoleId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"INSERT INTO offermanager.Role (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await connection.ExecuteScalarAsync<int>(sql, role);
            return id;
        }

        public async Task<bool> UpdateAsync(Role role)
        {
            _logger.LogWarning("Update failed, role not found: {RoleId}", role.RoleId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"UPDATE offermanager.Role SET Name = @Name WHERE RoleId = @RoleId";
            var rows = await connection.ExecuteAsync(sql, role);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogWarning("Delete failed, role not found: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM offermanager.Role WHERE RoleId = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}

