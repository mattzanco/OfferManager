using OfferManager.Domain.Interfaces;
using OfferManager.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace OfferManager.Storage.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly string _connectionString;

        public OfferRepository(IConfiguration configuration)
        {
            _connectionString = configuration["DbConnectionString"] ?? throw new KeyNotFoundException("DbConnectionString not found");
        }

        public async Task<IEnumerable<Offer>> GetAllAsync()
        {
            var offers = new List<Offer>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT Id, Title, Description, Price FROM Offers", conn);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                offers.Add(new Offer
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3)
                });
            }
            return offers;
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT Id, Title, Description, Price FROM Offers WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Offer
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Price = reader.GetDecimal(3)
                };
            }
            return null;
        }

        public async Task<int> AddAsync(Offer offer)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("INSERT INTO Offers (Title, Description, Price) OUTPUT INSERTED.Id VALUES (@Title, @Description, @Price)", conn);
            cmd.Parameters.AddWithValue("@Title", offer.Title);
            cmd.Parameters.AddWithValue("@Description", offer.Description);
            cmd.Parameters.AddWithValue("@Price", offer.Price);
            await conn.OpenAsync();
            var insertedId = (int)await cmd.ExecuteScalarAsync();
            return insertedId;
        }

        public async Task<bool> UpdateAsync(Offer offer)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("UPDATE Offers SET Title = @Title, Description = @Description, Price = @Price WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", offer.Id);
            cmd.Parameters.AddWithValue("@Title", offer.Title);
            cmd.Parameters.AddWithValue("@Description", offer.Description);
            cmd.Parameters.AddWithValue("@Price", offer.Price);
            await conn.OpenAsync();
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("DELETE FROM Offers WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }
    }
}
