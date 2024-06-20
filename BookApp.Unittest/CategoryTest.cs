using System.Linq;
using System.Threading.Tasks;
using BookApp.Core.DTO;
using BookApp.Core.Services;
using BookApp.Unittest.Fake_Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CategoryTest
    {
        private CategoryService _categoryService;

        [TestInitialize]
        public void Setup()
        {
            var fakeRepository = new FakeCategoryRepository();
            _categoryService = new CategoryService(fakeRepository);
        }

        [TestMethod]
        public async Task GetAllCategoriesAsync_ShouldReturnAllCategories()
        {
            // Act
            var categories = await _categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.AreEqual(4, categories.Count);
        }

        [TestMethod]
        public async Task GetCategoryByIdAsync_ExistingId_ShouldReturnCategory()
        {
            // Arrange
            int categoryId = 1;

            // Act
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);

            // Assert
            Assert.IsNotNull(category);
            Assert.AreEqual(categoryId, category.Id);
        }

        [TestMethod]
        public async Task GetCategoryByIdAsync_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            int categoryId = 99;

            // Act
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);

            // Assert
            Assert.IsNull(category);
        }

        [TestMethod]
        public async Task AddCategoryAsync_ShouldAddCategory()
        {
            // Arrange
            var categoryToAdd = new CategoryDTO(0, "Horror", false);

            // Act
            await _categoryService.AddCategoryAsync(categoryToAdd);
            var categories = await _categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.AreEqual(5, categories.Count);
            Assert.IsTrue(categories.Any(c => c.Name == "Horror"));
        }

        [TestMethod]
        public async Task UpdateCategoryAsync_ShouldUpdateCategory()
        {
            // Arrange
            var categoryId = 1;
            var newName = "Updated Fiction";

            // Act
            await _categoryService.UpdateCategoryNameAsync(categoryId, newName);
            var updatedCategory = await _categoryService.GetCategoryByIdAsync(categoryId);

            // Assert
            Assert.IsNotNull(updatedCategory);
            Assert.AreEqual(newName, updatedCategory.Name);
        }

        [TestMethod]
        public async Task DeleteCategoryAsync_ShouldDeleteCategory()
        {
            // Arrange
            int categoryId = 1;

            // Act
            await _categoryService.DeleteCategoryAsync(categoryId);
            var deletedCategory = await _categoryService.GetCategoryByIdAsync(categoryId);

            // Assert
            Assert.IsNull(deletedCategory);
        }
    }
}
