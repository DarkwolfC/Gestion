using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnicaesGestion.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Perfil()
        {
            return View();
        }
    }
}