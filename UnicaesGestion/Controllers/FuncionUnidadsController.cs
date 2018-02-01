using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnicaesGestion;

namespace UnicaesGestion.Controllers
{
    public class FuncionUnidadsController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: FuncionUnidads
        public ActionResult Index()
        {
            if (Request.Cookies["llave"] != null)
            {
                int llave;
                if (Request.Cookies["llave"]["idUnidad"] != null)
                {
                    llave = int.Parse(Request.Cookies["llave"]["idUnidad"].ToString());
                    var funcionUnidads = db.FuncionUnidads.Where(f => f.Unidad.id == llave);
                    return View(funcionUnidads.ToList());
                }
            }
            return View();
            //var funcionUnidads = db.FuncionUnidads.Include(f => f.Unidad);
            
        }

        // GET: FuncionUnidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionUnidad funcionUnidad = db.FuncionUnidads.Find(id);
            if (funcionUnidad == null)
            {
                return HttpNotFound();
            }
            return View(funcionUnidad);
        }

        // GET: FuncionUnidads/Create
        public ActionResult Create()
        {
            if (Request.Cookies["llave"] != null)
            {
                int llave;
                if (Request.Cookies["llave"]["idUnidad"] != null)
                {
                    llave = int.Parse(Request.Cookies["llave"]["idUnidad"].ToString());
                    ViewBag.idUnidad = new SelectList(db.Unidads.Where(x => x.id == llave), "id", "nombre");

                }
                else
                {
                    ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre");
                }
            }
            
            //ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre");
            return View();
        }

        // POST: FuncionUnidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,descripcion,idUnidad")] FuncionUnidad funcionUnidad)
        {
            if (ModelState.IsValid)
            {
                db.FuncionUnidads.Add(funcionUnidad);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre", funcionUnidad.idUnidad);
            return View(funcionUnidad);
        }

        // GET: FuncionUnidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionUnidad funcionUnidad = db.FuncionUnidads.Find(id);
            if (funcionUnidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre", funcionUnidad.idUnidad);
            return View(funcionUnidad);
        }

        // POST: FuncionUnidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,descripcion,idUnidad")] FuncionUnidad funcionUnidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionUnidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre", funcionUnidad.idUnidad);
            return View(funcionUnidad);
        }

        // GET: FuncionUnidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionUnidad funcionUnidad = db.FuncionUnidads.Find(id);
            if (funcionUnidad == null)
            {
                return HttpNotFound();
            }
            return View(funcionUnidad);
        }

        // POST: FuncionUnidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FuncionUnidad funcionUnidad = db.FuncionUnidads.Find(id);
            db.FuncionUnidads.Remove(funcionUnidad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
