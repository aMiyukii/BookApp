using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

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

        public IActionResult Index()
        {
            var booksInLibrary = _bookRepository.GetBooksInLibraryAsync().Result;
            var libraryViewModel = new LibraryViewModel
            {
                Books = booksInLibrary.Select(book => new BookViewModel
                {
                    Title = book.Title,
                    Author = book.Author,
                    ImageUrl = book.ImageUrl
                }).ToList()
            };

            return View(libraryViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult BookDetails(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Book title cannot be null or empty.");
            }

            var book = _bookRepository.GetBookByTitleAsync(title).Result;

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            var bookViewModel = new BookViewModel
            {
                Title = book.Title,
                Author = book.Author,
                Serie = book.Serie,
                Genre = book.Genre,
                ImageUrl = book.ImageUrl
            };

            return View(bookViewModel);
        }

        [HttpPost]
        public IActionResult DeleteBook(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Book title cannot be null or empty.");
            }

            var book = _bookRepository.GetBookByTitleAsync(title).Result;

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            _bookRepository.DeleteBookByTitleAsync(title).Wait();
            _bookRepository.DeleteUserBookByBookIdAsync(book.Id).Wait();

            return RedirectToAction("Index");
        }
    }
}
