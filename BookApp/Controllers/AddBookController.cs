using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookApp.Data;
using BookApp.Core.DTO;
using System.Threading.Tasks;
using BookApp.Core.Services;

namespace BookApp.Controllers
{
    public class AddBookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<AddBookController> _logger;

        public AddBookController(ILogger<AddBookController> logger, IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet("/addbook")]   
        public async Task<IActionResult> Index()
        {
            var booksDTO = await _bookRepository.GetAllAsync();
            var categoriesDTO = await _categoryRepository.GetAllCategoriesAsync(); // Corrected method name

            var addBookViewModel = new AddBookViewModel(booksDTO, categoriesDTO);

            _logger.LogInformation($"Number of books retrieved: {addBookViewModel.Books.Count}");
            _logger.LogInformation($"Number of categories retrieved: {addBookViewModel.Categories.Count}");

            return View(addBookViewModel);  
        }
        
        [HttpPost("/AddBook/SaveBook")]
        public async Task<ActionResult> SaveBook(int chosenBookId)
        {
            var bookTitle = await _bookRepository.GetBookTitleByIdAsync(chosenBookId);
            _logger.LogInformation($"Book added: {bookTitle}");

            await _bookRepository.AddBookToUserCollectionAsync(chosenBookId);

            return RedirectToAction("Index", "Home");
        }
    }
}