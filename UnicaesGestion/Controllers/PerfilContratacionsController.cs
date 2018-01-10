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
    public class PerfilContratacionsController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: PerfilContratacions
        public ActionResult Index()
        {
            return View(db.PerfilContratacions.ToList());
        }

        // GET: PerfilContratacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilContratacion perfilContratacion = db.PerfilContratacions.Find(id);
            if (perfilContratacion == null)
            {
                return HttpNotFound();
            }
            return View(perfilContratacion);
        }

        // GET: PerfilContratacions/Create
        public ActionResult Create()
        {
           return View();
        }

        // POST: PerfilContratacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fechaElaboracion,analista,aprobadoPor,fecha")] PerfilContratacion perfilContratacion)
        {
            if (ModelState.IsValid)
            {
                db.PerfilContratacions.Add(perfilContratacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(perfilContratacion);
        }
        


        // GET: PerfilContratacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilContratacion perfilContratacion = db.PerfilContratacions.Find(id);
            if (perfilContratacion == null)
            {
                return HttpNotFound();
            }
            return View(perfilContratacion);
        }

        // POST: PerfilContratacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fechaElaboracion,analista,aprobadoPor,fecha")] PerfilContratacion perfilContratacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perfilContratacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(perfilContratacion);
        }

        // GET: PerfilContratacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PerfilContratacion perfilContratacion = db.PerfilContratacions.Find(id);
            if (perfilContratacion == null)
            {
                return HttpNotFound();
            }
            return View(perfilContratacion);
        }

        // POST: PerfilContratacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PerfilContratacion perfilContratacion = db.PerfilContratacions.Find(id);
            db.PerfilContratacions.Remove(perfilContratacion);
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
