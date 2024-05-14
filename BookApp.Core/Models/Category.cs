using System.Collections.Generic;
using System.Threading.Tasks;
using BookApp.Core.DTO;
using BookApp.Core.Services;

namespace BookApp.Core.Models
{
    public class Category
    {
        private readonly ICategoryService _categoryService;

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStandard { get; set; }

        public Category(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _categoryService.GetAllCategoriesAsync();
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            return await _categoryService.GetCategoryByIdAsync(id);
        }

        public async Task AddCategoryAsync(CategoryDTO category)
        {
            await _categoryService.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(CategoryDTO category)
        {
            await _categoryService.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
        }
    }
}