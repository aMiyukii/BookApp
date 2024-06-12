using BookApp.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class BookTest
    {
        private BookService _bookService;

        [TestInitialize]
        public void Setup()
        {
            var fakeRepository = new FakeBookRepository();
            _bookService = new BookService(fakeRepository);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllBooks()
        {
            // Act
            var books = await _bookService.GetAllAsync();

            // Assert
            Assert.AreEqual(2, books.Count);
        }

        [TestMethod]
        public async Task GetBookTitleByIdAsync_ValidId_ShouldReturnTitle()
        {
            // Arrange
            var bookId = 1;

            // Act
            var title = await _bookService.GetBookTitleByIdAsync(bookId);

            // Assert
            Assert.AreEqual("Test Book 1", title);
        }

        [TestMethod]
        public async Task GetBookTitleByIdAsync_InvalidId_ShouldReturnEmptyString()
        {
            // Arrange
            var bookId = 99;

            // Act
            var title = await _bookService.GetBookTitleByIdAsync(bookId);

            // Assert
            Assert.AreEqual(string.Empty, title);
        }

        [TestMethod]
        public async Task GetCategoriesByBookIdAsync_ValidId_ShouldReturnCategories()
        {
            // Arrange
            var bookId = 1;

            // Act
            var categories = await _bookService.GetCategoriesByBookIdAsync(bookId);

            // Assert
            Assert.AreEqual(2, categories.Count());
        }

        [TestMethod]
        public async Task AddBookToUserCollectionAsync_ValidIds_ShouldNotThrowException()
        {
            // Arrange
            var bookId = 1;
            var categoryId1 = 1;
            var categoryId2 = 2;

            // Act & Assert
            await _bookService.AddBookToUserCollectionAsync(bookId, categoryId1, categoryId2);
        }

        [TestMethod]
        public async Task GetBookByTitleAsync_ValidTitle_ShouldReturnBook()
        {
            // Arrange
            var title = "Test Book 1";

            // Act
            var book = await _bookService.GetBookByTitleAsync(title);

            // Assert
            Assert.IsNotNull(book);
            Assert.AreEqual(title, book.Title);
        }

        [TestMethod]
        public async Task DeleteAsync_ValidTitle_ShouldRemoveBook()
        {
            // Arrange
            var title = "Test Book 1";

            // Act
            await _bookService.DeleteAsync(title);
            var books = await _bookService.GetAllAsync();

            // Assert
            Assert.AreEqual(1, books.Count);
        }

        [TestMethod]
        public async Task DeleteUserBookByBookIdAsync_ValidId_ShouldNotThrowException()
        {
            // Arrange
            var bookId = 1;

            // Act & Assert
            await _bookService.DeleteUserBookByBookIdAsync(bookId);
        }

        [TestMethod]
        public async Task GetBooksInLibraryAsync_ShouldReturnBooksInLibrary()
        {
            // Act
            var books = await _bookService.GetBooksInLibraryAsync();

            // Assert
            Assert.AreEqual(2, books.Count);
        }
    }
}
