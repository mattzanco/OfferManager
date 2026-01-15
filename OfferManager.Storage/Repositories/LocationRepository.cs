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
    public class LocationRepository : ILocationRepository
    {
        private readonly string _connectionString;
        private readonly Microsoft.Extensions.Logging.ILogger<LocationRepository> _logger;

        public LocationRepository(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger<LocationRepository> logger)
        {
            _connectionString = configuration["DbConnectionString"];
            _logger = logger;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all locations");
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Location";
            return await connection.QueryAsync<Location>(sql);
        }

        public async Task<Location?> GetByIdAsync(Guid id)
        {
            _logger.LogDebug("Fetching location by id: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Location WHERE LocationId = @Id";
            return await connection.QuerySingleOrDefaultAsync<Location>(sql, new { Id = id });
        }

        public async Task<Guid> AddAsync(Location location)
        {
            _logger.LogInformation("Added location: {Id}", location.LocationId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"INSERT INTO offermanager.Location (LocationId, OrganizationId, Name, Address1, Address2, City, StateProvince, PostalCode, Country, Latitude, Longitude, CreatedAt)
                                 VALUES (@LocationId, @OrganizationId, @Name, @Address1, @Address2, @City, @StateProvince, @PostalCode, @Country, @Latitude, @Longitude, @CreatedAt)";
            if (location.LocationId == Guid.Empty)
                location.LocationId = Guid.NewGuid();
            if (location.CreatedAt == default)
                location.CreatedAt = DateTime.UtcNow;
            await connection.ExecuteAsync(sql, location);
            return location.LocationId;
        }

        public async Task<bool> UpdateAsync(Location location)
        {
            _logger.LogWarning("Update failed, location not found: {Id}", location.LocationId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"UPDATE offermanager.Location SET OrganizationId = @OrganizationId, Name = @Name, Address1 = @Address1, Address2 = @Address2, City = @City, StateProvince = @StateProvince, PostalCode = @PostalCode, Country = @Country, Latitude = @Latitude, Longitude = @Longitude WHERE LocationId = @LocationId";
            var rows = await connection.ExecuteAsync(sql, location);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogWarning("Delete failed, location not found: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM offermanager.Location WHERE LocationId = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
