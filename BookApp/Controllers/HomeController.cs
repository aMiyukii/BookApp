using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookApp.Core.Services;
using BookApp.Core.DTO;
using BookApp.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ILoginManager _loginManager;

        public HomeController(ILogger<HomeController> logger, IUserService userService, ILoginManager loginManager)
        {
            _logger = logger;
            _userService = userService;
            _loginManager = loginManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Emailaddress, string Password)
        {
            return RedirectToAction("Index", "Library");
        }
    }
}