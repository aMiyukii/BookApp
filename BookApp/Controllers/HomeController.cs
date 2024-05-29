using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using BookApp.Core.Services;

namespace BookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookService _bookService;

        public HomeController(ILogger<HomeController> logger, BookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var booksInLibrary = await _bookService.GetBooksInLibraryAsync();
            var libraryViewModel = new LibraryViewModel
            {
                Books = booksInLibrary.Select(book =>
                {
                    var categories = _bookService.GetCategoriesByBookIdAsync(book.Id).Result;
                    return new BookViewModel
                    {
                        Title = book.Title,
                        Author = book.Author,
                        ImageUrl = book.ImageUrl,
                        Categories = categories.Select(c => new CategoryViewModel
                        {
                            Id = c.Id,
                            Name = c.Name,
                            IsStandard = c.IsStandard
                        }).ToList()
                    };
                }).ToList()
            };

            return View(libraryViewModel);
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

            var categories = await _bookService.GetCategoriesByBookIdAsync(book.Id);

            var bookViewModel = new BookViewModel
            {
                Title = book.Title,
                Author = book.Author,
                Serie = book.Serie,
                Genre = book.Genre,
                ImageUrl = book.ImageUrl,
                Categories = categories.Select(c => new CategoryViewModel
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
    }
}