using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Import the namespace for ILogger
using BookApp.Data;
using BookApp.Core.DTO;

namespace BookApp.Controllers
{
    public class AddBookController : Controller
    {
        private readonly BookRepository bookRepository;
        private readonly ILogger<AddBookController> logger;

        public AddBookController(ILogger<AddBookController> logger)
        {
            bookRepository = new BookRepository();
            this.logger = logger;
        }

        [HttpGet("/addbook")]
        public IActionResult Index()
        {
            var booksDTO = bookRepository.GetAll();

            var addBookViewModel = new AddBookViewModel(booksDTO.ToList());

            logger.LogInformation($"Number of books retrieved: {addBookViewModel.Books.Count}");
            return View(addBookViewModel);  
        }

        [HttpPost("/AddBook/SaveBook")]
        public ActionResult SaveBook(int chosenBookId)
        {
            var bookTitle = bookRepository.GetBookTitleById(chosenBookId);
            logger.LogInformation($"Book added: {bookTitle}");

            bookRepository.AddBookToUserCollection(chosenBookId);

            return RedirectToAction("Index", "Home");
        }
        
    }
}