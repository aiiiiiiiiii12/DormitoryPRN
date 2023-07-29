using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPrn211.Models;
using System.IO;


namespace ProjectPrn211.Controllers
{
    public class RoomController : Controller
    {
        private readonly AssignmentPrjContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public RoomController(AssignmentPrjContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var buildings = _dbContext.Buildings.ToList();
            var roomtypes = _dbContext.Roomtypes.ToList();

            ViewData["bookings"] = buildings;
            ViewData["roomtypes"] = roomtypes;
            return View();
        }
        [HttpPost]
        public IActionResult AddRoom(Room room, IFormFile roomImage)
        {
            if (room !=null)
            {
                // Lưu hình ảnh vào thư mục wwwroot/imgroom/
                if (roomImage != null && roomImage.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "imgroom");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + roomImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        roomImage.CopyTo(stream);
                    }

                    room.RoomImg = "imgroom/" + uniqueFileName;
                }

             
                

                _dbContext.Rooms.Add(room);
                _dbContext.SaveChanges();

                return RedirectToAction("AllRoom","Admin");
            }

            return View(room);
        }
        

    }
}