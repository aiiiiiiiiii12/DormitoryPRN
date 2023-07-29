using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPrn211.Models;

namespace ProjectPrn211.Controllers
{
    public class LoginAdminController : Controller
    {
        private readonly AssignmentPrjContext _dbContext;
        public LoginAdminController(AssignmentPrjContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin _admin)
        {
            var admin = await _dbContext.Admins.FirstOrDefaultAsync(a => a.Username == _admin.Username);  
            if (admin != null && admin.Password == _admin.Password)
            {
                return RedirectToAction("Index", "Admin", admin );
            }
            
            return View("Index");
        }
    }
}
