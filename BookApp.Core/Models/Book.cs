using BookApp.Core.DTO;
using BookApp.Core.Services;

namespace BookApp.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Serie { get; set; }
        public string ImageUrl { get; set; }

        public Book()
        {

        }
        
        private readonly IBookRepository _bookRepository;
        public Book(IBookRepository bookRepository)
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
        public async Task AddToUserCollectionAsync(int bookId)
        {
            await _bookRepository.AddBookToUserCollectionAsync(bookId);
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