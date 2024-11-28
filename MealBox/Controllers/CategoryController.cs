
using MealBox.Models.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealBox.Controllers
{
    [AllowAnonymous]
    public class CategoryController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            var degerler = c.Categorys.ToList();
            return View(degerler);

        }
        [HttpGet]
        public IActionResult AddCategory()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category k)
        {

            c.Categorys.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RemoveCategory(int id)
        {
            var ctg = c.Categorys.Find(id);
            c.Categorys.Remove(ctg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetCategory(int id)
        {
            var kategori = c.Categorys.Find(id);
            return View("GetCategory",kategori);
        }
        public ActionResult UpdateCategory(Category k) { 
            var ctgr = c.Categorys.Find(k.CategoryID);
            ctgr.CategoryName = k.CategoryName;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
