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


            // 3. Ürün sahibine yapılan yorumları al
            var userComments = _context.Comments
                .Where(c => c.UserId == mainProduct.UserId) // Ürün sahibine yapılan yorumlar
                .OrderByDescending(c => c.CommentDate) // Yorum tarihine göre sırala
                .ToList();

            // 3. Verileri ViewData ile gönder
            ViewData["MainProduct"] = mainProduct;
            ViewData["RelatedProducts"] = relatedProducts;
            ViewData["UserComments"] = userComments;


            return View();


        }

        // Ürün sahibine yorum ekleme işlemi
        [HttpPost]
        public IActionResult AddComment(int userId, string commentContent)
        {
            // Oturum kontrolü
            var userIdString = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Login");
            }

            var commenterName = _context.Users
                .Where(u => u.Id == int.Parse(userIdString))
                .Select(u => u.Name)
                .FirstOrDefault();

            // Yeni yorum oluştur
            var newComment = new Comment
            {
                CommentUser = commenterName,
                UserId = userId,
                CommentContent = commentContent,
                CommentDate = DateTime.Now,
                CommentStatus = true // Varsayılan olarak aktif
            };

            _context.Comments.Add(newComment);
            _context.SaveChanges();

            // Yorumu ekledikten sonra doğru ürüne dön
            var product = _context.Products.FirstOrDefault(p => p.UserId == userId && p.Status == true);
            if (product != null)
            {
                return RedirectToAction("Index", new { id = product.ProductID });
            }

            // Eğer ürün bulunamazsa ana sayfaya yönlendir
            return RedirectToAction("Index", "Home");

        }

       

    }
}
