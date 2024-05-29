using Microsoft.AspNetCore.Mvc;
using BookApp.Core.DTO;
using BookApp.Core.Services;
using BookApp.Models;

namespace BookApp.Controllers
{
    public class AddCategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public AddCategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("/addcategory")]
        public async Task<IActionResult> Index()
        {
            var categoriesDto = await _categoryService.GetAllCategoriesAsync();
            var viewModel = new AddCategoryViewModel
            {
                Categories = categoriesDto
            };

            return View(viewModel);
        }

        [HttpPost("/AddCategory/AddCategoryName")]
        public async Task<ActionResult> AddCategoryName(CategoryDTO category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                Console.WriteLine("Error: Empty category name");
                return RedirectToAction("Index");
            }

            await _categoryService.AddCategoryAsync(category);
            return RedirectToAction("Index");
        }

        [HttpPost("/AddCategory/UpdateCategoryName")]
        public async Task<ActionResult> UpdateCategoryName(int id, string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                Console.WriteLine("Error: Empty category name");
                return RedirectToAction("Index");
            }

            var category = await _categoryService.GetCategoryByIdAsync(id);
            category.Name = newName;

            await _categoryService.UpdateCategoryAsync(category);
            return RedirectToAction("Index");
        }

        [HttpPost("/AddCategory/DeleteCategory")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}