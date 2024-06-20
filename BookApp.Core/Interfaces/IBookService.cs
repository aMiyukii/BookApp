using BookApp.Core.DTO;

namespace BookApp.Core.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetAllAsync();
        Task<string> GetBookTitleByIdAsync(int bookId);
        Task<IEnumerable<CategoryDTO>> GetCategoriesByBookIdAsync(int userId, int bookId);
        Task AddBookToUserCollectionAsync(int userId, int bookId, int categoryId1, int? categoryId2 = null);
        Task<BookDTO> GetBookByTitleAsync(string title);
        Task DeleteBookByTitleAsync(string title, int userId);
        Task DeleteUserBookByBookIdAsync(int bookId, int userId);
        Task<List<BookDTO>> GetBooksInLibraryAsync();
        Task<List<BookDTO>> GetBooksByUserIdAsync(int userId);
    }
}