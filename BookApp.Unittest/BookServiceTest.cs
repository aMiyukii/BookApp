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
    }

}
