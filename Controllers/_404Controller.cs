using Microsoft.AspNetCore.Mvc;

namespace ProjectPrn211.Controllers
{
	public class _404Controller : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
