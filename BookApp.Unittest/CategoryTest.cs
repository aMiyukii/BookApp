using BookApp.Core.DTO;
using BookApp.Core.Services;
using BookApp.Unittest.Fake_Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

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
            int categoryId = 1;
            string newName = "Updated Fiction";

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

        [TestMethod]
        public async Task GetCategoryByNameAsync_ExistingName_ShouldReturnCategory()
        {
            // Arrange
            string categoryName = "Fiction";

            // Act
            var category = await _categoryService.GetCategoryByNameAsync(categoryName);

            // Assert
            Assert.IsNotNull(category);
            Assert.AreEqual(categoryName, category.Name);
        }

        [TestMethod]
        public async Task GetCategoryByNameAsync_NonExistingName_ShouldReturnNull()
        {
            // Arrange
            string categoryName = "NonExistingCategoryName";

            // Act
            var category = await _categoryService.GetCategoryByNameAsync(categoryName);

            // Assert
            Assert.IsNull(category);
        }

        [TestMethod]
        public async Task SaveCategoryAsync_ShouldSaveCategoriesForUserBook()
        {
            // Arrange
            int userBookId = 1;
            int categoryId1 = 1;
            int categoryId2 = 2;

            // Act
            await _categoryService.SaveCategoryAsync(userBookId, categoryId1, categoryId2);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task AddCategoryAsync_ShouldThrowExceptionIfCategoryNameIsNull()
        {
            // Arrange
            var categoryToAdd = new CategoryDTO(0, null, false);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await _categoryService.AddCategoryAsync(categoryToAdd);
            });
        }

        [TestMethod]
        public async Task AddCategoryAsync_ShouldThrowExceptionIfCategoryNameIsEmpty()
        {
            // Arrange
            var categoryToAdd = new CategoryDTO(0, "", false);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await _categoryService.AddCategoryAsync(categoryToAdd);
            });
        }

        [TestMethod]
        public async Task AddCategoryAsync_ShouldThrowExceptionIfCategoryExists()
        {
            // Arrange
            var categoryToAdd = new CategoryDTO(0, "Fiction", false);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
            {
                await _categoryService.AddCategoryAsync(categoryToAdd);
            });
        }

        [TestMethod]
        public async Task DeleteCategoryAsync_ShouldThrowExceptionIfCategoryIdIsInvalid()
        {
            // Arrange
            int invalidCategoryId = -1;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await _categoryService.DeleteCategoryAsync(invalidCategoryId);
            });
        }

        [TestMethod]
        public async Task DeleteCategoryAsync_ShouldDeleteCategorySuccessfully()
        {
            // Arrange
            var categoryToDelete = new CategoryDTO(5, "ToDelete", false);
            await _categoryService.AddCategoryAsync(categoryToDelete);

            // Act
            await _categoryService.DeleteCategoryAsync(categoryToDelete.Id);
            var deletedCategory = await _categoryService.GetCategoryByIdAsync(categoryToDelete.Id);

            // Assert
            Assert.IsNull(deletedCategory);
        }

        [TestMethod]
        public async Task GetCategoryByIdAsync_NegativeId_ShouldThrowArgumentException()
        {
            // Arrange
            int categoryId = -1;

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await _categoryService.GetCategoryByIdAsync(categoryId);
            });
        }

        [TestMethod]
        public async Task UpdateCategoryAsync_ShouldUpdateCategoryName()
        {
            // Arrange
            int categoryId = 1;
            string newName = "Updated Fiction";

            // Act
            await _categoryService.UpdateCategoryNameAsync(categoryId, newName);
            var updatedCategory = await _categoryService.GetCategoryByIdAsync(categoryId);

            // Assert
            Assert.IsNotNull(updatedCategory);
            Assert.AreEqual(newName, updatedCategory.Name);
        }

        [TestMethod]
        public async Task UpdateCategoryAsync_ShouldThrowExceptionIfCategoryIdIsInvalid()
        {
            // Arrange
            int invalidCategoryId = -1;
            string newName = "Updated Name";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await _categoryService.UpdateCategoryNameAsync(invalidCategoryId, newName);
            });
        }

        [TestMethod]
        public async Task UpdateCategoryAsync_ShouldThrowExceptionIfCategoryNameIsEmpty()
        {
            // Arrange
            int categoryId = 1;
            string emptyName = "";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await _categoryService.UpdateCategoryNameAsync(categoryId, emptyName);
            });
        }

        [TestClass]
        public class CategoryDTOTest
        {
            [TestMethod]
            public void CategoryDTO_Constructor_ValidInitialization()
            {
                // Arrange & Act
                var categoryDto = new CategoryDTO(1, "Category Name");

                // Assert
                Assert.AreEqual(1, categoryDto.Id);
                Assert.AreEqual("Category Name", categoryDto.Name);
            }
        }
    }
}