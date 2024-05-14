using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookApp.Data;
using BookApp.Core.DTO;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    public class AddBookController : Controller
    {
        private readonly BookRepository bookRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly ILogger<AddBookController> logger;

        public AddBookController(ILogger<AddBookController> logger, BookRepository bookRepository, CategoryRepository categoryRepository)
        {
            this.bookRepository = bookRepository;
            this.categoryRepository = categoryRepository;
            this.logger = logger;
        }

        [HttpGet("/addbook")]   
        public async Task<IActionResult> Index()
        {
            var booksDTO = await bookRepository.GetAllAsync();
            var categoriesDTO = await categoryRepository.GetAllCategoryAsync();

            var addBookViewModel = new AddBookViewModel(booksDTO, categoriesDTO);

            logger.LogInformation($"Number of books retrieved: {addBookViewModel.Books.Count}");
            logger.LogInformation($"Number of categories retrieved: {addBookViewModel.Categories.Count}");

            return View(addBookViewModel);  
        }

        [HttpPost("/AddBook/SaveBook")]
        public async Task<ActionResult> SaveBook(int chosenBookId)
        {
            var bookTitle = await bookRepository.GetBookTitleByIdAsync(chosenBookId);
            logger.LogInformation($"Book added: {bookTitle}");

            await bookRepository.AddBookToUserCollectionAsync(chosenBookId);

            return RedirectToAction("Index", "Home");
        }
    }
}