using System.Web.Mvc;
using Entity;
using YoRent.Models;
using MvcUtilitys;

namespace YoRent.Controllers
{
    [AuthorizeUser(Premission =Premission.Employee)]
    public class EmployeeController : Controller
    {
        //return the employee page
        public ActionResult ReturnCar()
        {
            return View();
        }

        //Return All Orders From User Id Card
        [HttpPost]
        public JsonResult GetOrders(string search)
        {
            using (UsersData db = new UsersData())
            {
                var orders = db.GetActiveOrders(search, FullOrderData.Create);
                return Json(orders);
            }
        }

        //set the order as returned 
        [HttpPost]
        public JsonResult ReturnCar(int orderId)
        {
            using (OrdersData db = new OrdersData())
            {
                var returned = db.ReturnCar(orderId);
                return Json(returned);
            }
        }

    }
}