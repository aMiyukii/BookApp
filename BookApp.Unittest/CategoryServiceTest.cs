using Xunit;
using Moq;
using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using BookApp.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApp.UnitTests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly CategoryServices _categoryService;

        public CategoryServiceTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _categoryService = new CategoryServices(_mockCategoryRepository.Object);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsCategoryList()
        {
            // Arrange
            var mockCategories = new List<CategoryDTO>
            {
                new CategoryDTO { Id = 1, Name = "Category 1", IsStandard = true },
                new CategoryDTO { Id = 2, Name = "Category 2", IsStandard = false }
            };

            _mockCategoryRepository.Setup(repo => repo.GetAllCategoriesAsync()).ReturnsAsync(mockCategories);

            // Act
            var result = await _categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Category 1", result[0].Name);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsEmptyListWhenNoCategories()
        {
            // Arrange
            var mockCategories = new List<CategoryDTO>();

            _mockCategoryRepository.Setup(repo => repo.GetAllCategoriesAsync()).ReturnsAsync(mockCategories);

            // Act
            var result = await _categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ReturnsCategory()
        {
            // Arrange
            var categoryId = 1;
            var mockCategory = new CategoryDTO { Id = categoryId, Name = "Category 1", IsStandard = true };

            _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(categoryId)).ReturnsAsync(mockCategory);

            // Act
            var result = await _categoryService.GetCategoryByIdAsync(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal("Category 1", result.Name);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ReturnsNullWhenCategoryNotFound()
        {
            // Arrange
            var categoryId = 1;

            _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(categoryId)).ReturnsAsync((CategoryDTO)null);

            // Act
            var result = await _categoryService.GetCategoryByIdAsync(categoryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddCategoryAsync_AddsCategory()
        {
            // Arrange
            var newCategory = new CategoryDTO { Name = "New Category", IsStandard = true };

            _mockCategoryRepository.Setup(repo => repo.AddCategoryAsync(newCategory)).Returns(Task.CompletedTask);

            // Act
            await _categoryService.AddCategoryAsync(newCategory);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.AddCategoryAsync(newCategory), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_UpdatesCategory()
        {
            // Arrange
            var updatedCategory = new CategoryDTO { Id = 1, Name = "Updated Category", IsStandard = false };

            _mockCategoryRepository.Setup(repo => repo.UpdateCategoryAsync(updatedCategory)).Returns(Task.CompletedTask);

            // Act
            await _categoryService.UpdateCategoryAsync(updatedCategory);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.UpdateCategoryAsync(updatedCategory), Times.Once);
        }

        [Fact]
        public async Task DeleteCategoryAsync_DeletesCategory()
        {
            // Arrange
            var categoryId = 1;

            _mockCategoryRepository.Setup(repo => repo.DeleteCategoryAsync(categoryId)).Returns(Task.CompletedTask);

            // Act
            await _categoryService.DeleteCategoryAsync(categoryId);

            // Assert
            _mockCategoryRepository.Verify(repo => repo.DeleteCategoryAsync(categoryId), Times.Once);
        }

        [Fact]
        public async Task AddCategoryAsync_ThrowsExceptionWhenCategoryIsNull()
        {
            // Arrange
            CategoryDTO newCategory = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _categoryService.AddCategoryAsync(newCategory));
        }

        [Fact]
        public async Task UpdateCategoryAsync_ThrowsExceptionWhenCategoryIsNull()
        {
            // Arrange
            CategoryDTO updatedCategory = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _categoryService.UpdateCategoryAsync(updatedCategory));
        }

        [Fact]
        public async Task DeleteCategoryAsync_ThrowsExceptionWhenCategoryIdIsInvalid()
        {
            // Arrange
            var invalidCategoryId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.DeleteCategoryAsync(invalidCategoryId));
        }
    }
}
