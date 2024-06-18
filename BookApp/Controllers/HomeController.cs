using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookApp.Core.Services;
using BookApp.Core.Interfaces;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Emailaddress, string Password)
        {
            if (string.IsNullOrWhiteSpace(Emailaddress) || string.IsNullOrWhiteSpace(Password))
            {
                ViewBag.ErrorMessage = "Email and password are required.";
                return View("Index");
            }

            var isValidUser = await _userService.LoginAsync(Emailaddress, Password);
            if (isValidUser)
            {
                return RedirectToAction("Index", "Library");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View("Index");
            }
        }
    }
}