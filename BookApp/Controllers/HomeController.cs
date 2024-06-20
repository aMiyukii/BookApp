using Microsoft.AspNetCore.Mvc;
using BookApp.Core.Interfaces;

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

        public IActionResult CreateAccount()
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
                // Retrieve user ID from database or some service method
                var userId = await _userService.GetUserIdAsync(Emailaddress);

                // Store user ID in session
                HttpContext.Session.SetInt32("UserId", userId);

                return RedirectToAction("Index", "Library");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View("Index");
            }
        }

        public IActionResult Logout()
        {
            // Clear user ID from session on logout
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", "Home");
        }
    }
}