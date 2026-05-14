using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly string? _connectionString;

        // OfferId -> Id: table column name does not match the domain property, so Dapper would leave Id at 0 with SELECT *.
        private const string SelectColumns = @"
            OfferId AS Id,
            OrganizationId,
            RfqId,
            CustomerId,
            Status,
            CurrentRevisionId,
            CreatedByUserId,
            CreatedAt,
            UpdatedAt";

        public OfferRepository(IConfiguration configuration)
        {
            _connectionString = configuration["DbConnectionString"];
        }

        public async Task<IEnumerable<Offer>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = $"SELECT {SelectColumns} FROM offermanager.Offer";
            return await connection.QueryAsync<Offer>(sql);
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = $"SELECT {SelectColumns} FROM offermanager.Offer WHERE OfferId = @Id";
            return await connection.QuerySingleOrDefaultAsync<Offer>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Offer offer)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"INSERT INTO offermanager.Offer (OrganizationId, RfqId, CustomerId, Status, CurrentRevisionId, CreatedByUserId, CreatedAt, UpdatedAt)
                                 VALUES (@OrganizationId, @RfqId, @CustomerId, @Status, @CurrentRevisionId, @CreatedByUserId, @CreatedAt, @UpdatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            if (offer.CreatedAt == default)
                offer.CreatedAt = DateTime.UtcNow;
            if (offer.UpdatedAt == default)
                offer.UpdatedAt = DateTime.UtcNow;
            var id = await connection.ExecuteScalarAsync<int>(sql, offer);
            return id;
        }

        public async Task<bool> UpdateAsync(Offer offer)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"UPDATE offermanager.Offer SET OrganizationId = @OrganizationId, RfqId = @RfqId, CustomerId = @CustomerId, Status = @Status, CurrentRevisionId = @CurrentRevisionId, CreatedByUserId = @CreatedByUserId, UpdatedAt = @UpdatedAt WHERE OfferId = @Id";
            offer.UpdatedAt = DateTime.UtcNow;
            var rows = await connection.ExecuteAsync(sql, offer);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM offermanager.Offer WHERE OfferId = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}

