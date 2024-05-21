using BookApp.Core.DTO;
using BookApp.Core.Interfaces;

namespace BookApp.Core.Services
{
    public class BookService
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Serie { get; set; }
        public string ImageUrl { get; set; }

        public BookService()
        {

        }
        
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<BookDTO>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<string> GetBookTitleByIdAsync(int bookId)
        {
            return await _bookRepository.GetBookTitleByIdAsync(bookId);
        }

        public async Task AddToUserCollectionAsync(int bookId, int categoryId)
        {
            await _bookRepository.AddBookToUserCollectionAsync(bookId, categoryId);
        }

        public async Task<BookDTO> GetBookByTitleAsync(string title)
        {
            return await _bookRepository.GetBookByTitleAsync(title);
        }

        public async Task DeleteAsync()
        {
            await _bookRepository.DeleteBookByTitleAsync(this.Title);
        }

        public async Task DeleteUserBookByBookIdAsync(int bookId)
        {
            await _bookRepository.DeleteUserBookByBookIdAsync(bookId);
        }

        public async Task<List<BookDTO>> GetBooksInLibraryAsync()
        {
            return await _bookRepository.GetBooksInLibraryAsync();
        }
    }
}