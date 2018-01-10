using System;
using System.Web.Mvc;
using Entity;
using YoRent.Models;
using MvcUtilitys;

namespace YoRent.Controllers
{
    public class AccountController : Controller
    {
        //registration view
        [AuthorizeUser(Premission = Premission.User, Inverse = true)]
        public ActionResult Register()
        {
            return View(new UserRegisterValidation());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterValidation user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (UsersData db = new UsersData())
                {
                    db.Register(user);
                    return RedirectToAction("Login");
                }
            }

            return View(user);
        }

        //login view
        [AuthorizeUser(Premission = Premission.User, Inverse = true)]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginValidation user, string returnUrl)
        {
            UserInfo info;
            if (ModelState.IsValid)
            {
                using (UsersData db = new UsersData())
                {
                    //check if the user name and password match
                    if (db.VerifyUser(user.Password, user.Name, out info))
                    {
                        Session.SetUserData(info);
                        return Redirect(string.IsNullOrEmpty(returnUrl) ? "~" : returnUrl);
                    }
                }
            }
            user.Error = "Invalid User Name Or Password";
            return View(user);
        }

        //return view with the current user orders
        [AuthorizeUser(Premission = Premission.User)]
        public ActionResult Orders()
        {
            return View();
        }

        //return buycar view if the order details are valid
        [AuthorizeUser(Premission = Premission.User)]
        public ActionResult BuyCar(string regPlate, DateTime start, DateTime end)
        {
            bool isOrderSet = Session.SetOrder(regPlate, start, end);
            if (isOrderSet)
                return View();
            Session["car not available"] = true;
            return RedirectToAction("cars","home");
        }

        //log the user out
        [AuthorizeUser(Premission = Premission.User)]
        public ActionResult LogOut()
        {
            Session.LogOutUser();
            return RedirectToAction("index", "home");
        }
    }
}