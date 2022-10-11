using Microsoft.AspNetCore.Mvc;

namespace ProjName.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int id)
        {
            return RedirectToAction("Index");
        }
    }
}
