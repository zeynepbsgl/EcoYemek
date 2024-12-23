using MealBox.Models;
using MealBox.Models.Classes;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace MealBox.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {



        Context c = new Context();
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Partial1(Admin p)
        {
            c.Admins.Add(p);
            c.SaveChanges();
            return PartialView();




        }



    }

}
