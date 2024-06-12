using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Core.Services
{
    public class BookService
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

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesByBookIdAsync(int bookId)
    {
        if (bookId <= 0) throw new ArgumentException("Invalid book ID", nameof(bookId));
        return await _bookRepository.GetCategoriesByBookIdAsync(bookId);
    }

    public async Task AddBookToUserCollectionAsync(int bookId, int categoryId1, int? categoryId2 = null)
    {
        if (bookId <= 0) throw new ArgumentException("Invalid book ID", nameof(bookId));
        if (categoryId1 <= 0) throw new ArgumentException("The required category is empty");

        bool isInCollection = await _bookRepository.IsBookInUserCollectionAsync(bookId);
        if (isInCollection)
        {
            throw new InvalidOperationException("The book is already added to the library.");
        }

        await _bookRepository.AddBookToUserCollectionAsync(bookId, categoryId1, categoryId2);
    }

    public async Task<BookDTO> GetBookByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be null or empty", nameof(title));
        return await _bookRepository.GetBookByTitleAsync(title);
    }

    public async Task DeleteBookByTitleAsync(string title)
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
