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
            // 1. İlgili ürünü al
            var mainProduct = _context.Products.FirstOrDefault(p => p.ProductID == id);
            if (mainProduct == null)
            {
                return NotFound();
            }

            var relatedProducts = _context.Products
                                .Where(p => p.ProductID != id // Şu anki ürünü hariç tut
                                         && p.Status == true // Aktif ürünler
                                         && p.CategoryId == mainProduct.CategoryId) // Aynı kategori
                                .OrderByDescending(p => p.ProductID) // ID'ye göre tersten sırala
                                .Take(4) // Son 4 ürünü al
                                .ToList();

            // 3. Verileri ViewData ile gönder
            ViewData["MainProduct"] = mainProduct;
            ViewData["RelatedProducts"] = relatedProducts;

            return View();


        }

    }
}
