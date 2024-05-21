using BookApp.Models;
using BookApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Core.Interfaces;

namespace BookApp.Controllers
{
    public class AddBookController : Controller
    {
        private readonly BookService _bookService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<AddBookController> _logger;

        public AddBookController(BookService bookService, ICategoryRepository categoryRepository, ILogger<AddBookController> logger)
        {
            _bookService = bookService;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet("/addbook")]
        public async Task<IActionResult> Index()
        {
            var booksDTO = await _bookService.GetAllAsync();
            var categoriesDTO = await _categoryRepository.GetAllCategoriesAsync();

            var addBookViewModel = new AddBookViewModel(booksDTO.ToList(), categoriesDTO.ToList());

            return View(addBookViewModel);
        }
        
        [HttpPost("/AddBook/SaveBook")]
        public async Task<ActionResult> SaveBook(int chosenBookId, int chosenCategoryId)
        {
            await _bookService.AddToUserCollectionAsync(chosenBookId, chosenCategoryId);
            
            return RedirectToAction("Index", "Home");
        }

    }
}