using System.Web.Mvc;

namespace WebInventory.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Title = "Home Page";

            return PartialView();
        }
    }
}
