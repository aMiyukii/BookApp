using BookApp.Core.DTO;
using BookApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Models;

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
