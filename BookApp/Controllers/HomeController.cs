using BookApp.Core.DTO;
using BookApp.Data;
using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookApp.Core.Models;

namespace BookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookRepository bookRepository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            bookRepository = new BookRepository();
        }
        public IActionResult Index()
        {
            var booksInLibrary = bookRepository.GetBooksInLibrary();
            var libraryViewModel = new LibraryViewModel
            {
                Books = booksInLibrary.Select(book => new BookViewModel
                {
                    Title = book.Title,
                    Author = book.Author,
                }).ToList()
            };

            return View(libraryViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}