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

        public async Task<IEnumerable<Offer>> GetAllAsync()
        {
            return await Task.FromResult(_offers);
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_offers.Find(o => o.Id == id));
        }

        public async Task<int> AddAsync(Offer offer)
        {
            offer.Id = _nextId++;
            _offers.Add(offer);
            return await Task.FromResult(offer.Id);
        }

        public async Task<bool> UpdateAsync(Offer offer)
        {
            var existing = _offers.Find(o => o.Id == offer.Id);
            if (existing == null) return await Task.FromResult(false);
            existing.Title = offer.Title;
            existing.Description = offer.Description;
            existing.Price = offer.Price;
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var offer = _offers.Find(o => o.Id == id);
            if (offer == null) return await Task.FromResult(false);
            _offers.Remove(offer);
            return await Task.FromResult(true);
        }
    }
}
