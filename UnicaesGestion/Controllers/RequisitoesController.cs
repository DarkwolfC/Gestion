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
    public class RequisitoesController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: Requisitoes
        public ActionResult Index()
        {
            var requisitoes = db.Requisitoes.Include(r => r.Criterio).Include(r => r.PerfilContratacion).Include(r => r.PrioridadRequisito).Include(r => r.TipoRequisito);
            return View(requisitoes.ToList());
        }

        // GET: Requisitoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisito requisito = db.Requisitoes.Find(id);
            if (requisito == null)
            {
                return HttpNotFound();
            }
            return View(requisito);
        }

        // GET: Requisitoes/Create
        public ActionResult Create()
        {
            ViewBag.idcriterios = new SelectList(db.Criterios, "id", "criterio1");
            ViewBag.idPerfilContratacion = new SelectList(db.PerfilContratacions, "id", "analista");
            ViewBag.idPrioridadRequisito = new SelectList(db.PrioridadRequisitoes, "id", "prioridad");
            ViewBag.idTipoRequisito = new SelectList(db.TipoRequisitoes, "id", "tipo");
            return View();
        }

        // POST: Requisitoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,denominacion,idTipoRequisito,idcriterios,idPrioridadRequisito,idPerfilContratacion")] Requisito requisito)
        {
            if (ModelState.IsValid)
            {
                db.Requisitoes.Add(requisito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idcriterios = new SelectList(db.Criterios, "id", "criterio1", requisito.idcriterios);
            ViewBag.idPerfilContratacion = new SelectList(db.PerfilContratacions, "id", "analista", requisito.idPerfilContratacion);
            ViewBag.idPrioridadRequisito = new SelectList(db.PrioridadRequisitoes, "id", "prioridad", requisito.idPrioridadRequisito);
            ViewBag.idTipoRequisito = new SelectList(db.TipoRequisitoes, "id", "tipo", requisito.idTipoRequisito);
            return View(requisito);
        }

        // GET: Requisitoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisito requisito = db.Requisitoes.Find(id);
            if (requisito == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcriterios = new SelectList(db.Criterios, "id", "criterio1", requisito.idcriterios);
            ViewBag.idPerfilContratacion = new SelectList(db.PerfilContratacions, "id", "analista", requisito.idPerfilContratacion);
            ViewBag.idPrioridadRequisito = new SelectList(db.PrioridadRequisitoes, "id", "prioridad", requisito.idPrioridadRequisito);
            ViewBag.idTipoRequisito = new SelectList(db.TipoRequisitoes, "id", "tipo", requisito.idTipoRequisito);
            return View(requisito);
        }

        // POST: Requisitoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,denominacion,idTipoRequisito,idcriterios,idPrioridadRequisito,idPerfilContratacion")] Requisito requisito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcriterios = new SelectList(db.Criterios, "id", "criterio1", requisito.idcriterios);
            ViewBag.idPerfilContratacion = new SelectList(db.PerfilContratacions, "id", "analista", requisito.idPerfilContratacion);
            ViewBag.idPrioridadRequisito = new SelectList(db.PrioridadRequisitoes, "id", "prioridad", requisito.idPrioridadRequisito);
            ViewBag.idTipoRequisito = new SelectList(db.TipoRequisitoes, "id", "tipo", requisito.idTipoRequisito);
            return View(requisito);
        }

        // GET: Requisitoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisito requisito = db.Requisitoes.Find(id);
            if (requisito == null)
            {
                return HttpNotFound();
            }
            return View(requisito);
        }

        // POST: Requisitoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Requisito requisito = db.Requisitoes.Find(id);
            db.Requisitoes.Remove(requisito);
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
