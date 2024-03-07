using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using BookApp.Core.Models;
using BookApp.Data;
using BookApp.Core.DTO;

namespace BookApp.Controllers
{
    public class AddBookController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddBookTitle(BookViewModel BookViewModel)
        {
           


            return RedirectToAction("Index", "Home");
        }
    }
}
