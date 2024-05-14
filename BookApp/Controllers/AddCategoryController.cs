using BookApp.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using BookApp.Models;
using BookApp.Data;
using BookApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    public class AddCategoryController : Controller
    {
        private readonly CategoryRepository categoryRepository;

        public AddCategoryController()
        {
            categoryRepository = new CategoryRepository();
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryRepository.GetAllCategoryAsync();

            var viewModel = new AddCategoryViewModel(categories);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategoryName(AddCategoryViewModel addCategoryViewModel)
        {
            var categoryName = addCategoryViewModel.Name;

            var category = new CategoryDTO
            {
                Name = categoryName
            };

            await categoryRepository.AddCategoryAsync(category);

            var categories = await categoryRepository.GetAllCategoryAsync();
            var viewModel = new AddCategoryViewModel(categories);
            
            return View("Index", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCategoryName(int id, string newName)
        {
            var category = new CategoryDTO
            {
                Id = id,
                Name = newName
            };

            await categoryRepository.UpdateCategoryAsync(category);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await categoryRepository.DeleteCategoryAsync(id);

            return RedirectToAction("Index");
        }
    }
}