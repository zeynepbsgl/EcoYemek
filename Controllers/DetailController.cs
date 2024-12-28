using Microsoft.AspNetCore.Mvc;

namespace MealBox.Controllers
{
    public class DetailController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
