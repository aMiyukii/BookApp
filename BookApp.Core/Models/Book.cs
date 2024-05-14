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
        
        private readonly IBookService _bookService;
        public Book(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<List<BookDTO>> GetAllAsync()
        {
            return await _bookService.GetAllAsync();
        }
        public async Task<string> GetBookTitleByIdAsync(int bookId)
        {
            return await _bookService.GetBookTitleByIdAsync(bookId);
        }
        public async Task AddToUserCollectionAsync(int bookId)
        {
            await _bookService.AddBookToUserCollectionAsync(bookId);
        }
        public async Task<BookDTO> GetBookByTitleAsync(string title)
        {
            return await _bookService.GetBookByTitleAsync(title);
        }
        public async Task DeleteAsync()
        {
            await _bookService.DeleteBookByTitleAsync(this.Title);
        }
        public async Task DeleteUserBookByBookIdAsync(int bookId)
        {
            await _bookService.DeleteUserBookByBookIdAsync(bookId);
        }
    }
}