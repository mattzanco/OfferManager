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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;
        private readonly Microsoft.Extensions.Logging.ILogger<CustomerRepository> _logger;

        public CustomerRepository(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger<CustomerRepository> logger)
        {
            _connectionString = configuration["DbConnectionString"];
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all customers");
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Customer";
            return await connection.QueryAsync<Customer>(sql);
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            _logger.LogDebug("Fetching customer by id: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Customer WHERE CustomerId = @Id";
            var customer = await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { Id = id });
            if (customer == null)
                _logger.LogWarning("Customer not found: {Id}", id);
            else
                _logger.LogInformation("Customer found: {Id}", id);
            return customer;
        }

        public async Task<Guid> AddAsync(Customer customer)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"INSERT INTO offermanager.Customer (CustomerId, OrganizationId, Name, AccountCode, BillingTerms, Status, CreatedAt)
                                 VALUES (@CustomerId, @OrganizationId, @Name, @AccountCode, @BillingTerms, @Status, @CreatedAt)";
            if (customer.CustomerId == Guid.Empty)
                customer.CustomerId = Guid.NewGuid();
            if (customer.CreatedAt == default)
                customer.CreatedAt = DateTime.UtcNow;
            await connection.ExecuteAsync(sql, customer);
            _logger.LogInformation("Added customer: {Id}", customer.CustomerId);
            return customer.CustomerId;
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"UPDATE offermanager.Customer SET OrganizationId = @OrganizationId, Name = @Name, AccountCode = @AccountCode, BillingTerms = @BillingTerms, Status = @Status WHERE CustomerId = @CustomerId";
            var rows = await connection.ExecuteAsync(sql, customer);
            if (rows > 0)
                _logger.LogInformation("Updated customer: {Id}", customer.CustomerId);
            else
                _logger.LogWarning("Update failed, customer not found: {Id}", customer.CustomerId);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM offermanager.Customer WHERE CustomerId = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            if (rows > 0)
                _logger.LogInformation("Deleted customer: {Id}", id);
            else
                _logger.LogWarning("Delete failed, customer not found: {Id}", id);
            return rows > 0;
        }
    }
}
