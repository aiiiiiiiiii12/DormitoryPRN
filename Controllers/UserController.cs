using Microsoft.AspNetCore.Mvc;
using ProjectPrn211.Models;
using System.Security.Principal;

namespace ProjectPrn211.Controllers
{
    public class UserController : Controller
    {
        private readonly AssignmentPrjContext _dbContext;
        public UserController(AssignmentPrjContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(string UserName)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Account == UserName);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["User"] = user;

                return View();

            }

        }

        public IActionResult payment(string Account)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Account.Equals(Account));
            if (user == null)
            {
                return RedirectToAction("Index", "_404");
            }
            else
            {
                ViewData["user"] = user;
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddMoney(string account, string payment)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Account.Equals(account));
            if (user == null)
            {
                return RedirectToAction("Index", "_404");
            }
            else
            {
                decimal paymentValue;
                if (decimal.TryParse(payment, out paymentValue))
                {
                    user.Money += paymentValue;
                    _dbContext.Update(user);
                    _dbContext.SaveChanges();
                    ViewData["User"] = user;
                }
                else
                {
                    return RedirectToAction("Index", "_404");
                }

            }

            return View("PaymentSuccess");
        }


        [HttpPost]
        public ActionResult UpdateProfile(User user)
        {
            string UserName="";
            if (user != null)
            {
                var user1 = _dbContext.Users.FirstOrDefault(x => x.Account ==user.Account);
                if (user != null)
                {
                    UserName =user.Account;

                    user1.Email = user.Email;
                    user1.Name = user.Name;
                    user1.Address = user.Address;
                    user1.Dateofbirth = user.Dateofbirth;
                    _dbContext.SaveChanges();

                }

            }

            return RedirectToAction("Index", "User", new { username = UserName });
        }
    }
}
