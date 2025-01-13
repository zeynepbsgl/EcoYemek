using MealBox.Models.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace MealBox.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller

    {


        Context c = new Context();
        public IActionResult Index()
        {

            var userIdString = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Login");
            }

            var userId = int.Parse(userIdString);
            var user = c.Users.Find(userId);

            if (user == null || user.Latitude == null || user.Longitude == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var products = c.Products
                .Include(x => x.Category)
                .Include(x => x.User)
                .Where(x => x.Status == true)
                .ToList();

            // Mesafeleri hesapla ve kaydet
            foreach (var product in products)
            {
                if (product.Latitude.HasValue && product.Longitude.HasValue)
                {
                    // Veritabanındaki koordinatları doğru formatta düzelt
                    double correctedLatitude = CorrectCoordinateFormat(product.Latitude.Value);
                    double correctedLongitude = CorrectCoordinateFormat(product.Longitude.Value);

                    // Kullanıcının ve ürünün mesafesini hesapla
                    double distance = CalculateDistance(user.Latitude.Value, user.Longitude.Value, correctedLatitude, correctedLongitude);
                    product.Distance = distance;  // Mesafeyi ürüne ata
                }
                else
                {
                    product.Distance = 0;  // Eğer ürünün konumu yoksa mesafeyi 0 olarak ayarla
                }
            }

            // Veritabanına kaydet
            c.SaveChanges();
            
            // Ürünleri mesafeye göre sırala
            var sortedProducts = products.OrderBy(p => p.Distance).ToList();

            return View(sortedProducts);
        }
       
        [HttpGet]
        public ActionResult NewProduct()
        {
            //DropdownList
            List<SelectListItem> value1 = (from x in c.Categorys.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            //degerleri viewe Viewbag ile atıyoruz
            ViewBag.val1=value1;
            return View();
        }
        //Yeni ürünleri veritanbanına kaydeder ve Indexe yönlendirme yapar.
        [HttpPost]
        public ActionResult NewProduct(Product p)
        {

            // Oturumdan kullanıcı kimliğini al
            var userIdString = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdString))
            {
                // Kullanıcı oturum açmamışsa
                return RedirectToAction("Login", "Login");
            }

            // UserID'yi int'e dönüştür
            var userId = int.Parse(userIdString);

            // Kullanıcı kimliğini ürüne ata
            p.UserId = userId;



            if (!p.Status.HasValue)
            {
                p.Status = true;  // Status'ü true olarak ayarlıyoruz.
            }


            // Eğer gelen veri virgül ile geliyorsa, bunu nokta ile değiştirebilirsiniz
            double latitude = Convert.ToDouble(Request.Form["Latitude"].ToString().Replace(',', '.'));
            double longitude = Convert.ToDouble(Request.Form["Longitude"].ToString().Replace(',', '.'));

            p.Latitude = latitude;
            p.Longitude = longitude;


            c.Products.Add(p);
            c.SaveChanges();


            return RedirectToAction("Index");



        }
        //Veritabanından id yi bulur pasif hale getirir
        public ActionResult DeleteProduct(int id)
        {
            var value = c.Products.Find(id);

            value.Status = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        // Güncellenecek ürünü bulur detaylarını alır
        public ActionResult GetProduct(int id)
        {
            //DropdownList
            List<SelectListItem> value1 = (from x in c.Categorys.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            //degerleri viewe Viewbag ile atıyoruz
            ViewBag.val1=value1;
            var productValue = c.Products.Find(id);
            return View("GetProduct", productValue);
        }
        //GÜncelleme işlemi yapar
        public ActionResult UpdateProduct(Product p)
        {
            var prodct = c.Products.Find(p.ProductID);
            prodct.Status = p.Status;
            prodct.ProductName = p.ProductName;
            prodct.Province = p.Province;
            prodct.Brand = p.Brand;
            prodct.District = p.District;
            prodct.Price = p.Price;
            prodct.Image = p.Image;
            prodct.Stock = p.Stock;
            prodct.ProductDescription = p.ProductDescription;
            prodct.CategoryId=p.CategoryId;
            prodct.Town = p.Town;
            c.SaveChanges();
            return RedirectToAction("Index");



        }

        private double CorrectCoordinateFormat(double coordinate)
        {
            if (coordinate.ToString().Length >= 8) // 8 veya daha fazla basamaklı değerler için düzeltme
            {
                return coordinate / 10000000.0;
            }
            return coordinate; // Zaten doğruysa döndür
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadiusKm = 6371; // Dünya'nın yarıçapı

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public IActionResult TestDistance()
        {
            // Test Koordinatları
            double userLat = 39.92077; // Ankara
            double userLon = 32.85411; // Ankara

            var testLocations = new List<(string Name, double Latitude, double Longitude)>
    {
        ("Istanbul", 41.0082, 28.9784),
        ("Izmir", 38.4192, 27.1287),
        ("Sakarya", 40.7488, 30.6002)
    };

            var results = new List<string>();

            foreach (var location in testLocations)
            {
                double distance = CalculateDistance(userLat, userLon, location.Latitude, location.Longitude);
                results.Add($"Distance to {location.Name}: {distance:F2} km");
            }

            // Log veya ViewBag ile gönder
            ViewBag.TestResults = results;

            return View(); // İstersen sonuçları döndürmek için bir View yapabilirsin
        }





    }
}
