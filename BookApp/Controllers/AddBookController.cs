using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using BookApp.Core.Models;

namespace BookApp.Controllers
{
    public class AddBookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddBookTitle(AddBookViewModel addBookViewModel)
        {
            var book = new Book
            {
                Title = addBookViewModel.Title
            };

            return RedirectToAction("Index", "Home");
        }
    }
}
