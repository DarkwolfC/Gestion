using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnicaesGestion;
using UnicaesGestion.Models;

namespace UnicaesGestion.Controllers
{
    public class UnidadsController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: Unidads
        public ActionResult Index()
        {
            //var unidads = db.Unidads.Include(u => u.PuestoTrabajo).Include(u => u.Unidad2);
            return View("_PartialListDependences",  db.Unidads);
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
        public ActionResult AddUnity()
        {
            ViewBag.idPuestoResponsableTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo");
            ViewBag.depende = new SelectList(db.Unidads, "id", "nombre");
            return View();
        }

        // POST: Unidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUnity([Bind(Include = "id,nombre,objetivo,depende,idPuestoResponsableTrabajo,unidadFunciones")] FuncionUnidadViewModel funcionUnidad)
        {
            if (ModelState.IsValid)
            {
                Unidad unidad = new Unidad()
                {
                    id = funcionUnidad.id,
                    nombre = funcionUnidad.nombre,
                    objetivo = funcionUnidad.objetivo,
                    depende=funcionUnidad.depende,
                    idPuestoResponsableTrabajo=funcionUnidad.idPuestoResponsableTrabajo                   
                };
                
                db.Unidads.Add(unidad);
                db.SaveChanges();

                String[] cadenas = funcionUnidad.unidadFunciones.Split('.');
                foreach (var cadena in cadenas)
                {
                    FuncionUnidad descripcion = new FuncionUnidad
                    {
                        idUnidad = unidad.id,
                        descripcion= cadena
                    };
                    db.FuncionUnidads.Add(descripcion);
                    //db.Funcion.Add(descripcion);
                }
                db.SaveChanges();
                return RedirectToAction("ReadUnity");
            }

            //ViewBag.idPuestoResponsableTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", unidad.idPuestoResponsableTrabajo);
            //ViewBag.depende = new SelectList(db.Unidads, "id", "nombre", unidad.depende);
            return View(funcionUnidad);
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
            ViewBag.idPuestoResponsableTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", unidad.idPuestoResponsableTrabajo);
            ViewBag.depende = new SelectList(db.Unidads, "id", "nombre", unidad.depende);
            return View(unidad);
        }

        // POST: Unidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,objetivo,depende,idPuestoResponsableTrabajo")] Unidad unidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPuestoResponsableTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", unidad.idPuestoResponsableTrabajo);
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
