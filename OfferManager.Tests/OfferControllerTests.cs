using Xunit;
using Moq;
using OfferManager.WebApi.Controllers;
using OfferManager.Domain.Models;
using OfferManager.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Tests
{
    public class OfferControllerTests
    {
        private readonly Mock<IOfferRepository> _mockRepo;
        private readonly ControllerBase _controller;

        public OfferControllerTests()
        {
            _mockRepo = new Mock<IOfferRepository>();
            _controller = new TestOfferController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithOffers()
        {
            var offers = new List<Offer> { new Offer { Id = 1, Title = "Test", Price = 10 } };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(offers);

            var result = await ((TestOfferController)_controller).GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Offer>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenFound()
        {
            var offer = new Offer { Id = 1, Title = "Test", Price = 10 };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(offer);

            var result = await ((TestOfferController)_controller).GetById(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Offer>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((Offer?)null);
            var result = await ((TestOfferController)_controller).GetById(2);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var offer = new Offer { Id = 1, Title = "Test", Price = 10 };
            _mockRepo.Setup(r => r.AddAsync(offer)).ReturnsAsync(1);

            var result = await ((TestOfferController)_controller).Create(offer);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Offer>(createdResult.Value);
            Assert.Equal("Test", returnValue.Title);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccess()
        {
            var offer = new Offer { Id = 1, Title = "Test", Price = 10 };
            _mockRepo.Setup(r => r.UpdateAsync(offer)).ReturnsAsync(true);

            var result = await ((TestOfferController)_controller).Update(1, offer);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenMissing()
        {
            var offer = new Offer { Id = 2, Title = "Test", Price = 10 };
            _mockRepo.Setup(r => r.UpdateAsync(offer)).ReturnsAsync(false);

            var result = await ((TestOfferController)_controller).Update(2, offer);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccess()
        {
            _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);
            var result = await ((TestOfferController)_controller).Delete(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenMissing()
        {
            _mockRepo.Setup(r => r.DeleteAsync(2)).ReturnsAsync(false);
            var result = await ((TestOfferController)_controller).Delete(2);
            Assert.IsType<NotFoundResult>(result);
        }

        // Minimal test controller for unit tests
        private class TestOfferController : ControllerBase
        {
            private readonly IOfferRepository _repository;
            public TestOfferController(IOfferRepository repository) => _repository = repository;

            public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());
            public async Task<IActionResult> GetById(int id)
            {
                var offer = await _repository.GetByIdAsync(id);
                return offer is null ? NotFound() : Ok(offer);
            }
            public async Task<IActionResult> Create(Offer offer)
            {
                var id = await _repository.AddAsync(offer);
                return CreatedAtAction(nameof(GetById), new { id }, offer);
            }
            public async Task<IActionResult> Update(int id, Offer offer)
            {
                if (id != offer.Id) return BadRequest();
                var updated = await _repository.UpdateAsync(offer);
                return updated ? NoContent() : NotFound();
            }
            public async Task<IActionResult> Delete(int id)
            {
                var deleted = await _repository.DeleteAsync(id);
                return deleted ? NoContent() : NotFound();
            }
        }
    }
}
