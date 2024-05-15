using BookApp.Core.DTO;
using BookApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using BookApp.Models;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    public class AddCategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public AddCategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var viewModel = new AddCategoryViewModel(categories);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategoryName(AddCategoryViewModel addCategoryViewModel)
        {
            var categoryName = addCategoryViewModel.Name;
            var category = new CategoryDTO { Name = categoryName };
            await _categoryRepository.AddCategoryAsync(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCategoryName(int id, string newName)
        {
            var category = new CategoryDTO { Id = id, Name = newName };
            await _categoryRepository.UpdateCategoryAsync(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}