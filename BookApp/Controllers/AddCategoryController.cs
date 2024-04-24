using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    public class AddCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddCategory()
        {

            return View();
        }
    }
}
