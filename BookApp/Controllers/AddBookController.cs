using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using BookApp.Core.Models;
using BookApp.Data;
using BookApp.Core.DTO;
using BookApp.Models;

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
            var booksDTO = bookRepository.GetAll();

            var addBookViewModel = new AddBookViewModel(booksDTO.ToList());

            Console.WriteLine($"Number of books retrieved: {addBookViewModel.Books.Count}");
            return View(addBookViewModel);  
        }





        [HttpPost("/Home/Index")]
        public ActionResult SaveBook(AddBookViewModel chosenBook)
        {
            return RedirectToAction("Index", "Home");
        }

    }
}
