using Xunit;
using Moq;
using OfferManager.WebApi.Controllers;
using OfferManager.Domain.Models;
using OfferManager.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace OfferManager.Tests
{
    public class LocationControllerTests
    {
        private readonly Mock<ILocationRepository> _mockRepo;
        private readonly LocationController _controller;

        public LocationControllerTests()
        {
            _mockRepo = new Mock<ILocationRepository>();
            _controller = new LocationController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithLocations()
        {
            var locations = new List<Location> { new Location { LocationId = Guid.NewGuid(), Name = "Test" } };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(locations);

            var result = await _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Location>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenFound()
        {
            var locationId = Guid.NewGuid();
            var location = new Location { LocationId = locationId, Name = "Test" };
            _mockRepo.Setup(r => r.GetByIdAsync(locationId)).ReturnsAsync(location);

            var result = await _controller.GetById(locationId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Location>(okResult.Value);
            Assert.Equal(locationId, returnValue.LocationId);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            var missingId = Guid.NewGuid();
            _mockRepo.Setup(r => r.GetByIdAsync(missingId)).ReturnsAsync((Location?)null);
            var result = await _controller.GetById(missingId);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var location = new Location { LocationId = Guid.NewGuid(), Name = "Test" };
            _mockRepo.Setup(r => r.AddAsync(location)).ReturnsAsync(location.LocationId);

            var result = await _controller.Create(location);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Location>(createdResult.Value);
            Assert.Equal("Test", returnValue.Name);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccess()
        {
            var locationId = Guid.NewGuid();
            var location = new Location { LocationId = locationId, Name = "Test" };
            _mockRepo.Setup(r => r.UpdateAsync(location)).ReturnsAsync(true);

            var result = await _controller.Update(locationId, location);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenMissing()
        {
            var locationId = Guid.NewGuid();
            var location = new Location { LocationId = locationId, Name = "Test" };
            _mockRepo.Setup(r => r.UpdateAsync(location)).ReturnsAsync(false);

            var result = await _controller.Update(locationId, location);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccess()
        {
            var locationId = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeleteAsync(locationId)).ReturnsAsync(true);
            var result = await _controller.Delete(locationId);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenMissing()
        {
            var locationId = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeleteAsync(locationId)).ReturnsAsync(false);
            var result = await _controller.Delete(locationId);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
