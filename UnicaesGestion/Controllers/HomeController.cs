using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnicaesGestion.Models;

namespace UnicaesGestion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            LoginModel modelo = new LoginModel();
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Auth(Models.LoginModel modelo)
        {

            return View();
        }

        public ActionResult LockScreen()
        {
            return View();
        }

        public ActionResult NotFound()
        {

            return View();
        }


    }
}