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
    public class LoadRepository : ILoadRepository
    {
        private readonly string _connectionString;

        public LoadRepository(IConfiguration configuration)
        {
            _connectionString = configuration["DbConnectionString"];
        }

        public async Task<IEnumerable<Load>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Load";
            return await connection.QueryAsync<Load>(sql);
        }

        public async Task<Load?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Load WHERE LoadId = @Id";
            return await connection.QuerySingleOrDefaultAsync<Load>(sql, new { Id = id });
        }

        public async Task<Guid> AddAsync(Load load)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"INSERT INTO offermanager.Load (LoadId, OrganizationId, OfferId, OfferRevisionId, CustomerId, OriginLocationId, DestinationLocationId, Status, PickupAt, DeliveryAt, ReferenceNumber, CreatedAt, UpdatedAt)
                                 VALUES (@LoadId, @OrganizationId, @OfferId, @OfferRevisionId, @CustomerId, @OriginLocationId, @DestinationLocationId, @Status, @PickupAt, @DeliveryAt, @ReferenceNumber, @CreatedAt, @UpdatedAt)";
            if (load.LoadId == Guid.Empty)
                load.LoadId = Guid.NewGuid();
            await connection.ExecuteAsync(sql, load);
            return load.LoadId;
        }

        public async Task<bool> UpdateAsync(Load load)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"UPDATE offermanager.Load SET OrganizationId = @OrganizationId, OfferId = @OfferId, OfferRevisionId = @OfferRevisionId, CustomerId = @CustomerId, OriginLocationId = @OriginLocationId, DestinationLocationId = @DestinationLocationId, Status = @Status, PickupAt = @PickupAt, DeliveryAt = @DeliveryAt, ReferenceNumber = @ReferenceNumber, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt WHERE LoadId = @LoadId";
            var rows = await connection.ExecuteAsync(sql, load);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM offermanager.Load WHERE LoadId = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
