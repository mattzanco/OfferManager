using Xunit;
using OfferManager.Storage.Repositories;
using OfferManager.Domain.Models;
using System.Threading.Tasks;
using System;

namespace OfferManager.Tests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public async Task AddAndGetCustomer_Works()
        {
            var repo = new CustomerRepository();
            var customer = new Customer { CustomerId = Guid.NewGuid(), Name = "Acme" };
            await repo.AddAsync(customer);
            var result = await repo.GetByIdAsync(customer.CustomerId);
            Assert.NotNull(result);
            Assert.Equal("Acme", result!.Name);
        }
    }
}
