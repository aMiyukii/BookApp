using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookApp.Core.DTO;
using BookApp.Core.Interfaces;

namespace BookApp.Core.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public string Title { get; set; }

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
        
        public async Task<IEnumerable<CategoryDTO>> GetCategoriesByBookIdAsync(int bookId)
        {
            if (bookId <= 0) throw new ArgumentException("Invalid book ID", nameof(bookId));
            return await _bookRepository.GetCategoriesByBookIdAsync(bookId);
        }

        public async Task AddToUserCollectionAsync(int bookId, int categoryId)
        {
            if (bookId <= 0) throw new ArgumentException("Invalid book ID", nameof(bookId));
            if (categoryId <= 0) throw new ArgumentException("Invalid category ID", nameof(categoryId));

            await _bookRepository.AddBookToUserCollectionAsync(bookId, categoryId);
        }

        public async Task<BookDTO> GetBookByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be null or empty", nameof(title));
            return await _bookRepository.GetBookByTitleAsync(title);
        }

        public async Task DeleteAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty", nameof(title));
            }

            await _bookRepository.DeleteBookByTitleAsync(title);
        }


        public async Task DeleteUserBookByBookIdAsync(int bookId)
        {
            if (bookId <= 0) throw new ArgumentException("Invalid book ID", nameof(bookId));
            await _bookRepository.DeleteUserBookByBookIdAsync(bookId);
        }

        public async Task<List<BookDTO>> GetBooksInLibraryAsync()
        {
            return await _bookRepository.GetBooksInLibraryAsync();
        }
    }
}
