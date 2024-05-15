using BookApp.Core.DTO;

namespace BookApp.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<List<BookDTO>> GetAllAsync();
        Task<string> GetBookTitleByIdAsync(int bookId);
        Task AddBookToUserCollectionAsync(int bookId);
        Task<BookDTO> GetBookByTitleAsync(string title);
        Task DeleteBookByTitleAsync(string title);
        Task DeleteUserBookByBookIdAsync(int bookId);
        Task<List<BookDTO>> GetBooksInLibraryAsync();
    }
}