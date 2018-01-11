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
    public class TipoRequisitoesController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: TipoRequisitoes
        public ActionResult Index()
        {
            return View(db.TipoRequisitoes.ToList());
        }

        // GET: TipoRequisitoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRequisito tipoRequisito = db.TipoRequisitoes.Find(id);
            if (tipoRequisito == null)
            {
                return HttpNotFound();
            }
            return View(tipoRequisito);
        }

        // GET: TipoRequisitoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoRequisitoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tipo")] TipoRequisito tipoRequisito)
        {
            if (ModelState.IsValid)
            {
                db.TipoRequisitoes.Add(tipoRequisito);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(tipoRequisito);
        }

        // GET: TipoRequisitoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRequisito tipoRequisito = db.TipoRequisitoes.Find(id);
            if (tipoRequisito == null)
            {
                return HttpNotFound();
            }
            return View(tipoRequisito);
        }

        // POST: TipoRequisitoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tipo")] TipoRequisito tipoRequisito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoRequisito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            return View(tipoRequisito);
        }

        // GET: TipoRequisitoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRequisito tipoRequisito = db.TipoRequisitoes.Find(id);
            if (tipoRequisito == null)
            {
                return HttpNotFound();
            }
            return View(tipoRequisito);
        }

        // POST: TipoRequisitoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoRequisito tipoRequisito = db.TipoRequisitoes.Find(id);
            db.TipoRequisitoes.Remove(tipoRequisito);
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
