using MealBox.Models.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace MealBox.Controllers
{
    public class BaseController : Controller
    {
        protected readonly Context _context;

        public BaseController()
        {
            _context = new Context();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // Tüm sayfalarda ViewBag'e kategorileri ekle
            ViewBag.Categories = _context.Categorys.ToList();
        }
    }
}
