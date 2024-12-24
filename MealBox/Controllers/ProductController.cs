using MealBox.Models.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace MealBox.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            //Ürünlerle ilişkili category ve yöneticileri dahil ederek status= true ürünleri listeler
            var product = c.Products.Include(x => x.Category).Include(x => x.Admin).Where(x=>x.Status==true).ToList();
            
            return View(product);
        }
        //Ürün ekleme sayfasına yönlendirme yapar
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
        public ActionResult GetProduct(int id) {
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
            return View("GetProduct",productValue);
        }
        //GÜncelleme işlemi yapar
        public ActionResult UpdateProduct(Product p) { 
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

    }
 
}
