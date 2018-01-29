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
    public class TipoPuestoController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: TipoPuesto
        public ActionResult Index()
        {
            return View(db.TipoPuestoes.ToList());
        }

        // GET: TipoPuesto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPuesto tipoPuesto = db.TipoPuestoes.Find(id);
            if (tipoPuesto == null)
            {
                return HttpNotFound();
            }
            return View(tipoPuesto);
        }

        // GET: TipoPuesto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPuesto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,tipo")] TipoPuesto tipoPuesto)
        {
            if (ModelState.IsValid)
            {
                db.TipoPuestoes.Add(tipoPuesto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoPuesto);
        }

        // GET: TipoPuesto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPuesto tipoPuesto = db.TipoPuestoes.Find(id);
            if (tipoPuesto == null)
            {
                return HttpNotFound();
            }
            return View(tipoPuesto);
        }

        // POST: TipoPuesto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,tipo")] TipoPuesto tipoPuesto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPuesto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoPuesto);
        }

        // GET: TipoPuesto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPuesto tipoPuesto = db.TipoPuestoes.Find(id);
            if (tipoPuesto == null)
            {
                return HttpNotFound();
            }
            return View(tipoPuesto);
        }

        // POST: TipoPuesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPuesto tipoPuesto = db.TipoPuestoes.Find(id);
            db.TipoPuestoes.Remove(tipoPuesto);
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
