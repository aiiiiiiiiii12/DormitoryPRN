using Microsoft.AspNetCore.Mvc;

namespace ProjectPrn211.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
