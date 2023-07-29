using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPrn211.Models;

namespace ProjectPrn211.Controllers
{
	public class BookingController : Controller
	{
		private readonly AssignmentPrjContext _dbContext;

		public BookingController(AssignmentPrjContext dbContext)
		{
			_dbContext = dbContext;
		}
		public IActionResult Index(User user)
		{

            if (user != null)
			{
                
				var rooms = _dbContext.Rooms.Include(r => r.Rtype).ToList();
                var bookings = _dbContext.Bookings.Where(r => r.Account == user.Account).ToList();

                ViewData["bookings"] = bookings;
                ViewData["user"] = user;
				ViewData["Rooms"] = rooms;

				return View();
			}
			else
			{
				return RedirectToAction("Index","Login");
			}
		}

        public IActionResult bookroom(string Account, string RoomId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Account == Account);
            var room = _dbContext.Rooms.Include(r => r.Rtype).FirstOrDefault(u => u.Roomid == int.Parse(RoomId));

            if (user != null && room != null)
            {
                if (user.Money < room.Rtype.Price)
                {
                    return RedirectToAction("payment", "User", new { account = Account });
                }
                else
                {
                    user.Money -= room.Rtype.Price;
                    Booking booking = new Booking()
                    {
                        RoomId = room.Roomid,
                        Account = Account,
                        InDate = DateTime.Now,
                        Confirmroom = false
                    };
                    _dbContext.Bookings.Add(booking);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index", "User", new { UserName = Account });
                }
            }
            else
            {
                return NotFound();
            }
        }




    }
}
