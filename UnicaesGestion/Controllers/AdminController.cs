using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnicaesGestion.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //start views of job
        public ActionResult ReadJob()
        {
            return View();
        }
        
        public ActionResult AddJob()
        {
            return View();
        }

        public ActionResult EditJob()
        {
            return View();
        }

        //end views of job

        //start views of JobProfile
        public ActionResult ReadJobProfile()
        {
            return View();
        }

        public ActionResult AddJobProfile()
        {
            return View();
        }

        public ActionResult EditJobProfile()
        {
            return View();
        }

        //end views of JobProfile

        

        //start views of procedure
        public ActionResult ReadProcedure()
        {
            return View();
        }

        public ActionResult AddProcedure()
        {
            return View();
        }

        public ActionResult EditProcedure()
        {
            return View();
        }
        //end views of procedure

        //start views of unity
        public ActionResult ReadUnity()
        {
            return View();
        }

        public ActionResult AddUnity()
        {
            return View();
        }

        public ActionResult EditUnity()
        {
            return View();
        }

        //end views of unity


        // Start views of User
        public ActionResult ReadUser()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }

        public ActionResult EditUser()
        {
            return View();
        }
        //end views of user
    }
}