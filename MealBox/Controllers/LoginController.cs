using MealBox.Models;
using MealBox.Models.Classes;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace MealBox.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
      
        

      
        public IActionResult Index()
        {
            return View();
        }

        //Register = Partial1
        //[HttpGet]
        
        //public PartialViewResult Partial1()
        //{
        //    return PartialView();
        //}
        //[HttpPost]
       
        //public async Task<PartialViewResult> Partial1(UserRegisterViewModel p)
        //{
        //   Admin admin = new Admin()
        //   {
        //       UserName = p.UserName,
        //       Email =p.Mail,
        //       Phone=p.Phone,

        //       //Şifre burda alınmadı çünkü backendde hashleniyor
        //   };
        //    if (p.Password ==p.ConfirmPassword)
        //    {
        //        var result = await userManager.CreateAsync(admin,p.Password);

        //        if (result.Succeeded)
        //        {
        //            return PartialView("Partial1");
        //        }
        //        else { 
        //            foreach(var item in result.Errors)
        //            {
        //                ModelState.AddModelError("",item.Description);
        //            }
                
        //        }
        //    };
        //    return PartialView(p);   
        //}
       
    }

}
