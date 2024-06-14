using System.Collections.Generic;
using System.Threading.Tasks;
using BookApp.Core.DTO;

namespace BookApp.Core.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetAllAsync();
        Task<string> GetBookTitleByIdAsync(int bookId);
        Task<IEnumerable<CategoryDTO>> GetCategoriesByBookIdAsync(int bookId);
        Task AddBookToUserCollectionAsync(int bookId, int categoryId1, int? categoryId2 = null);
        Task<BookDTO> GetBookByTitleAsync(string title);
        Task DeleteBookByTitleAsync(string title);
        Task DeleteUserBookByBookIdAsync(int bookId);
        Task<List<BookDTO>> GetBooksInLibraryAsync();
    }
}