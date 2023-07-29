using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPrn211.Models;

namespace ProjectPrn211.Controllers
{
	public class LoginController : Controller
	{
		private readonly AssignmentPrjContext _dbContext;

		public LoginController(AssignmentPrjContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IActionResult Index()
		{
            var existingCookie = Request.Cookies["UserName"];

			if (existingCookie != null)
            {
				var user = _dbContext.Users.FirstOrDefault(u => u.Account == existingCookie);

				if (user != null)
                {
					return RedirectToAction("Index", "Booking", user);

                }
                else
                {
                    Response.Cookies.Delete("UserName");
                    return RedirectToAction("Index", "_404");
                }




            }
            else
            {
                return View(); 
            }
		}

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            string existingCookie = Request.Cookies["UserName"];

            if (existingCookie != null)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Account == existingCookie);
                if(user !=null)
                return RedirectToAction("Index", "Booking", user);
                else
                {
                    return View("Index");
                }
            }
            else
            {


                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Account == model.Account);

                if (user != null && user.Password == model.Password)
                {


                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        Secure = true,
                        // Cookie chỉ có thể truy cập từ phía máy chủ
                        HttpOnly = true
                    };

                    Response.Cookies.Append("UserName", user.Account, options);

                    return RedirectToAction("Index", "Booking",user);
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập không hợp lệ");
                    return View("Index", model);
                }
            }
        }

        public IActionResult Logout()
        {
            var existingCookie = Request.Cookies["UserName"];

            if (existingCookie != null)
            {
                Response.Cookies.Delete("UserName");
            }
            return RedirectToAction("Index", "Home");
        }



    }
}
