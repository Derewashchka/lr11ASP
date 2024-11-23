using lr11.Filters;
using Microsoft.AspNetCore.Mvc;

namespace lr11.Controllers
{
    [ServiceFilter(typeof(ActionLoggerFilter))]
    [ServiceFilter(typeof(UniqueUsersFilter))]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return Content("About Page");
        }

        public IActionResult Contact()
        {
            return Content("Contact Page");
        }
    }
}