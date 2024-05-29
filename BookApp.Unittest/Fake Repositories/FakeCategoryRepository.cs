using BookApp.Core.DTO;
using BookApp.Core.Interfaces;

namespace BookApp.Unittest.Fake_Repositories
{
    public class FakeCategoryRepository : ICategoryRepository
    {
        private readonly List<CategoryDTO> _categories;

        public FakeCategoryRepository()
        {
            _categories = new List<CategoryDTO>
            {
                new CategoryDTO(1, "Fiction", true),
                new CategoryDTO(2, "Non-Fiction", true),
                new CategoryDTO(3, "Science Fiction", false),
                new CategoryDTO(4, "Mystery", false)
            };
        }

        public Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return Task.FromResult(_categories);
        }

        public Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            return Task.FromResult(_categories.Find(c => c.Id == id));
        }

        public Task AddCategoryAsync(CategoryDTO category)
        {
            _categories.Add(category);
            return Task.CompletedTask;
        }

        public Task UpdateCategoryAsync(CategoryDTO category)
        {
            var existingCategory = _categories.Find(c => c.Id == category.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.IsStandard = category.IsStandard;
            }
            return Task.CompletedTask;
        }

        public Task DeleteCategoryAsync(int id)
        {
            _categories.RemoveAll(c => c.Id == id);
            return Task.CompletedTask;
        }
    }
}
