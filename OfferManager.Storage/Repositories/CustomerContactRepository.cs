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
    public class CustomerContactRepository : ICustomerContactRepository
    {
        private readonly string _connectionString;
        private readonly Microsoft.Extensions.Logging.ILogger<CustomerContactRepository> _logger;

        public CustomerContactRepository(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger<CustomerContactRepository> logger)
        {
            _connectionString = configuration["DbConnectionString"];
            _logger = logger;
        }

        public async Task<IEnumerable<CustomerContact>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all customer contacts");
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.CustomerContact";
            return await connection.QueryAsync<CustomerContact>(sql);
        }

        public async Task<CustomerContact?> GetByIdAsync(Guid id)
        {
            _logger.LogDebug("Fetching customer contact by id: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.CustomerContact WHERE ContactId = @Id";
            return await connection.QuerySingleOrDefaultAsync<CustomerContact>(sql, new { Id = id });
        }

        public async Task<Guid> AddAsync(CustomerContact contact)
        {
            _logger.LogInformation("Added customer contact: {Id}", contact.ContactId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"INSERT INTO offermanager.CustomerContact (ContactId, OrganizationId, CustomerId, Name, Email, Phone, Title, IsPrimary, CreatedAt)
                                 VALUES (@ContactId, @OrganizationId, @CustomerId, @Name, @Email, @Phone, @Title, @IsPrimary, @CreatedAt)";
            if (contact.ContactId == Guid.Empty)
                contact.ContactId = Guid.NewGuid();
            if (contact.CreatedAt == default)
                contact.CreatedAt = DateTime.UtcNow;
            await connection.ExecuteAsync(sql, contact);
            return contact.ContactId;
        }

        public async Task<bool> UpdateAsync(CustomerContact contact)
        {
            _logger.LogWarning("Update failed, customer contact not found: {Id}", contact.ContactId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"UPDATE offermanager.CustomerContact SET OrganizationId = @OrganizationId, CustomerId = @CustomerId, Name = @Name, Email = @Email, Phone = @Phone, Title = @Title, IsPrimary = @IsPrimary WHERE ContactId = @ContactId";
            var rows = await connection.ExecuteAsync(sql, contact);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogWarning("Delete failed, customer contact not found: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM offermanager.CustomerContact WHERE ContactId = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
