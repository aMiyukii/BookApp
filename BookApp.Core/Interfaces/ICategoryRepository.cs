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
        Task<CategoryDTO> GetCategoryByNameAsync(string name);
        Task SaveCategoryAsync(int userBookId, int categoryId1, int categoryId2);
        Task<List<CategoryDTO>> GetCategoriesByUserIdAsync(int userId);
        Task<List<CategoryDTO>> GetStandardCategoriesAsync();
    }
}