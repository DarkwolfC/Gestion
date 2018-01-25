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
    public class CompetenciaPuestoController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: CompetenciaPuesto
        public ActionResult Index()
        {
            var competenciaPuestoTrabajoes = db.CompetenciaPuestoTrabajoes.Include(c => c.CatalogoCompetencia).Include(c => c.PuestoTrabajo);
            return View(competenciaPuestoTrabajoes.ToList());
        }

        // GET: CompetenciaPuesto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompetenciaPuestoTrabajo competenciaPuestoTrabajo = db.CompetenciaPuestoTrabajoes.Find(id);
            if (competenciaPuestoTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(competenciaPuestoTrabajo);
        }

        // GET: CompetenciaPuesto/Create
        public ActionResult Create()
        {
            ViewBag.idCompetencia = new SelectList(db.CatalogoCompetencias, "Id", "competencia");
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo");
            return View();
        }

        // POST: CompetenciaPuesto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,idCompetencia,idPuestoTrabajo,escencial")] CompetenciaPuestoTrabajo competenciaPuestoTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.CompetenciaPuestoTrabajoes.Add(competenciaPuestoTrabajo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCompetencia = new SelectList(db.CatalogoCompetencias, "Id", "competencia", competenciaPuestoTrabajo.idCompetencia);
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", competenciaPuestoTrabajo.idPuestoTrabajo);
            return View(competenciaPuestoTrabajo);
        }

        // GET: CompetenciaPuesto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompetenciaPuestoTrabajo competenciaPuestoTrabajo = db.CompetenciaPuestoTrabajoes.Find(id);
            if (competenciaPuestoTrabajo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCompetencia = new SelectList(db.CatalogoCompetencias, "Id", "competencia", competenciaPuestoTrabajo.idCompetencia);
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", competenciaPuestoTrabajo.idPuestoTrabajo);
            return View(competenciaPuestoTrabajo);
        }

        // POST: CompetenciaPuesto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,idCompetencia,idPuestoTrabajo,escencial")] CompetenciaPuestoTrabajo competenciaPuestoTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competenciaPuestoTrabajo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCompetencia = new SelectList(db.CatalogoCompetencias, "Id", "competencia", competenciaPuestoTrabajo.idCompetencia);
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", competenciaPuestoTrabajo.idPuestoTrabajo);
            return View(competenciaPuestoTrabajo);
        }

        // GET: CompetenciaPuesto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompetenciaPuestoTrabajo competenciaPuestoTrabajo = db.CompetenciaPuestoTrabajoes.Find(id);
            if (competenciaPuestoTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(competenciaPuestoTrabajo);
        }

        // POST: CompetenciaPuesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompetenciaPuestoTrabajo competenciaPuestoTrabajo = db.CompetenciaPuestoTrabajoes.Find(id);
            db.CompetenciaPuestoTrabajoes.Remove(competenciaPuestoTrabajo);
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
