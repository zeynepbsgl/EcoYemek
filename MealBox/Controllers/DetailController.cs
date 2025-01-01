using Microsoft.AspNetCore.Mvc;
using MealBox.Models.Classes;  // Context ve diğer modeller için

namespace MealBox.Controllers
{
    public class DetailController : Controller
    {
        private readonly Context _context;  // ApplicationDbContext yerine Context

        // Context sınıfını enjekte et
        public DetailController(Context context)
        {
            _context = context;
        }

        // Ürün detayını almak için ID parametresini al
        public IActionResult Index(int id)
        {
            var products = _context.Products.Where(p => p.ProductID == id).ToList();  // Liste olarak al
            if (products == null || !products.Any())
            {
                return NotFound();
            }

            return View(products);  // Listeyi view'a gönder
        }

    }
}
