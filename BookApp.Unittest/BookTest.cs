using BookApp.Core.DTO;
using BookApp.Core.Services;
using BookApp.Unittest.Fake_Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            // Act
            var title = await _bookService.GetBookTitleByIdAsync(1);

            // Assert
            Assert.AreEqual("Test Book 1", title);
        }

        [TestMethod]
        public async Task GetBookTitleByIdAsync_InvalidId_ShouldReturnEmptyString()
        {
            // Act
            var title = await _bookService.GetBookTitleByIdAsync(99);

            // Assert
            Assert.AreEqual(string.Empty, title);
        }

        [TestMethod]
        public async Task GetBookTitleByIdAsync_NegativeId_ShouldThrowArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _bookService.GetBookTitleByIdAsync(-1));
        }

        [TestMethod]
        public async Task GetCategoriesByBookIdAsync_ValidIds_ShouldReturnCategories()
        {
            // Act
            var categories = await _bookService.GetCategoriesByBookIdAsync(1, 1);

            // Assert
            Assert.AreEqual(2, categories.Count());
        }

        [TestMethod]
        public async Task GetCategoriesByBookIdAsync_InvalidBookId_ShouldThrowArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _bookService.GetCategoriesByBookIdAsync(1, -1));
        }

        [TestMethod]
        public async Task AddBookToUserCollectionAsync_ValidIds_ShouldNotThrowException()
        {
            // Act & Assert
            await _bookService.AddBookToUserCollectionAsync(1, 2, 1, 2);
        }

        [TestMethod]
        public async Task AddBookToUserCollectionAsync_InvalidBookId_ShouldThrowArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _bookService.AddBookToUserCollectionAsync(1, -1, 1, 2));
        }

        [TestMethod]
        public async Task AddBookToUserCollectionAsync_BookAlreadyInCollection_ShouldThrowInvalidOperationException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                _bookService.AddBookToUserCollectionAsync(1, 1, 1, 2));
        }

        [TestMethod]
        public async Task GetBookByTitleAsync_ValidTitle_ShouldReturnBook()
        {
            // Act
            var book = await _bookService.GetBookByTitleAsync("Test Book 1");

            // Assert
            Assert.IsNotNull(book);
            Assert.AreEqual("Test Book 1", book.Title);
        }

        [TestMethod]
        public async Task GetBookByTitleAsync_NullTitle_ShouldThrowArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _bookService.GetBookByTitleAsync(null));
        }

        [TestMethod]
        public async Task DeleteBookByTitleAsync_ValidTitle_ShouldRemoveBook()
        {
            // Arrange
            string titleToDelete = "Test Book 1";
            int userId = 1;

            // Act
            await _bookService.DeleteBookByTitleAsync(titleToDelete, userId);

            // Assert
            var books = await _bookService.GetAllAsync();
            Assert.AreEqual(1, books.Count);
            Assert.IsFalse(books.Any(b => b.Title == titleToDelete));
        }

        [TestMethod]
        public async Task DeleteBookByTitleAsync_NullTitle_ShouldThrowArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _bookService.DeleteBookByTitleAsync(null, 1));
        }

        [TestMethod]
        public async Task DeleteUserBookByBookIdAsync_ValidId_ShouldNotThrowException()
        {
            // Act & Assert
            await _bookService.DeleteUserBookByBookIdAsync(1, 1);
        }

        [TestMethod]
        public async Task DeleteUserBookByBookIdAsync_InvalidBookId_ShouldThrowArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _bookService.DeleteUserBookByBookIdAsync(-1, 1));
        }


        [TestMethod]
        public async Task GetBooksInLibraryAsync_ShouldReturnBooksInLibrary()
        {
            // Act
            var books = await _bookService.GetBooksInLibraryAsync();

            // Assert
            Assert.AreEqual(2, books.Count);
        }

        [TestMethod]
        public async Task GetBooksByUserIdAsync_InvalidUserId_ShouldReturnEmptyList()
        {
            // Act
            var books = await _bookService.GetBooksByUserIdAsync(-1);

            // Assert
            Assert.AreEqual(0, books.Count);
        }
        
        [TestClass]
        public class BookDTOTest
        {
            [TestMethod]
            public void BookDTO_Constructor_ValidInitialization()
            {
                // Arrange & Act
                var bookDto = new BookDTO(1, "Test Book", "Author", "ImageUrl");

                // Assert
                Assert.AreEqual(1, bookDto.Id);
                Assert.AreEqual("Test Book", bookDto.Title);
                Assert.AreEqual("Author", bookDto.Author);
                Assert.AreEqual("ImageUrl", bookDto.ImageUrl);
            }
        }
    }
}
