using Microsoft.AspNetCore.Mvc;
using BookApp.Core.Interfaces;
using BookApp.Models;
using System.Threading.Tasks;
using BookApp.Core.DTO;
using BookApp.Core.Services;

    namespace BookApp.Controllers
    {
        public class AddCategoryController : Controller
        {
            private readonly CategoryServices _categoryServices;

            public AddCategoryController(CategoryServices categoryServices)
            {
                _categoryServices = categoryServices;
            }

            [HttpGet("/addcategory")]
            public async Task<IActionResult> Index()
            {
                var categoriesDTO = await _categoryServices.GetAllCategoriesAsync();

                var viewModel = new AddCategoryViewModel
                {
                    Categories = categoriesDTO
                };

                return View(viewModel);
            }

            [HttpPost("/AddCategory/AddCategoryName")]
            public async Task<ActionResult> AddCategoryName(CategoryDTO category)
            {
                await _categoryServices.AddCategoryAsync(category);
                return RedirectToAction("Index");
            }

            [HttpPost("/AddCategory/UpdateCategoryName")]
            public async Task<ActionResult> UpdateCategoryName(CategoryDTO category)
            {
                await _categoryServices.UpdateCategoryAsync(category);
                return RedirectToAction("Index");
            }

            [HttpPost("/AddCategory/DeleteCategory")]
            public async Task<ActionResult> DeleteCategory(int id)
            {
                await _categoryServices.DeleteCategoryAsync(id);
                return RedirectToAction("Index");
            }
        }
}