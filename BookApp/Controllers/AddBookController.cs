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
        private readonly BookRepository bookRepository;

        public AddBookController()
        {
            bookRepository = new BookRepository();
        }

        [HttpGet("/addbook")]
        public IActionResult Index()
        {
            var bookDTOs = bookRepository.GetAll();

            var books = bookDTOs.Select(dto => new Book
            {
                Id = dto.Id,
                Title = dto.Title,
                Author = dto.Author,
                //Genre = dto.Genre,
                //Serie = dto.Serie,
                Image = dto.Image
            }).ToList();

            Console.WriteLine($"Number of books retrieved: {books.Count}");
            return View(books);
        }
    

        [HttpPost("/Home/Index")]
        public ActionResult SaveBook(string chosenBookId)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
