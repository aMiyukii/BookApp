using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using BookApp.Core.Interfaces;

namespace BookApp.Controllers
{
    public class AddBookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<AddBookController> _logger;

        public AddBookController(IBookService bookService, ICategoryService categoryService,
            ILogger<AddBookController> logger)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet("/addbook")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var booksDTO = await _bookService.GetAllAsync();
                var categoriesDTO = await _categoryService.GetAllCategoriesAsync();

                var viewModel = new AddBookViewModel(booksDTO.ToList(), categoriesDTO.ToList());

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving data for the add book page.");
                TempData["ErrorMessage"] = "An error occurred while retrieving data for the add book page.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost("/AddBook/SaveBook")]
        public async Task<ActionResult> SaveBook(int chosenBookId, int chosenCategoryId1, int chosenCategoryId2)
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("UserId");

                if (userId == null)
                {
                    TempData["ErrorMessage"] = "User not logged in.";
                    return RedirectToAction("Index");
                }

                int? categoryId2 = (chosenCategoryId2 == 0) ? (int?)null : chosenCategoryId2;

                await _bookService.AddBookToUserCollectionAsync((int)userId, chosenBookId, chosenCategoryId1, categoryId2);

                TempData["SuccessMessage"] = "Book added successfully.";
                return RedirectToAction("Index", "Library");
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the book.");
                TempData["ErrorMessage"] = "An error occurred while saving the book.";
                return RedirectToAction("Index");
            }
        }
    }
}
