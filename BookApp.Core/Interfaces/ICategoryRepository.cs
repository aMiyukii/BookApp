using BookApp.Core.DTO;

namespace BookApp.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CategoryDTO category);
        Task UpdateCategoryAsync(CategoryDTO category);
        Task DeleteCategoryAsync(int id);
        Task SaveCategoryAsync(int userBookId, int categoryId1, int categoryId2);
        Task<CategoryDTO> GetCategoryByNameAsync(string name);
    }
}