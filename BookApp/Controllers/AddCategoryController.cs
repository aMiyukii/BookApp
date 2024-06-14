using Microsoft.AspNetCore.Mvc;
using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using BookApp.Models;
using System;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    public class AddCategoryController : Controller
    {
        private readonly ICategoryService _categoryServices;

        public AddCategoryController(ICategoryService categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet("/addcategory")]
        public async Task<IActionResult> Index()
        {
            var categoriesDto = await _categoryServices.GetAllCategoriesAsync();
            var viewModel = new AddCategoryViewModel
            {
                Categories = categoriesDto
            };

            return View(viewModel);
        }

        [HttpPost("/AddCategory/AddCategoryName")]
        public async Task<ActionResult> AddCategoryName(CategoryDTO category)
        {
            try
            {
                await _categoryServices.AddCategoryAsync(category);
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost("/AddCategory/UpdateCategoryName")]
        public async Task<ActionResult> UpdateCategoryName(int id, string newName)
        {
            try
            {
                await _categoryServices.UpdateCategoryNameAsync(id, newName);
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost("/AddCategory/DeleteCategory")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryServices.DeleteCategoryAsync(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
