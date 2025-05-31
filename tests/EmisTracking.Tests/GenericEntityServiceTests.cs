using AutoMapper;
using EmisTracking.Services.Exceptions;
using EmisTracking.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace EmisTracking.Tests
{
    public class GenericEntityServiceTests
    {
        private readonly Mock<IRepository<TestEntity>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TestEntityService _service;

        public GenericEntityServiceTests()
        {
            _repositoryMock = new Mock<IRepository<TestEntity>>();
            _mapperMock = new Mock<IMapper>();
            _service = new TestEntityService(_repositoryMock.Object, _mapperMock.Object);
        }


        [Fact]
        public async Task GetAllAsync_ShouldReturnEntities_WhenNoPredicate()
        {
            // Arrange
            var testData = new List<TestEntity>
             {
                new TestEntity { Id = "1", Name = "Test" }
             }.AsQueryable();

            var mockQueryable = testData.BuildMock();

            var repositoryMock = new Mock<IRepository<TestEntity>>();
            var mapperMock = new Mock<IMapper>();

            repositoryMock.Setup(r => r.GetAll(null)).Returns(mockQueryable);

            var service = new TestEntityService(repositoryMock.Object, mapperMock.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            result.Should().HaveCount(1);
            result.First().Name.Should().Be("Test");
        }


        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenIdIsValid()
        {
            // Arrange
            var entity = new TestEntity { Id = "1", Name = "Test" };
            _repositoryMock.Setup(r => r.GetByIdAsync("1", It.IsAny<Expression<Func<TestEntity, object>>[]>()))
            .ReturnsAsync(entity);

            // Act
            var result = await _service.GetByIdAsync("1", true);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be("1");
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepositoryCreate_WhenValidEntity()
        {
            // Arrange
            var entity = new TestEntity { Id = "1", Name = "Test" };
            _mapperMock.Setup(m => m.Map<TestEntity>(entity)).Returns(entity);
            _repositoryMock.Setup(r => r.CreateAsync(entity)).ReturnsAsync("1");

            // Act
            var result = await _service.AddAsync(entity);

            // Assert
            result.Should().Be("1");
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenEntityExists()
        {
            // Arrange
            var entity = new TestEntity { Id = "1", Name = "Updated" };
            _mapperMock.Setup(m => m.Map<TestEntity>(entity)).Returns(entity);
            _repositoryMock.Setup(r => r.UpdateAsync(entity)).ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(entity);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenEntityExists()
        {
            // Arrange
            _repositoryMock.Setup(r => r.DeleteAsync("1")).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync("1");

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task AddAsync_ShouldThrowBusinessLogicException_WhenValidationFails()
        {
            // Arrange
            var entity = new TestEntity { Id = "1", Name = null };

            // Act
            Func<Task> act = async () => await _service.AddAsync(entity);

            // Assert
            await act.Should().ThrowAsync<BusinessLogicException>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowBusinessLogicException_WhenRepositoryThrowsArgumentNullException()
        {
            // Act
            Func<Task> act = async () => await _service.GetByIdAsync(null, true);

            // Assert
            var exception = await act.Should().ThrowAsync<BusinessLogicException>();
            exception.Which.InnerException.Should().BeOfType<ArgumentNullException>();
            ((ArgumentNullException)exception.Which.InnerException!).ParamName.Should().Be("id");
        }

        [Fact]
        public async Task AddAsync_ShouldThrowBusinessLogicException_WhenRepositoryThrowsDbUpdateException()
        {
            // Arrange
            var entity = new TestEntity { Id = "1", Name = "Valid" };
            _mapperMock.Setup(m => m.Map<TestEntity>(entity)).Returns(entity);
            _repositoryMock.Setup(r => r.CreateAsync(entity)).ThrowsAsync(new DbUpdateException("DB error"));

            // Act
            Func<Task> act = async () => await _service.AddAsync(entity);

            // Assert
            var exception = await act.Should().ThrowAsync<BusinessLogicException>();
            exception.Which.InnerException.Should().BeOfType<DbUpdateException>();
            exception.Which.InnerException!.Message.Should().Be("DB error");
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowBusinessLogicException_WhenRepositoryThrowsArgumentNullException()
        {
            // Arrange
            var entity = new TestEntity { Id = "1", Name = "Valid" };
            _mapperMock.Setup(m => m.Map<TestEntity>(entity)).Returns(entity);
            _repositoryMock.Setup(r => r.UpdateAsync(entity)).ThrowsAsync(new ArgumentNullException("entity"));

            // Act
            Func<Task> act = async () => await _service.UpdateAsync(entity);

            // Assert
            var exception = await act.Should().ThrowAsync<BusinessLogicException>();
            exception.Which.InnerException.Should().BeOfType<ArgumentNullException>();
            ((ArgumentNullException)exception.Which.InnerException!).ParamName.Should().Be("entity");
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowBusinessLogicException_WhenIdIsNull()
        {
            // Act
            Func<Task> act = async () => await _service.DeleteAsync(null);

            // Assert
            var exception = await act.Should().ThrowAsync<BusinessLogicException>();
            exception.Which.InnerException.Should().BeOfType<ArgumentNullException>();
            ((ArgumentNullException)exception.Which.InnerException!).ParamName.Should().Be("id");
        }
    }
}
