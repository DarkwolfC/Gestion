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
    public class UnidadsController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: Unidads
        public ActionResult Index()
        {
            var unidads = db.Unidads.Include(u => u.Unidad2);
            return View(unidads.ToList());
        }

        // GET: Unidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidad unidad = db.Unidads.Find(id);
            if (unidad == null)
            {
                return HttpNotFound();
            }
            return View(unidad);
        }

        // GET: Unidads/Create
        public ActionResult Create()
        {
            ViewBag.idPuestoResponsableTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo");//agregado
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre");//agregado
            ViewBag.depende = new SelectList(db.Unidads, "id", "nombre");
            return View();
        }

        // POST: Unidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,objetivo,depende,idPuestoResponsableTrabajo")] Unidad unidad)
        {
            if (ModelState.IsValid)
            {
                db.Unidads.Add(unidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.depende = new SelectList(db.Unidads, "id", "nombre", unidad.depende);
            return View(unidad);
        }

        // GET: Unidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidad unidad = db.Unidads.Find(id);
            if (unidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.depende = new SelectList(db.Unidads, "id", "nombre", unidad.depende);
            return View(unidad);
        }

        // POST: Unidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,objetivo,depende,idPuestoResponsableTrabajo")] Unidad unidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.depende = new SelectList(db.Unidads, "id", "nombre", unidad.depende);
            return View(unidad);
        }

        // GET: Unidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidad unidad = db.Unidads.Find(id);
            if (unidad == null)
            {
                return HttpNotFound();
            }
            return View(unidad);
        }

        // POST: Unidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Unidad unidad = db.Unidads.Find(id);
            db.Unidads.Remove(unidad);
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
