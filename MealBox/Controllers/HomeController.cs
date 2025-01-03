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
            // Kullanıcının ID'sini session'dan alıyoruz
            var userIdString = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userIdString))
            {
                return Json(new { success = false, message = "Kullanıcı girişi yapılmamış" });
            }

            int userId = int.Parse(userIdString);
            var user = c.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                // Kullanıcının konum bilgisini güncelliyoruz
                user.Latitude = latitude;
                user.Longitude = longitude;
                c.SaveChanges();  // Değişiklikleri veritabanına kaydediyoruz
                return Json(new { success = true, message = "Konum başarıyla kaydedildi" });
            }

            return Json(new { success = false, message = "Kullanıcı bulunamadı" });
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
