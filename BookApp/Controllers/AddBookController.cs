using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookApp.Data;
using BookApp.Core.DTO;

namespace BookApp.Controllers
{
    public class AddBookController : Controller
    {
        private readonly BookRepository bookRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly ILogger<AddBookController> logger;

        public AddBookController(ILogger<AddBookController> logger)
        {
            bookRepository = new BookRepository();
            categoryRepository = new CategoryRepository();
            this.logger = logger;
        }

        [HttpGet("/addbook")]
        public IActionResult Index()
        {
            var booksDTO = bookRepository.GetAll();
            
            var categoriesDTO = categoryRepository.GetAllCategory();

            var addBookViewModel = new AddBookViewModel(booksDTO.ToList(), categoriesDTO.ToList());

            logger.LogInformation($"Number of books retrieved: {addBookViewModel.Books.Count}");
            logger.LogInformation($"Number of categories retrieved: {addBookViewModel.Categories.Count}");
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