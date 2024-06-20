using BookApp.Core.DTO;
using BookApp.Core.Interfaces;

namespace BookApp.Unittest.Fake_Repositories
{
    public class FakeCategoryRepository : ICategoryRepository
    {
        private readonly List<CategoryDTO> _categories = new List<CategoryDTO>();

        public FakeCategoryRepository()
        {
            _categories.Add(new CategoryDTO(1, "Fiction", true));
            _categories.Add(new CategoryDTO(2, "Non-Fiction", true));
            _categories.Add(new CategoryDTO(3, "Science", false));
            _categories.Add(new CategoryDTO(4, "Fantasy", false));
        }

        public Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return Task.FromResult(_categories);
        }

        public Task AddCategoryAsync(CategoryDTO category)
        {
            category.Id = _categories.Max(c => c.Id) + 1;
            _categories.Add(category);
            return Task.CompletedTask;
        }

        public Task DeleteCategoryAsync(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _categories.Remove(category);
            }
            return Task.CompletedTask;
        }

        public Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(category);
        }

        public Task UpdateCategoryAsync(CategoryDTO category)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.IsStandard = category.IsStandard;
            }
            return Task.CompletedTask;
        }

        public Task SaveCategoryAsync(int userBookId, int categoryId1, int categoryId2)
        {
            return Task.CompletedTask;
        }

        public Task<CategoryDTO> GetCategoryByNameAsync(string name)
        {
            var category = _categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(category);
        }
    }
}
