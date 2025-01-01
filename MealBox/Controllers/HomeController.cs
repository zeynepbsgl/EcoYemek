using MealBox.Models.Classes;
using MealBox.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace MealBox.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        Context c = new Context();  

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
          
            var homeProducts = c.Products.Where(p => p.Status == true).ToList();
            return View(homeProducts);
        }
        [HttpPost]
        public IActionResult GetUserLocation(double latitude, double longitude)
        {
            // Kullanýcýnýn ID'sini session'dan alýyoruz
            var userIdString = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userIdString))
            {
                return Json(new { success = false, message = "Kullanýcý giriþi yapýlmamýþ" });
            }

            int userId = int.Parse(userIdString);
            var user = c.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                // Kullanýcýnýn konum bilgisini güncelliyoruz
                user.Latitude = latitude;
                user.Longitude = longitude;
                c.SaveChanges();  // Deðiþiklikleri veritabanýna kaydediyoruz
                return Json(new { success = true, message = "Konum baþarýyla kaydedildi" });
            }

            return Json(new { success = false, message = "Kullanýcý bulunamadý" });
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
