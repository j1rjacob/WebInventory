using System.Web.Mvc;

namespace WebInventory.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult SingleProduct()
        {
            return View();
        }
    }
}