using BookApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using BookApp.Models;
using BookApp.Data;
using BookApp.Core.Models;
using BookApp.Core.DTO;

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

            var viewModel = new CategoryViewModel
            {
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddCategoryName(CategoryViewModel categoryViewModel)
        {
            var category = new Category
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name
            };

            Console.WriteLine($"Category added with name: {categoryViewModel.Name}");

            categoryRepository.AddCategory(category);

            var categories = categoryRepository.GetAllCategory();

            var viewModel = new CategoryViewModel
            {
                Categories = categories
            };

            return View("Index");
        }


    }
}
