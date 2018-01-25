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
    public class RequisitosController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: Requisitos
        public ActionResult Index()
        {
            var requisitoes = db.Requisitoes.Include(r => r.Categoria).Include(r => r.PuestoTrabajo);
            return View(requisitoes.ToList());
        }

        // GET: Requisitos/Details/5
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

        // GET: Requisitos/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "id", "categoria1");
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo");
            return View();
        }

        // POST: Requisitos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,descripcion,idPuestoTrabajo,idCategoria")] Requisito requisito)
        {
            if (ModelState.IsValid)
            {
                db.Requisitoes.Add(requisito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCategoria = new SelectList(db.Categorias, "id", "categoria1", requisito.idCategoria);
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", requisito.idPuestoTrabajo);
            return View(requisito);
        }

        // GET: Requisitos/Edit/5
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
            ViewBag.idCategoria = new SelectList(db.Categorias, "id", "categoria1", requisito.idCategoria);
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", requisito.idPuestoTrabajo);
            return View(requisito);
        }

        // POST: Requisitos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,descripcion,idPuestoTrabajo,idCategoria")] Requisito requisito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "id", "categoria1", requisito.idCategoria);
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", requisito.idPuestoTrabajo);
            return View(requisito);
        }

        // GET: Requisitos/Delete/5
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

        // POST: Requisitos/Delete/5
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
