using Microsoft.AspNetCore.Mvc;

namespace MealBox.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
