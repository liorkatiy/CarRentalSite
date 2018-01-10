using System.Web.Mvc;
using YoRent.Models;

namespace YoRent.Controllers
{
    public class HomeController : Controller
    {
        //home page
        public ActionResult Index()
        {
            return View();
        }

        //return the car search page
        public ActionResult Cars()
        {
            return View();
        }

        //about page
        public ActionResult About()
        {
            return View();
        }
    }
}