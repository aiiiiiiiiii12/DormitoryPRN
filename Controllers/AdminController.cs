using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPrn211.Models;

namespace ProjectPrn211.Controllers
{
    public class AdminController : Controller
    {
		private readonly AssignmentPrjContext _dbContext;
		public AdminController(AssignmentPrjContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IActionResult Index(Admin _admin)
		{
			var admin = _dbContext.Admins.FirstOrDefault(a => a.Username == _admin.Username);

			if (admin == null)
			{
				
				return RedirectToAction("Index", "LoginAdmin");

			}
			else
			{

				var Users = _dbContext.Users.ToList();
				ViewData["admin"] = admin;
				ViewData["Users"] = Users;

				return View();
			}
        }

		public IActionResult DetailUser(string Account)
		{
			var user = _dbContext.Users.FirstOrDefault(u => u.Account == Account);
			if(user !=null)
			{
				ViewData["user"] = user;
				return View();
			}
			return RedirectToAction("Index", "_404");

		}

		public IActionResult AllRoom()
		{
   //         var admin = _dbContext.Admins.FirstOrDefault( a => a.Username == Account);

			//if (admin != null)
			//{
				var rooms = _dbContext.Rooms.
				Include(b => b.Building)
				.Include(c => c.Rtype)
				.ToList();

				ViewData["Rooms"] = rooms;
			//	ViewData["Admin"] = admin;
				
			//}
			return View();


		}
		public IActionResult booking()
		{
			var bookings = _dbContext.Bookings.ToList();
			ViewData["bookings"] = bookings;
			if (TempData.ContainsKey("ErrorMessage"))
			{
				ViewData["ErrorMessage"] = TempData["ErrorMessage"];
			}

			return View();
		}
		public IActionResult donebooking()
		{
			var bookings = _dbContext.Bookings.ToList();
			ViewData["bookings"] = bookings;
			return View();

		}
		public IActionResult confirmbooking(string Bookingid)
		{
			var booking = _dbContext.Bookings
				.FirstOrDefault(x => x.BookingId == int.Parse(Bookingid));

			if (booking != null)
			{
				var room = _dbContext.Rooms.Include(r => r.Rtype).FirstOrDefault(x => x.Roomid == booking.RoomId);
				var user =_dbContext.Users.FirstOrDefault(x => x.Account == booking.Account);
				if(room.Member < room.Rtype.Number)
				{
					room.Member += 1;
					booking.Confirmroom = true;
					user.Inroom = true;

				}
				else
				{
					TempData["ErrorMessage"] = $"Không đủ chỗ cho user {booking.Account}";

				}
				_dbContext.SaveChanges();

			}
			return RedirectToAction("booking");
		}

		public IActionResult deletebooking(string Bookingid)
		{
			var booking = _dbContext.Bookings
			
				.FirstOrDefault(x => x.BookingId == int.Parse(Bookingid));
			if (booking != null)
			{
				var user = _dbContext.Users.FirstOrDefault(x => x.Account == booking.Account);
				var room = _dbContext.Rooms.FirstOrDefault(x => x.Roomid == booking.RoomId);
				if (user != null)
				{
					user.Inroom = false;
				}
				room.Member -=1;

				TempData["ErrorMessage"] = $"Đã xóa booking của người dùng: {booking.Account}";

				_dbContext.Remove(booking);
				_dbContext.SaveChanges();
			}
			return RedirectToAction("booking");

		}




	}
}
