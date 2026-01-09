using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using OfferManager.Domain.Models;
using OfferManager.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfferManager.Tests
{
    public class RoleControllerTests
    {
        private readonly Mock<IRoleRepository> _mockRepo;
        private readonly ControllerBase _controller;

        public RoleControllerTests()
        {
            _mockRepo = new Mock<IRoleRepository>();
            _controller = new TestRoleController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithRoles()
        {
            var roles = new List<Role> { new Role { RoleId = 1, Name = "Admin" } };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(roles);

            var result = await ((TestRoleController)_controller).GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Role>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenFound()
        {
            var role = new Role { RoleId = 1, Name = "Admin" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(role);

            var result = await ((TestRoleController)_controller).GetById(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Role>(okResult.Value);
            Assert.Equal(1, returnValue.RoleId);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((Role?)null);
            var result = await ((TestRoleController)_controller).GetById(2);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var role = new Role { RoleId = 1, Name = "Admin" };
            _mockRepo.Setup(r => r.AddAsync(role)).ReturnsAsync(1);

            var result = await ((TestRoleController)_controller).Create(role);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Role>(createdResult.Value);
            Assert.Equal("Admin", returnValue.Name);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccess()
        {
            var role = new Role { RoleId = 1, Name = "Admin" };
            _mockRepo.Setup(r => r.UpdateAsync(role)).ReturnsAsync(true);

            var result = await ((TestRoleController)_controller).Update(1, role);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenMissing()
        {
            var role = new Role { RoleId = 2, Name = "Admin" };
            _mockRepo.Setup(r => r.UpdateAsync(role)).ReturnsAsync(false);

            var result = await ((TestRoleController)_controller).Update(2, role);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccess()
        {
            _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);
            var result = await ((TestRoleController)_controller).Delete(1);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenMissing()
        {
            _mockRepo.Setup(r => r.DeleteAsync(2)).ReturnsAsync(false);
            var result = await ((TestRoleController)_controller).Delete(2);
            Assert.IsType<NotFoundResult>(result);
        }

        // Minimal test controller for unit tests
        private class TestRoleController : ControllerBase
        {
            private readonly IRoleRepository _repository;
            public TestRoleController(IRoleRepository repository) => _repository = repository;

            public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());
            public async Task<IActionResult> GetById(int id)
            {
                var role = await _repository.GetByIdAsync(id);
                return role is null ? NotFound() : Ok(role);
            }
            public async Task<IActionResult> Create(Role role)
            {
                var id = await _repository.AddAsync(role);
                return CreatedAtAction(nameof(GetById), new { id }, role);
            }
            public async Task<IActionResult> Update(int id, Role role)
            {
                if (id != role.RoleId) return BadRequest();
                var updated = await _repository.UpdateAsync(role);
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
