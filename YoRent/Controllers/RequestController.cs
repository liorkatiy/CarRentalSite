using Entity;
using MvcUtilitys;
using System.Web.Mvc;
using YoRent.Models;

namespace YoRent.Controllers
{
    public class RequestController : Controller
    {
        //request all the branches
        [HttpPost]
        public JsonResult GetBranches()
        {
            using (BranchesData db = new BranchesData())
            {
                var branches = db.GetAll(branch => new
                {
                    latitude=branch.latitude,
                    longitude = branch.longitude,
                    id = branch.Id,
                    name = branch.name
                });
                return Json(branches);
            }
        }

        //check if the user name isnt already in the database
        [HttpPost]
        public JsonResult UserNameAvailable(string validate)
        {
            using (UsersData db = new UsersData())
            {
                return Json(!db.Contains(validate));
            }
        }

        //check if the user IdCard isnt already in the database
        [HttpPost]
        public JsonResult IdCardAvailable(string validate)
        {
            using (UsersData db = new UsersData())
            {
                return Json(!db.ContainsByIdCard(validate));
            }
        }
        //get all the current user orders
        //return all the user orders from the user id
        [AuthorizeUser(Premission = Premission.User)]
        [HttpPost]
        public ActionResult MyOrders()
        {
            using (UsersData db = new UsersData())
            {
                string id = Session.GetUserID();
                var orders = db.GetOrdersByID(id, FullOrderData.Create);
                return Json(orders);
            }
        }

        //if the user order is ok return true and add it to the database
        [HttpPost]
        public JsonResult SetOrder(int creditCard)
        {
            bool orderToDb = Session.SetOrderToDatabase(creditCard);
            return Json(orderToDb);
        }

        //return cars from search
        [HttpPost]
        public JsonResult GetCars(SearchBy search)
        {
            if (ModelState.IsValid)
            {
                using (CarsData db = new CarsData())
                {
                    CarView[] carsFromSearch = db.GetCarsByDate(search, CarView.Create);
                    return Json(carsFromSearch);
                }
            }
            return Json(new string[0]);
        }
    }
}