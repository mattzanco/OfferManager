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
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly string? _connectionString;
        private readonly Microsoft.Extensions.Logging.ILogger<OrganizationRepository> _logger;

        public OrganizationRepository(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger<OrganizationRepository> logger)
        {
            _connectionString = configuration["DbConnectionString"];
            _logger = logger;
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all organizations");
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Organization";
            return await connection.QueryAsync<Organization>(sql);
        }

        public async Task<Organization?> GetByIdAsync(int id)
        {
            _logger.LogDebug("Fetching organization by id: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Organization WHERE OrganizationId = @Id";
            return await connection.QuerySingleOrDefaultAsync<Organization>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Organization organization)
        {
            _logger.LogInformation("Added organization: {Id}", organization.OrganizationId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"INSERT INTO offermanager.Organization (Name, CreatedAt)
                                 VALUES (@Name, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            if (organization.CreatedAt == default)
                organization.CreatedAt = DateTime.UtcNow;
            var id = await connection.ExecuteScalarAsync<int>(sql, organization);
            return id;
        }

        public async Task<bool> UpdateAsync(Organization organization)
        {
            _logger.LogWarning("Update failed, organization not found: {Id}", organization.OrganizationId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"UPDATE offermanager.Organization SET Name = @Name WHERE OrganizationId = @OrganizationId";
            var rows = await connection.ExecuteAsync(sql, organization);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogWarning("Delete failed, organization not found: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM offermanager.Organization WHERE OrganizationId = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}

