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
        private readonly List<Offer> _offers = new();
        private int _nextId = 1;
        private readonly Microsoft.Extensions.Logging.ILogger<OfferRepository> _logger;

        public OfferRepository(Microsoft.Extensions.Logging.ILogger<OfferRepository> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Offer>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all offers");
            return await Task.FromResult(_offers);
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            _logger.LogDebug("Fetching offer by id: {Id}", id);
            var offer = _offers.Find(o => o.Id == id);
            if (offer == null)
                _logger.LogWarning("Offer not found: {Id}", id);
            else
                _logger.LogInformation("Offer found: {Id}", id);
            return await Task.FromResult(offer);
        }

        public async Task<int> AddAsync(Offer offer)
        {
            offer.Id = _nextId++;
            _offers.Add(offer);
            _logger.LogInformation("Added offer: {Id}", offer.Id);
            return await Task.FromResult(offer.Id);
        }

        public async Task<bool> UpdateAsync(Offer offer)
        {
            var existing = _offers.Find(o => o.Id == offer.Id);
            if (existing == null)
            {
                _logger.LogWarning("Update failed, offer not found: {Id}", offer.Id);
                return await Task.FromResult(false);
            }
            existing.Title = offer.Title;
            existing.Description = offer.Description;
            existing.Price = offer.Price;
            _logger.LogInformation("Updated offer: {Id}", offer.Id);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var offer = _offers.Find(o => o.Id == id);
            if (offer == null)
            {
                _logger.LogWarning("Delete failed, offer not found: {Id}", id);
                return await Task.FromResult(false);
            }
            _offers.Remove(offer);
            _logger.LogInformation("Deleted offer: {Id}", id);
            return await Task.FromResult(true);
        }
    }
}
