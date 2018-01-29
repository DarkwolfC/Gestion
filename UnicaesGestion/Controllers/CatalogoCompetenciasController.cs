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
    public class CatalogoCompetenciasController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: CatalogoCompetencias
        public ActionResult Index()
        {
            var catalogoCompetencias = db.CatalogoCompetencias.Include(c => c.Categoria).Include(c => c.TipoPuesto);
            return View(catalogoCompetencias.ToList());
        }

        // GET: CatalogoCompetencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogoCompetencia catalogoCompetencia = db.CatalogoCompetencias.Find(id);
            if (catalogoCompetencia == null)
            {
                return HttpNotFound();
            }
            return View(catalogoCompetencia);
        }

        // GET: CatalogoCompetencias/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "id", "categoria1");
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "Id", "tipo");
            return View();
        }

        // POST: CatalogoCompetencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,competencia,idTipoPuesto,idCategoria")] CatalogoCompetencia catalogoCompetencia)
        {
            if (ModelState.IsValid)
            {
                db.CatalogoCompetencias.Add(catalogoCompetencia);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.idCategoria = new SelectList(db.Categorias, "id", "categoria1", catalogoCompetencia.idCategoria);
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "Id", "tipo", catalogoCompetencia.idTipoPuesto);
            return View(catalogoCompetencia);
        }

        // GET: CatalogoCompetencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogoCompetencia catalogoCompetencia = db.CatalogoCompetencias.Find(id);
            if (catalogoCompetencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "id", "categoria1", catalogoCompetencia.idCategoria);
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "Id", "tipo", catalogoCompetencia.idTipoPuesto);
            return View(catalogoCompetencia);
        }

        // POST: CatalogoCompetencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,competencia,idTipoPuesto,idCategoria")] CatalogoCompetencia catalogoCompetencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalogoCompetencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "id", "categoria1", catalogoCompetencia.idCategoria);
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "Id", "tipo", catalogoCompetencia.idTipoPuesto);
            return View(catalogoCompetencia);
        }

        // GET: CatalogoCompetencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatalogoCompetencia catalogoCompetencia = db.CatalogoCompetencias.Find(id);
            if (catalogoCompetencia == null)
            {
                return HttpNotFound();
            }
            return View(catalogoCompetencia);
        }

        // POST: CatalogoCompetencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CatalogoCompetencia catalogoCompetencia = db.CatalogoCompetencias.Find(id);
            db.CatalogoCompetencias.Remove(catalogoCompetencia);
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
