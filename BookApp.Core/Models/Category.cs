using System.Collections.Generic;
using System.Threading.Tasks;
using BookApp.Core.DTO;
using BookApp.Core.Services;

namespace BookApp.Core.Models
{
    public class Category
    {
        private readonly ICategoryRepository _categoryRepository;

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStandard { get; set; }

        public Category(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task AddCategoryAsync(CategoryDTO category)
        {
            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryDTO category)
        {
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}