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
    public class PrioridadRequisitoesController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: PrioridadRequisitoes
        public ActionResult Index()
        {
            return View(db.PrioridadRequisitoes.ToList());
        }

        // GET: PrioridadRequisitoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrioridadRequisito prioridadRequisito = db.PrioridadRequisitoes.Find(id);
            if (prioridadRequisito == null)
            {
                return HttpNotFound();
            }
            return View(prioridadRequisito);
        }

        // GET: PrioridadRequisitoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrioridadRequisitoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,prioridad")] PrioridadRequisito prioridadRequisito)
        {
            if (ModelState.IsValid)
            {
                db.PrioridadRequisitoes.Add(prioridadRequisito);
                db.SaveChanges();
                return RedirectToAction("create");
            }

            return View(prioridadRequisito);
        }

        // GET: PrioridadRequisitoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrioridadRequisito prioridadRequisito = db.PrioridadRequisitoes.Find(id);
            if (prioridadRequisito == null)
            {
                return HttpNotFound();
            }
            return View(prioridadRequisito);
        }

        // POST: PrioridadRequisitoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,prioridad")] PrioridadRequisito prioridadRequisito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prioridadRequisito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            return View(prioridadRequisito);
        }

        // GET: PrioridadRequisitoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrioridadRequisito prioridadRequisito = db.PrioridadRequisitoes.Find(id);
            if (prioridadRequisito == null)
            {
                return HttpNotFound();
            }
            return View(prioridadRequisito);
        }

        // POST: PrioridadRequisitoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrioridadRequisito prioridadRequisito = db.PrioridadRequisitoes.Find(id);
            db.PrioridadRequisitoes.Remove(prioridadRequisito);
            db.SaveChanges();
            return RedirectToAction("Create");
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
