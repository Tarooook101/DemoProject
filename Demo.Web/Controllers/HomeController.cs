using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    public class HomeController : Controller
    {

        // Main Page
        public IActionResult Index()
        {

            return View();
        }
    }
}
