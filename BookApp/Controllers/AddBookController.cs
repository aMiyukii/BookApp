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
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<AddBookController> _logger;

        public AddBookController(ILogger<AddBookController> logger, IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet("/addbook")]   
        public async Task<IActionResult> Index()
        {
            var booksDTO = await _bookService.GetAllAsync();
            var categoriesDTO = await _categoryService.GetAllCategoryAsync();

            var addBookViewModel = new AddBookViewModel(booksDTO, categoriesDTO);

            _logger.LogInformation($"Number of books retrieved: {addBookViewModel.Books.Count}");
            _logger.LogInformation($"Number of categories retrieved: {addBookViewModel.Categories.Count}");

            return View(addBookViewModel);  
        }

        [HttpPost("/AddBook/SaveBook")]
        public async Task<ActionResult> SaveBook(int chosenBookId)
        {
            var bookTitle = await _bookService.GetBookTitleByIdAsync(chosenBookId);
            _logger.LogInformation($"Book added: {bookTitle}");

            await _bookService.AddBookToUserCollectionAsync(chosenBookId);

            return RedirectToAction("Index", "Home");
        }
    }
}