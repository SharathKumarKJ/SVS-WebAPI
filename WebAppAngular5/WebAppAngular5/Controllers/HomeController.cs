using System.Web.Mvc;
using WebAppAngular5.Base;

namespace WebAppAngular5.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IRepository repository)
        {

        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
