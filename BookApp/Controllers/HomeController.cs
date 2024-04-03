using BookApp.Core.DTO;
using BookApp.Data;
using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/")]
        public IActionResult Index(string chosenBookId, string chosenBookTitle, string chosenBookAuthor)
        {
            ViewBag.ChosenBookId = chosenBookId;
            ViewBag.ChosenBookTitle = chosenBookTitle;
            ViewBag.ChosenBookAuthor = chosenBookAuthor;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}