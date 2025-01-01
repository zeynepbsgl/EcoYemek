using MealBox.Models.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MealBox.Controllers
{
   
    public class ProfileController : Controller
    {
        private readonly Context _context = new Context();

        // Profil sayfası
        public IActionResult Index()
        {
            var userIdString = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Login");
            }

            var userId = int.Parse(userIdString);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View(user);
        }

        // Profil güncelleme (GET)
        [HttpGet]
        public IActionResult EditProfile()
        {
            var userIdString = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Login");
            }

            var userId = int.Parse(userIdString);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View(user);
        }

        // Profil güncelleme (POST)
        [HttpPost]
        public IActionResult EditProfile(User updatedUser)
        {
            var userIdString = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Login");
            }

            var userId = int.Parse(userIdString);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }

            // Kullanıcı bilgilerini güncelle
            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;
            user.Mail = updatedUser.Mail;
            user.Phone = updatedUser.Phone;
            user.Latitude = updatedUser.Latitude;
            user.Longitude = updatedUser.Longitude;

            _context.SaveChanges();  // Veritabanını güncelle

            return RedirectToAction("Index");  // Profil sayfasına yönlendir
        }
    }
}
