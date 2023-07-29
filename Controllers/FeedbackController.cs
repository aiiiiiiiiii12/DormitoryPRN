using Microsoft.AspNetCore.Mvc;
using ProjectPrn211.Models;

namespace ProjectPrn211.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly AssignmentPrjContext _dbContext;

        public FeedbackController(AssignmentPrjContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(IFormCollection f)
        {
            var existingCookie = Request.Cookies["UserName"];

            if (existingCookie != null)
            {
                var Title = f["Title"];
                string Message = f["Message"];
                string username = existingCookie;

                Feedback feedback = new Feedback()
                {
                    Account = username,
                    Feedback1 = Message,
                    Felling = Title,
                    Email = _dbContext.Users.Find(username).Email,
                    Name = username,

                };

                _dbContext.Add(feedback);
                _dbContext.SaveChanges();
                
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
