using BookApp.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using BookApp.Models;
using BookApp.Data;
using BookApp.Core.Models;
using System.Collections.Generic;
using BookApp.Models;

namespace BookApp.Controllers
{
    public class AddCategoryController : Controller
    {
        private readonly CategoryRepository categoryRepository;

        public AddCategoryController()
        {
            categoryRepository = new CategoryRepository();
        }

        public IActionResult Index()
        {   
            var categories = categoryRepository.GetAllCategory();

            var viewModel = new AddCategoryViewModel(categories);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddCategoryName(AddCategoryViewModel addCategoryViewModel)
        {
            var categoryName = addCategoryViewModel.Name;

            var category = new Category
            {
                Name = categoryName
            };

            categoryRepository.AddCategory(category);

            var categories = categoryRepository.GetAllCategory();
            var viewModel = new AddCategoryViewModel(categories);
            
            return View("Index", viewModel);
        }

        [HttpPost]
        public ActionResult UpdateCategoryName(int id, string newName)
        {
            var category = new Category
            {
                Id = id,
                Name = newName
            };

            categoryRepository.UpdateCategory(category);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            categoryRepository.DeleteCategory(id);

            return RedirectToAction("Index");
        }

    }
}