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
    public class PerfilTrabajoController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: PerfilTrabajo
        public ActionResult Index()
        {
            var puestoTrabajoes = db.PuestoTrabajoes.Include(p => p.PuestoTrabajo2).Include(p => p.TipoPuesto).Include(p => p.Unidad);
            return View(puestoTrabajoes.ToList());
        }

        // GET: PerfilTrabajo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuestoTrabajo puestoTrabajo = db.PuestoTrabajoes.Find(id);
            if (puestoTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(puestoTrabajo);
        }

        // GET: PerfilTrabajo/Create
        public ActionResult Create()
        {
            ViewBag.jefeInmediato = new SelectList(db.PuestoTrabajoes, "id", "titulo");
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "Id", "tipo");
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre");
            return View();
        }

        // POST: PerfilTrabajo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,titulo,objetivo,jefeInmediato,idUnidad,idTipoPuesto,fechaCreacion,activo,aprobado")] PuestoTrabajo puestoTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.PuestoTrabajoes.Add(puestoTrabajo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.jefeInmediato = new SelectList(db.PuestoTrabajoes, "id", "titulo", puestoTrabajo.jefeInmediato);
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "Id", "tipo", puestoTrabajo.idTipoPuesto);
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre", puestoTrabajo.idUnidad);
            return View(puestoTrabajo);
        }

        // GET: PerfilTrabajo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuestoTrabajo puestoTrabajo = db.PuestoTrabajoes.Find(id);
            if (puestoTrabajo == null)
            {
                return HttpNotFound();
            }
            ViewBag.jefeInmediato = new SelectList(db.PuestoTrabajoes, "id", "titulo", puestoTrabajo.jefeInmediato);
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "Id", "tipo", puestoTrabajo.idTipoPuesto);
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre", puestoTrabajo.idUnidad);
            return View(puestoTrabajo);
        }

        // POST: PerfilTrabajo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,titulo,objetivo,jefeInmediato,idUnidad,idTipoPuesto,fechaCreacion,activo,aprobado")] PuestoTrabajo puestoTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(puestoTrabajo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.jefeInmediato = new SelectList(db.PuestoTrabajoes, "id", "titulo", puestoTrabajo.jefeInmediato);
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "Id", "tipo", puestoTrabajo.idTipoPuesto);
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre", puestoTrabajo.idUnidad);
            return View(puestoTrabajo);
        }

        // GET: PerfilTrabajo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuestoTrabajo puestoTrabajo = db.PuestoTrabajoes.Find(id);
            if (puestoTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(puestoTrabajo);
        }

        // POST: PerfilTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PuestoTrabajo puestoTrabajo = db.PuestoTrabajoes.Find(id);
            db.PuestoTrabajoes.Remove(puestoTrabajo);
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
