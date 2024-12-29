using MealBox.Models;
using MealBox.Models.Classes;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace MealBox.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<LoginController> _logger;
        private readonly Context c;

        public LoginController(IUserService userService, ILogger<LoginController> logger)
        {
            _userService = userService;
            _logger = logger;
            c = new Context();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }

       

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.AddUserAsync(user);
                    return RedirectToAction("Index", "Login");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error registering user");
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
            }
            return View("Partial1", user);
        }

        [HttpPost]
        public IActionResult Login(string Mail, string Password)
        {
            // User tablosunda eþleþen kullanýcýyý bul
            var user = c.Users.FirstOrDefault(u => u.Mail == Mail && u.Password == Password);
            if (user != null)
            {
                // Kullanýcý oturumu baþlatmak için Session veya Cookie kullanýlabilir.
                HttpContext.Session.SetString("UserID", user.Id.ToString());
                return RedirectToAction("Index", "Home");
            }

            // Giriþ baþarýsýz olduysa hata mesajý ekle
            ModelState.AddModelError("", "Geçersiz e-posta veya þifre");
            return View("Index");
        }
    }

}
