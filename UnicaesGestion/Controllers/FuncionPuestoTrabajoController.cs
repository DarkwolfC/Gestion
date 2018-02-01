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
    public class FuncionPuestoTrabajoController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: FuncionPuestoTrabajo
        public ActionResult Index()
        {
            if (Request.Cookies["llave"]!=null)
            {
                int llave;
                if (Request.Cookies["llave"]["idPuestoTrabajo"] != null)
                { llave = int.Parse(Request.Cookies["llave"]["idPuestoTrabajo"].ToString());
                    var funcionPuestoTrabajoes = db.FuncionPuestoTrabajoes.Where(f => f.PuestoTrabajo.id == llave);
                    return View(funcionPuestoTrabajoes.ToList());
                }
            }

            return View();

        }

        // GET: FuncionPuestoTrabajo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionPuestoTrabajo funcionPuestoTrabajo = db.FuncionPuestoTrabajoes.Find(id);
            if (funcionPuestoTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(funcionPuestoTrabajo);
        }

        // GET: FuncionPuestoTrabajo/Create
        public ActionResult Create()
        {

            if (Request.Cookies["llave"] != null)
            {
                int llave;
                if (Request.Cookies["llave"]["idPuestoTrabajo"] != null)
                {
                    llave = int.Parse(Request.Cookies["llave"]["idPuestoTrabajo"].ToString());
                    ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes.Where(x => x.id == llave), "id", "titulo");
                   
                }
                else
                {
                    ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo");
                }
            }
            
            return View();
        }

        // POST: FuncionPuestoTrabajo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,funcion,idPuestoTrabajo")] FuncionPuestoTrabajo funcionPuestoTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.FuncionPuestoTrabajoes.Add(funcionPuestoTrabajo);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", funcionPuestoTrabajo.idPuestoTrabajo);
            return View(funcionPuestoTrabajo);
        }

        // GET: FuncionPuestoTrabajo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionPuestoTrabajo funcionPuestoTrabajo = db.FuncionPuestoTrabajoes.Find(id);
            if (funcionPuestoTrabajo == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", funcionPuestoTrabajo.idPuestoTrabajo);
            return View(funcionPuestoTrabajo);
        }

        // POST: FuncionPuestoTrabajo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,funcion,idPuestoTrabajo")] FuncionPuestoTrabajo funcionPuestoTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionPuestoTrabajo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", funcionPuestoTrabajo.idPuestoTrabajo);
            return View(funcionPuestoTrabajo);
        }

        // GET: FuncionPuestoTrabajo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionPuestoTrabajo funcionPuestoTrabajo = db.FuncionPuestoTrabajoes.Find(id);
            if (funcionPuestoTrabajo == null)
            {
                return HttpNotFound();
            }
            return View(funcionPuestoTrabajo);
        }

        // POST: FuncionPuestoTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FuncionPuestoTrabajo funcionPuestoTrabajo = db.FuncionPuestoTrabajoes.Find(id);
            db.FuncionPuestoTrabajoes.Remove(funcionPuestoTrabajo);
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
