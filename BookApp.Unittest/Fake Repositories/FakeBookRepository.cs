using BookApp.Core.DTO;
using BookApp.Core.Interfaces;

public class FakeBookRepository : IBookRepository
{
    private List<BookDTO> _books;
    private List<CategoryDTO> _categories;

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

    public Task<IEnumerable<CategoryDTO>> GetCategoriesByBookIdAsync(int bookId)
    {
        // Assuming all books have the same categories in the fake repository
        return Task.FromResult(_categories.AsEnumerable());
    }

    public Task AddBookToUserCollectionAsync(int bookId, int categoryId1, int categoryId2)
    {
        // No operation needed for fake repository
        return Task.CompletedTask;
    }

    public Task<BookDTO> GetBookByTitleAsync(string title)
    {
        var book = _books.FirstOrDefault(b => b.Title == title);
        return Task.FromResult(book);
    }

    public Task<BookDTO> GetBookByIdAsync(int bookId)
    {
        var book = _books.FirstOrDefault(b => b.Id == bookId);
        return Task.FromResult(book);
    }

    public Task DeleteBookByTitleAsync(string title)
    {
        var book = _books.FirstOrDefault(b => b.Title == title);
        if (book != null)
        {
            _books.Remove(book);
        }
        return Task.CompletedTask;
    }

    public Task DeleteUserBookByBookIdAsync(int bookId)
    {
        // No operation needed for fake repository
        return Task.CompletedTask;
    }

    public Task<List<BookDTO>> GetBooksInLibraryAsync()
    {
        // Assuming all books are in the library in the fake repository
        return Task.FromResult(_books);
    }
}
