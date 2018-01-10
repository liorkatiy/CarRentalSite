using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YoRent.Controllers
{
    //templates for the angular
    public class TemplateController : Controller
    {
        public ViewResult Car()
        {
            return View();
        }

        public ViewResult AddCar()
        {
            return View();
        }

        public ViewResult AddCarType()
        {
            return View();
        }

        public ViewResult AddUser()
        {
            return View();
        }

        public ViewResult AddOrder()
        {
            return View();
        }

        public ViewResult AddBranch()
        {
            return View();
        }
    }
}