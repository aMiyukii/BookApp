using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Core.Services;

namespace BookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookService _bookService;
        private readonly CategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, BookService bookService, CategoryService categoryService)
        {
            _logger = logger;
            _bookService = bookService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var booksInLibrary = await _bookService.GetBooksInLibraryAsync();

                if (booksInLibrary == null || !booksInLibrary.Any())
                {
                    _logger.LogInformation("No books found in the library.");
                    return View(new LibraryViewModel { Books = new List<BookViewModel>() });
                }

                _logger.LogInformation($"Found {booksInLibrary.Count()} books in the library.");

                var booksViewModel = booksInLibrary.Select(book => new BookViewModel
                {
                    Title = book.Title,
                    Author = book.Author,
                    ImageUrl = book.ImageUrl
                }).ToList();

                return View(new LibraryViewModel { Books = booksViewModel });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the index action.");
                return View(new LibraryViewModel { Books = new List<BookViewModel>() });
            }
        }

        public async Task<IActionResult> BookDetails(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Book title cannot be null or empty.");
            }

            var book = await _bookService.GetBookByTitleAsync(title);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            var bookCategories = await _bookService.GetCategoriesByBookIdAsync(book.Id);
            var allCategories = await _categoryService.GetAllCategoriesAsync();

            var bookViewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Serie = book.Serie,
                Genre = book.Genre,
                ImageUrl = book.ImageUrl,
                Categories = bookCategories.Select(c => new BookViewModel.CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsStandard = c.IsStandard
                }).ToList(),
                AllCategories = allCategories.Select(c => new BookViewModel.CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsStandard = c.IsStandard
                }).ToList()
            };

            return View(bookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Book title cannot be null or empty.");
            }

            var book = await _bookService.GetBookByTitleAsync(title);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            await _bookService.DeleteBookByTitleAsync(title);
            await _bookService.DeleteUserBookByBookIdAsync(book.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(int bookId, int categoryId)
        {
            await _categoryService.SaveCategoryAsync(bookId, categoryId, categoryId);

            return Ok();
        }

    }
}
