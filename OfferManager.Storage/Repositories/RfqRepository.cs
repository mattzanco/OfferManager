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
    public class RfqRepository : IRfqRepository
    {
        private readonly string? _connectionString;
        private readonly Microsoft.Extensions.Logging.ILogger<RfqRepository> _logger;

        public RfqRepository(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger<RfqRepository> logger)
        {
            _connectionString = configuration["DbConnectionString"];
            _logger = logger;
        }

        public async Task<IEnumerable<Rfq>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all RFQs");
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Rfq";
            return await connection.QueryAsync<Rfq>(sql);
        }

        public async Task<Rfq?> GetByIdAsync(int id)
        {
            _logger.LogDebug("Fetching RFQ by id: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Rfq WHERE RfqId = @Id";
            return await connection.QuerySingleOrDefaultAsync<Rfq>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Rfq rfq)
        {
            _logger.LogInformation("Added RFQ: {Id}", rfq.RfqId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"INSERT INTO offermanager.Rfq (OrganizationId, CustomerId, RequestedByContactId, OriginLocationId, DestinationLocationId, Mode, EquipmentType, ServiceLevel, PickupEarliestAt, PickupLatestAt, DeliveryEarliestAt, DeliveryLatestAt, Commodity, WeightLbs, PalletCount, PieceCount, Hazmat, TemperatureControlled, Notes, Status, CreatedByUserId, CreatedAt, UpdatedAt)
                                 VALUES (@OrganizationId, @CustomerId, @RequestedByContactId, @OriginLocationId, @DestinationLocationId, @Mode, @EquipmentType, @ServiceLevel, @PickupEarliestAt, @PickupLatestAt, @DeliveryEarliestAt, @DeliveryLatestAt, @Commodity, @WeightLbs, @PalletCount, @PieceCount, @Hazmat, @TemperatureControlled, @Notes, @Status, @CreatedByUserId, @CreatedAt, @UpdatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            if (rfq.CreatedAt == default)
                rfq.CreatedAt = DateTime.UtcNow;
            if (rfq.UpdatedAt == default)
                rfq.UpdatedAt = DateTime.UtcNow;
            var id = await connection.ExecuteScalarAsync<int>(sql, rfq);
            return id;
        }

        public async Task<bool> UpdateAsync(Rfq rfq)
        {
            _logger.LogWarning("Update failed, RFQ not found: {Id}", rfq.RfqId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"UPDATE offermanager.Rfq SET OrganizationId = @OrganizationId, CustomerId = @CustomerId, RequestedByContactId = @RequestedByContactId, OriginLocationId = @OriginLocationId, DestinationLocationId = @DestinationLocationId, Mode = @Mode, EquipmentType = @EquipmentType, ServiceLevel = @ServiceLevel, PickupEarliestAt = @PickupEarliestAt, PickupLatestAt = @PickupLatestAt, DeliveryEarliestAt = @DeliveryEarliestAt, DeliveryLatestAt = @DeliveryLatestAt, Commodity = @Commodity, WeightLbs = @WeightLbs, PalletCount = @PalletCount, PieceCount = @PieceCount, Hazmat = @Hazmat, TemperatureControlled = @TemperatureControlled, Notes = @Notes, Status = @Status, CreatedByUserId = @CreatedByUserId, UpdatedAt = @UpdatedAt WHERE RfqId = @RfqId";
            rfq.UpdatedAt = DateTime.UtcNow;
            var rows = await connection.ExecuteAsync(sql, rfq);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogWarning("Delete failed, RFQ not found: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM offermanager.Rfq WHERE RfqId = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}

