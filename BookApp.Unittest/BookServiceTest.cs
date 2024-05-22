using Xunit;
using Moq;
using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using BookApp.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.UnitTests
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _bookService = new BookService(_mockBookRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsBookList()
        {
            // Arrange
            var mockBooks = new List<BookDTO>
            {
                new BookDTO { Id = 1, Title = "Book 1", Author = "Author 1", ImageUrl = "url1" },
                new BookDTO { Id = 2, Title = "Book 2", Author = "Author 2", ImageUrl = "url2" }
            };

            _mockBookRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockBooks);

            // Act
            var result = await _bookService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Book 1", result[0].Title);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEmptyListWhenNoBooks()
        {
            // Arrange
            var mockBooks = new List<BookDTO>();

            _mockBookRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockBooks);

            // Act
            var result = await _bookService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBookTitleByIdAsync_ReturnsTitle()
        {
            // Arrange
            var bookId = 1;
            var bookTitle = "Book 1";

            _mockBookRepository.Setup(repo => repo.GetBookTitleByIdAsync(bookId)).ReturnsAsync(bookTitle);

            // Act
            var result = await _bookService.GetBookTitleByIdAsync(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookTitle, result);
        }

        [Fact]
        public async Task GetBookTitleByIdAsync_ReturnsNullWhenBookNotFound()
        {
            // Arrange
            var bookId = 1;

            _mockBookRepository.Setup(repo => repo.GetBookTitleByIdAsync(bookId)).ReturnsAsync((string)null);

            // Act
            var result = await _bookService.GetBookTitleByIdAsync(bookId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBookTitleByIdAsync_ThrowsExceptionWhenBookIdIsInvalid()
        {
            // Arrange
            var invalidBookId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.GetBookTitleByIdAsync(invalidBookId));
        }

        [Fact]
        public async Task GetCategoriesByBookIdAsync_ReturnsCategories()
        {
            // Arrange
            var bookId = 1;
            var mockCategories = new List<CategoryDTO>
            {
                new CategoryDTO(1, "Category 1"),
                new CategoryDTO(2, "Category 2")
            };

            _mockBookRepository.Setup(repo => repo.GetCategoriesByBookIdAsync(bookId)).ReturnsAsync(mockCategories);

            // Act
            var result = await _bookService.GetCategoriesByBookIdAsync(bookId);

            // Assert
            Assert.NotNull(result);
            var resultList = result.ToList();  // Convert to List to support indexing
            Assert.Equal(2, resultList.Count);
            Assert.Equal("Category 1", resultList[0].Name);
        }

        [Fact]
        public async Task GetCategoriesByBookIdAsync_ThrowsExceptionWhenBookIdIsInvalid()
        {
            // Arrange
            var invalidBookId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.GetCategoriesByBookIdAsync(invalidBookId));
        }

        [Fact]
        public async Task AddToUserCollectionAsync_AddsBookToUserCollection()
        {
            // Arrange
            var bookId = 1;
            var categoryId = 1;

            _mockBookRepository.Setup(repo => repo.AddBookToUserCollectionAsync(bookId, categoryId)).Returns(Task.CompletedTask);

            // Act
            await _bookService.AddToUserCollectionAsync(bookId, categoryId);

            // Assert
            _mockBookRepository.Verify(repo => repo.AddBookToUserCollectionAsync(bookId, categoryId), Times.Once);
        }

        [Fact]
        public async Task AddToUserCollectionAsync_ThrowsExceptionWhenBookIdOrCategoryIdIsInvalid()
        {
            // Arrange
            var invalidBookId = -1;
            var invalidCategoryId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.AddToUserCollectionAsync(invalidBookId, invalidCategoryId));
        }

        [Fact]
        public async Task GetBookByTitleAsync_ReturnsBook()
        {
            // Arrange
            var bookTitle = "Book 1";
            var mockBook = new BookDTO(1, bookTitle, "Author 1", "url1", "Series 1", "Genre 1");

            _mockBookRepository.Setup(repo => repo.GetBookByTitleAsync(bookTitle)).ReturnsAsync(mockBook);

            // Act
            var result = await _bookService.GetBookByTitleAsync(bookTitle);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookTitle, result.Title);
        }

        [Fact]
        public async Task GetBookByTitleAsync_ReturnsNullWhenBookNotFound()
        {
            // Arrange
            var bookTitle = "NonExistentBook";

            _mockBookRepository.Setup(repo => repo.GetBookByTitleAsync(bookTitle)).ReturnsAsync((BookDTO)null);

            // Act
            var result = await _bookService.GetBookByTitleAsync(bookTitle);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBookByTitleAsync_ThrowsExceptionWhenTitleIsNullOrEmpty()
        {
            // Arrange
            var invalidTitle = string.Empty;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.GetBookByTitleAsync(invalidTitle));
        }

        [Fact]
        public async Task DeleteAsync_DeletesBook()
        {
            // Arrange
            var bookTitle = "Book 1";
            _bookService.Title = bookTitle;

            _mockBookRepository.Setup(repo => repo.DeleteBookByTitleAsync(bookTitle)).Returns(Task.CompletedTask);

            // Act
            await _bookService.DeleteAsync();

            // Assert
            _mockBookRepository.Verify(repo => repo.DeleteBookByTitleAsync(bookTitle), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ThrowsExceptionWhenTitleIsNullOrEmpty()
        {
            // Arrange
            _bookService.Title = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.DeleteAsync());

            // Arrange
            _bookService.Title = string.Empty;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.DeleteAsync());
        }

        [Fact]
        public async Task DeleteUserBookByBookIdAsync_DeletesUserBook()
        {
            // Arrange
            var bookId = 1;

            _mockBookRepository.Setup(repo => repo.DeleteUserBookByBookIdAsync(bookId)).Returns(Task.CompletedTask);

            // Act
            await _bookService.DeleteUserBookByBookIdAsync(bookId);

            // Assert
            _mockBookRepository.Verify(repo => repo.DeleteUserBookByBookIdAsync(bookId), Times.Once);
        }

        [Fact]
        public async Task DeleteUserBookByBookIdAsync_ThrowsExceptionWhenBookIdIsInvalid()
        {
            // Arrange
            var invalidBookId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.DeleteUserBookByBookIdAsync(invalidBookId));
        }

        [Fact]
        public async Task GetBooksInLibraryAsync_ReturnsBooksInLibrary()
        {
            // Arrange
            var mockBooks = new List<BookDTO>
            {
                new BookDTO { Id = 1, Title = "Book 1", Author = "Author 1", ImageUrl = "url1" },
                new BookDTO { Id = 2, Title = "Book 2", Author = "Author 2", ImageUrl = "url2" }
            };

            _mockBookRepository.Setup(repo => repo.GetBooksInLibraryAsync()).ReturnsAsync(mockBooks);

            // Act
            var result = await _bookService.GetBooksInLibraryAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Book 1", result[0].Title);
        }

        [Fact]
        public async Task GetBooksInLibraryAsync_ReturnsEmptyListWhenNoBooks()
        {
            // Arrange
            var mockBooks = new List<BookDTO>();

            _mockBookRepository.Setup(repo => repo.GetBooksInLibraryAsync()).ReturnsAsync(mockBooks);

            // Act
            var result = await _bookService.GetBooksInLibraryAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
