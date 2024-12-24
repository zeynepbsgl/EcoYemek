using Microsoft.AspNetCore.Mvc;
using MealBox.Models.Classes;
using MealBox.Helpers; // DistanceHelper sınıfını dahil ediyoruz
using Microsoft.EntityFrameworkCore;

namespace MealBox.Controllers
{
    [Route("api/[controller]")] // API controller rotası
    [ApiController] // API controller olduğunu belirtir
    public class ProductApiController : ControllerBase
    {
        private readonly Context _context;

        public ProductApiController(Context context)
        {
            _context = context;
        }

        // Kullanıcıya yakın ürünleri döndüren API metodu
        [HttpGet("nearby")]
        public IActionResult GetNearbyProducts(decimal userLat, decimal userLon, double maxDistance)
        {
            var products = _context.Products.Where(x => x.Status == true).ToList(); // Geçerli ürünler
            var nearbyProducts = products.Where(product =>
                DistanceHelper.CalculateDistance((double)userLat, (double)userLon, (double)product.Latitude, (double)product.Longitude) <= maxDistance)
                .ToList();

            return Ok(nearbyProducts); // Yakın ürünleri döndür
        }
    }
}
