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
    public class DocumentRepository : IDocumentRepository
    {
        private readonly string? _connectionString;
        private readonly Microsoft.Extensions.Logging.ILogger<DocumentRepository> _logger;

        public DocumentRepository(IConfiguration configuration, Microsoft.Extensions.Logging.ILogger<DocumentRepository> logger)
        {
            _connectionString = configuration["DbConnectionString"];
            _logger = logger;
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all documents");
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Document";
            return await connection.QueryAsync<Document>(sql);
        }

        public async Task<Document?> GetByIdAsync(int id)
        {
            _logger.LogDebug("Fetching document by id: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "SELECT * FROM offermanager.Document WHERE DocumentId = @Id";
            return await connection.QuerySingleOrDefaultAsync<Document>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Document document)
        {
            _logger.LogInformation("Added document: {Id}", document.DocumentId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"INSERT INTO offermanager.Document (OrganizationId, EntityType, EntityId, FileName, ContentType, StorageProvider, StorageKey, UploadedByUserId, UploadedAt)
                                 VALUES (@OrganizationId, @EntityType, @EntityId, @FileName, @ContentType, @StorageProvider, @StorageKey, @UploadedByUserId, @UploadedAt); SELECT CAST(SCOPE_IDENTITY() as int);";
            if (document.UploadedAt == default)
                document.UploadedAt = DateTime.UtcNow;
            var id = await connection.ExecuteScalarAsync<int>(sql, document);
            return id;
        }

        public async Task<bool> UpdateAsync(Document document)
        {
            _logger.LogWarning("Update failed, document not found: {Id}", document.DocumentId);
            using var connection = new SqlConnection(_connectionString);
            const string sql = @"UPDATE offermanager.Document SET OrganizationId = @OrganizationId, EntityType = @EntityType, EntityId = @EntityId, FileName = @FileName, ContentType = @ContentType, StorageProvider = @StorageProvider, StorageKey = @StorageKey, UploadedByUserId = @UploadedByUserId WHERE DocumentId = @DocumentId";
            var rows = await connection.ExecuteAsync(sql, document);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogWarning("Delete failed, document not found: {Id}", id);
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM offermanager.Document WHERE DocumentId = @Id";
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}

