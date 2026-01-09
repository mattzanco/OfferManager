using Xunit;
using OfferManager.Storage.Repositories;
using OfferManager.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OfferManager.Tests
{
    public class OfferRepositoryTests
    {
        private OfferRepository CreateRepository() => new OfferRepository();

        [Fact]
        public async Task GetAllAsync_ReturnsList()
        {
            var repo = CreateRepository();
            var offers = await repo.GetAllAsync();
            Assert.NotNull(offers);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNullForMissing()
        {
            var repo = CreateRepository();
            var offer = await repo.GetByIdAsync(-1);
            Assert.Null(offer);
        }
    }
}
