using BookApp.Core.DTO;
using BookApp.Core.Interfaces;

public class FakeBookRepository : IBookRepository
{
    private List<BookDTO> _books;
    private List<CategoryDTO> _categories;
    private List<(int userId, int bookId)> _userBooks;

    public FakeBookRepository()
    {
        _books = new List<BookDTO>
        {
            new BookDTO(1, "Test Book 1", "Author 1", "ImageUrl1"),
            new BookDTO(2, "Test Book 2", "Author 2", "ImageUrl2")
        };

        _categories = new List<CategoryDTO>
        {
            new CategoryDTO(1, "Category 1"),
            new CategoryDTO(2, "Category 2")
        };

        _userBooks = new List<(int userId, int bookId)>
        {
            (1, 1),
            (2, 2)
        };
    }

    public Task<List<BookDTO>> GetAllAsync()
    {
        return Task.FromResult(_books);
    }

    public Task<string> GetBookTitleByIdAsync(int bookId)
    {
        var book = _books.FirstOrDefault(b => b.Id == bookId);
        return Task.FromResult(book?.Title ?? string.Empty);
    }

    public Task<IEnumerable<CategoryDTO>> GetCategoriesByBookIdAsync(int userId, int bookId)
    {
        var categoriesForBook = _categories.Where(c => c.Id == 1 || c.Id == 2).ToList();
        return Task.FromResult(categoriesForBook.AsEnumerable());
    }


    public Task AddBookToUserCollectionAsync(int userId, int bookId, int categoryId1, int? categoryId2 = null)
    {
        _userBooks.Add((userId, bookId));
        return Task.CompletedTask;
    }

    public Task<BookDTO> GetBookByTitleAsync(string title)
    {
        var book = _books.FirstOrDefault(b => b.Title == title);
        return Task.FromResult(book);
    }

    public Task DeleteBookByTitleAsync(string title, int userId)
    {
        var book = _books.FirstOrDefault(b => b.Title == title);
        if (book != null)
        {
            _books.Remove(book);
        }
        return Task.CompletedTask;
    }

    public Task DeleteUserBookByBookIdAsync(int bookId, int userId)
    {
        var userBook = _userBooks.FirstOrDefault(ub => ub.userId == userId && ub.bookId == bookId);
        if (userBook != default)
        {
            _userBooks.Remove(userBook);
        }
        return Task.CompletedTask;
    }

    public Task<List<BookDTO>> GetBooksInLibraryAsync()
    {
        var booksInLibrary = _userBooks.Select(ub => _books.FirstOrDefault(b => b.Id == ub.bookId)).ToList();
        return Task.FromResult(booksInLibrary);
    }

    public Task SaveCategoryAsync(int userBookId, int categoryId1, int categoryId2)
    {
        // Not relevant for the FakeRepository implementation for now.
        return Task.CompletedTask;
    }

    public Task<bool> IsBookInUserCollectionAsync(int userId, int bookId)
    {
        var isInCollection = _userBooks.Any(ub => ub.userId == userId && ub.bookId == bookId);
        return Task.FromResult(isInCollection);
    }

    public Task<List<BookDTO>> GetBooksByUserIdAsync(int userId)
    {
        var booksByUser = _userBooks
            .Where(ub => ub.userId == userId)
            .Select(ub => _books.FirstOrDefault(b => b.Id == ub.bookId))
            .ToList();
        return Task.FromResult(booksByUser);
    }
}
