using BookApp.Core.DTO;
using BookApp.Core.Interfaces;

namespace BookApp.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<List<BookDTO>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<string> GetBookTitleByIdAsync(int bookId)
        {
            if (bookId <= 0) throw new ArgumentException("Invalid book ID", nameof(bookId));
            return await _bookRepository.GetBookTitleByIdAsync(bookId);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesByBookIdAsync(int userId, int bookId)
        {
            if (userId <= 0) throw new ArgumentException("Invalid user ID", nameof(userId));
            if (bookId <= 0) throw new ArgumentException("Invalid book ID", nameof(bookId));
            return await _bookRepository.GetCategoriesByBookIdAsync(userId, bookId);
        }

        public async Task AddBookToUserCollectionAsync(int userId, int bookId, int categoryId1, int? categoryId2 = null)
        {
            if (bookId <= 0) throw new ArgumentException("Invalid book ID", nameof(bookId));
            if (categoryId1 <= 0) throw new ArgumentException("The required category is empty");

            bool isInCollection = await _bookRepository.IsBookInUserCollectionAsync(bookId, userId);
            if (isInCollection)
            {
                throw new InvalidOperationException("The book is already added to the library.");
            }

            await _bookRepository.AddBookToUserCollectionAsync(userId, bookId, categoryId1, categoryId2);
        }

        public async Task<BookDTO> GetBookByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty", nameof(title));
            return await _bookRepository.GetBookByTitleAsync(title);
        }

        public async Task DeleteBookByTitleAsync(string title, int userId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty", nameof(title));
            }

            await _bookRepository.DeleteBookByTitleAsync(title, userId);
        }

        public async Task DeleteUserBookByBookIdAsync(int bookId, int userId)
        {
            if (bookId <= 0) throw new ArgumentException("Invalid book ID", nameof(bookId));
            await _bookRepository.DeleteUserBookByBookIdAsync(bookId, userId);
        }

        public async Task<List<BookDTO>> GetBooksInLibraryAsync()
        {
            return await _bookRepository.GetBooksInLibraryAsync();
        }

        public async Task<List<BookDTO>> GetBooksByUserIdAsync(int userId)
        {
            return await _bookRepository.GetBooksByUserIdAsync(userId);
        }
    }
}