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
    public class TipoPasoesController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: TipoPasoes
        public ActionResult Index()
        {
            return View(db.TipoPasoes.ToList());
        }

        // GET: TipoPasoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPaso tipoPaso = db.TipoPasoes.Find(id);
            if (tipoPaso == null)
            {
                return HttpNotFound();
            }
            return View(tipoPaso);
        }

        // GET: TipoPasoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPasoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tipoPaso1")] TipoPaso tipoPaso)
        {
            if (ModelState.IsValid)
            {
                db.TipoPasoes.Add(tipoPaso);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(tipoPaso);
        }

        // GET: TipoPasoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPaso tipoPaso = db.TipoPasoes.Find(id);
            if (tipoPaso == null)
            {
                return HttpNotFound();
            }
            return View(tipoPaso);
        }

        // POST: TipoPasoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tipoPaso1")] TipoPaso tipoPaso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPaso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            return View(tipoPaso);
        }

        // GET: TipoPasoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPaso tipoPaso = db.TipoPasoes.Find(id);
            if (tipoPaso == null)
            {
                return HttpNotFound();
            }
            return View(tipoPaso);
        }

        // POST: TipoPasoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPaso tipoPaso = db.TipoPasoes.Find(id);
            db.TipoPasoes.Remove(tipoPaso);
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
