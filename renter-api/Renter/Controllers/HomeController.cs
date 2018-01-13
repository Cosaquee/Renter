using Microsoft.AspNetCore.Mvc;

namespace Renter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}