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
    public class LaneRepository : ILaneRepository
    {
        private readonly string? _connectionString;
        private readonly Microsoft.Extensions.Logging.ILogger<LaneRepository> _logger;

        public LaneRepository(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger<LaneRepository> logger)
        {
            _connectionString = configuration["DbConnectionString"];
            _logger = logger;
        }

        public async Task<IEnumerable<Lane>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all lanes");
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Lane";
            return await connection.QueryAsync<Lane>(sql);
        }

        public async Task<Lane?> GetByIdAsync(int id)
        {
            _logger.LogDebug("Fetching lane by id: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Lane WHERE LaneId = @Id";
            return await connection.QuerySingleOrDefaultAsync<Lane>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Lane lane)
        {
            _logger.LogInformation("Added lane: {Id}", lane.LaneId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"INSERT INTO offermanager.Lane (OrganizationId, OriginLocationId, DestinationLocationId, LaneCode, DistanceMiles, CreatedAt)
                                 VALUES (@OrganizationId, @OriginLocationId, @DestinationLocationId, @LaneCode, @DistanceMiles, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            if (lane.CreatedAt == default)
                lane.CreatedAt = DateTime.UtcNow;
            var id = await connection.ExecuteScalarAsync<int>(sql, lane);
            return id;
        }

        public async Task<bool> UpdateAsync(Lane lane)
        {
            _logger.LogWarning("Update failed, lane not found: {Id}", lane.LaneId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"UPDATE offermanager.Lane SET OrganizationId = @OrganizationId, OriginLocationId = @OriginLocationId, DestinationLocationId = @DestinationLocationId, LaneCode = @LaneCode, DistanceMiles = @DistanceMiles WHERE LaneId = @LaneId";
            var rows = await connection.ExecuteAsync(sql, lane);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogWarning("Delete failed, lane not found: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM offermanager.Lane WHERE LaneId = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}

