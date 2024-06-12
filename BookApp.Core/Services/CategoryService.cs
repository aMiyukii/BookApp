using BookApp.Core.DTO;
using BookApp.Core.Interfaces;

namespace BookApp.Core.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid category ID", nameof(id));
            }

            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task AddCategoryAsync(CategoryDTO category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (await CategoryExistsAsync(category.Name))
            {
                throw new InvalidOperationException("Category with the same name already exists.");
            }

            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryDTO category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (await CategoryExistsAsync(category.Name, category.Id))
            {
                throw new InvalidOperationException("Category with the same name already exists.");
            }

            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid category ID", nameof(id));
            }

            await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<bool> CategoryExistsAsync(string categoryName, int? categoryId = null)
        {
            var existingCategory = await _categoryRepository.GetCategoryByNameAsync(categoryName);
            return existingCategory != null && existingCategory.Id != categoryId;
        }

        public async Task SaveCategoryAsync(int userBookId, int categoryId1, int categoryId2)
        {
            await _categoryRepository.SaveCategoryAsync(userBookId, categoryId1, categoryId2);
        }
        
        public async Task<CategoryDTO> GetCategoryByNameAsync(string name)
        {
            return await _categoryRepository.GetCategoryByNameAsync(name);
        }
    }
}