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
                var userId = await _userService.GetUserIdAsync(Emailaddress);

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
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAccount(string Name, string Emailaddress, string Password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Emailaddress) || string.IsNullOrWhiteSpace(Password))
                {
                    ViewBag.ErrorMessage = "Name, email, and password are required.";
                    return View();
                }

                bool userExists = await _userService.UserExistsAsync(Emailaddress);
                if (userExists)
                {
                    ViewBag.ErrorMessage = "User with this email address already exists.";
                    return View();
                }

                bool createUserResult = await _userService.CreateUserAsync(Name, Emailaddress, Password);
                if (createUserResult)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to create user account. Please try again.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error creating user account: " + ex.Message;
                return View();
            }
        }
    }
}