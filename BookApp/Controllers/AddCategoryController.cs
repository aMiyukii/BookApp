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

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true });
                }

                return RedirectToAction("Index");
            }
            catch (ArgumentNullException ex)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = ex.Message });
                }

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = ex.Message });
                }

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
    
            return RedirectToAction("Index");
        }

        [HttpPost("/AddCategory/UpdateCategoryName")]
        public async Task<ActionResult> UpdateCategoryName(int id, string newName)
        {
            try
            {
                await _categoryServices.UpdateCategoryNameAsync(id, newName);
                return Json(new { success = true });
            }
            catch (ArgumentNullException ex)
            {
                // Handle specific exceptions
                return Json(new { success = false, message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Handle specific exceptions
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return Json(new { success = false, message = ex.Message });
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