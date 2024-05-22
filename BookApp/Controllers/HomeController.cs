using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository;

        public HomeController(ILogger<HomeController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index()
        {
            var booksInLibrary = await _bookRepository.GetBooksInLibraryAsync();
            var libraryViewModel = new LibraryViewModel
            {
                Books = booksInLibrary.Select(book =>
                {
                    var categories = _bookRepository.GetCategoriesByBookIdAsync(book.Id).Result;
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

            var book = await _bookRepository.GetBookByTitleAsync(title);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            var categories = await _bookRepository.GetCategoriesByBookIdAsync(book.Id);

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

            var book = await _bookRepository.GetBookByTitleAsync(title);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            await _bookRepository.DeleteBookByTitleAsync(title);
            await _bookRepository.DeleteUserBookByBookIdAsync(book.Id);

            return RedirectToAction("Index");
        }
    }
}
