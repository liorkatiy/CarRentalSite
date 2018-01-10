using System.Web;
using System.Web.Mvc;
using Entity;
using YoRent.Models;
using System.IO;
using MvcUtilitys;

namespace YoRent.Controllers
{
    /*
     View section Have All The Tables Views
     All Other Sections Have Method That Return Json Result:
        method to get items from search string
        add item to the database return true if sucssess
        remove item from database return true if sucssess
        update item into database return true if sucssess
         */

    [AuthorizeUser(Premission =Premission.Manger)]
    public class AdminController : Controller
    {
        #region Views
        //Cars managment page
        public ActionResult CarManager()
        {
            return View();
        }
        
        //car type managment page
        public ActionResult CarTypeManager()
        {
            return View();
        }
        
        //branches managment page
        public ActionResult BranchManager()
        {
            return View();
        }

        //users managment page
        public ActionResult UserManager()
        {
            return View();
        }

        //order managment page
        public ActionResult OrderManager()
        {
            return View();
        }

        #endregion

        #region Car

        [HttpPost]
        public JsonResult GetCar(string search)
        {
            using (CarsData db = new CarsData())
            {
                var cars = db.GetFromSearch(search, car => new
                {
                    registrationPlate = car.registrationplate,
                    km = car.km,
                    available = car.available,
                    branch = car.branch,
                    cartype = car.cartype
                });
                return Json(cars);
            }
        }

        [HttpPost]
        public JsonResult AddCar(CarValidation car)
        {
            using (CarsData db = new CarsData())
            {
                car.Available = true;
                return Json(db.Add(car));
            }
        }

        [HttpPost]
        public JsonResult UpdateCar(CarValidation car,string registrationplate)
        {
            using (CarsData db = new CarsData())
            {
                bool updated = db.Update(_car => _car.registrationplate == registrationplate, car);
                return Json(updated);
            }
        }

        [HttpPost]
        public JsonResult DeleteCar(string id)
        {
            using(CarsData db = new CarsData())
            {
                bool removed = db.Delete(car => car.registrationplate == id);
                return Json(removed);
            }
        }

        #endregion

        #region CarType

        [HttpPost]
        public JsonResult GetCarType(string search)
        {
            using (CarTypeData db = new CarTypeData())
            {
                var carTypes = db.GetFromSearch(search, car => new
                {
                    id = car.id,
                    model = car.model,
                    manufacturer = car.manufacturer,
                    year = car.year,
                    ismanual = car.ismanual,
                    dailycost = car.dailycost,
                    latefee = car.latefee,
                    picture = car.picture
                });
                return Json(carTypes);
            }
        }

        [HttpPost]
        public JsonResult AddCarType(CarTypeValidation cartype, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                //if supply image file save it to the server and save the path to the database
                cartype.Picture = Utilitys.GetRandomFileName("jpg");
                string _path = Path.Combine(Server.MapPath("~/Images"), cartype.Picture);
                image.SaveAs(_path);
            }
            using (CarTypeData db = new CarTypeData())
            {
                bool add = db.Add(cartype);
                return Json(add);
            }
        }

        [HttpPost]
        public JsonResult UpdateCarType(CarTypeValidation cartype,HttpPostedFileBase image,int id)
        {
            //if supply image save to server
            if (image != null && image.ContentLength > 0)
            {

                if (string.IsNullOrEmpty(cartype.Picture))
                //if the car didnt had image before create new file name
                {
                    cartype.Picture = Utilitys.GetRandomFileName("jpg");
                }
                string _path = Path.Combine(Server.MapPath("~/Images"), cartype.Picture);
                image.SaveAs(_path);
            }
                using (CarTypeData db = new CarTypeData())
            {
                bool update = db.Update(_cartype => _cartype.id == id, cartype);
                return Json(update);
            }
        }

        [HttpPost]
        public JsonResult DeleteCarType(int id)
        {
            using (CarTypeData db = new CarTypeData())
            {
                bool remove = db.Delete(_cartype => _cartype.id == id);
                return Json(remove);
            }
        }

        #endregion

        #region User

        [HttpPost]
        public JsonResult GetUser(string search)
        {
            using(UsersData db = new UsersData())
            {
                var users = db.GetFromSearch(search,user => new
                {
                    id=user.id,
                    fullname = user.fullname,
                    username = user.username,
                    email = user.email,
                    idcard = user.idcard,
                    birthdate = user.birthdate,
                    sex = user.sex,
                    permission = user.permission
                });
                return Json(users);
            }
        }

        [HttpPost]
        public JsonResult AddUser(UserRegisterValidation user)
        {
            if (ModelState.IsValid)
            {
                using (UsersData db = new UsersData())
                {
                    return Json(db.Register(user));
                }
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult UpdateUser(UserUpdateValidation user, string id, byte permission)
        {
            if (ModelState.IsValid)
            {
                using (UsersData db = new UsersData())
                {
                    User updatedUser = user;
                    updatedUser.permission = permission;
                    bool update = db.Update(_user => _user.id == id, updatedUser);
                    return Json(update);
                }
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult DeleteUser(string id)
        {
            using (UsersData db = new UsersData())
            {
                bool remove = db.Delete(user => user.id == id);
                return Json(remove);
            }
        }

        #endregion

        #region Order

        [HttpPost]
        public JsonResult GetOrder(string search)
        {
            using (OrdersData db = new OrdersData())
            {
                var orders = db.GetFromSearch(search, order => new
                {
                    id = order.Id,
                    carid= order.carid,
                    userid = order.userid,
                    startdate = order.startdate,
                    enddate= order.enddate,
                    returndate= order.returndate
                });
                return Json(orders);
            }
        }

        [HttpPost]
        public JsonResult AddOrder(OrderValidation order)
        {
            if (ModelState.IsValid)
            {
                using (OrdersData db = new OrdersData())
                {
                    return Json(db.Add(order));
                }
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult UpdateOrder(OrderValidation order,int id)
        {
            if (ModelState.IsValid)
            {
                using (OrdersData db = new OrdersData())
                {
                    return Json(db.Update(_order => _order.Id == id, order));
                }
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult DeleteOrder(int id)
        {
            using (OrdersData db = new OrdersData())
            {
                return Json(db.Delete(_order => _order.Id == id));
            }
        }

        #endregion

        #region Branch

        [HttpPost]
        public JsonResult GetBranch(string search)
        {
            using (BranchesData db = new BranchesData())
            {
                var branches = db.GetFromSearch(search, branch => new
                {
                    id = branch.Id,
                    latitude = branch.latitude,
                    longitude = branch.longitude,
                    name = branch.name,
                });
                return Json(branches);
            }
        }

        [HttpPost]
        public JsonResult AddBranch(BranchValidation branch)
        {
            if (ModelState.IsValid)
            {
                using (BranchesData db = new BranchesData())
                {
                    return Json(db.Add(branch));
                }
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult UpdateBranch(BranchValidation branch)
        {
            if (ModelState.IsValid)
            {
                using (BranchesData db = new BranchesData())
                {
                    return Json(db.Update(_branch => _branch.Id == branch.ID, branch));
                }
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult DeleteBranch(int id)
        {
            using (BranchesData db = new BranchesData())
            {
                return Json(db.Delete(_branch => _branch.Id == id));
            }
        }

        #endregion
    }
}