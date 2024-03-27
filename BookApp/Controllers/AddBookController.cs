using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using BookApp.Core.Models;
using BookApp.Data;
using BookApp.Core.DTO;

namespace BookApp.Controllers
{
    public class AddBookController : Controller
    {
        private readonly BookDTO _bookDTO;


        public AddBookController()
        {
            _bookDTO = new BookDTO();
        }

        public IActionResult Index()
        {
            var books = _bookDTO.GetAllBooks();
            Console.WriteLine($"Number of books retrieved: {books.Count}");
            return View(books);
        }

    }
}
