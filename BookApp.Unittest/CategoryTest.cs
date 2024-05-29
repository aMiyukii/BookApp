using BookApp.Core.DTO;
using BookApp.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookApp.Unittest.Fake_Repositories;

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
        var categoryToAdd = new CategoryDTO(5, "Horror", false);

        // Act
        await _categoryService.AddCategoryAsync(categoryToAdd);
        var categories = await _categoryService.GetAllCategoriesAsync();

        // Assert
        Assert.IsTrue(categories.Any(c => c.Id == categoryToAdd.Id));
    }

    [TestMethod]
    public async Task UpdateCategoryAsync_ShouldUpdateCategory()
    {
        // Arrange
        var categoryId = 1;
        var newName = "Updated Fiction";

        // Act
        var categoryToUpdate = await _categoryService.GetCategoryByIdAsync(categoryId);
        categoryToUpdate.Name = newName;
        await _categoryService.UpdateCategoryAsync(categoryToUpdate);
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
