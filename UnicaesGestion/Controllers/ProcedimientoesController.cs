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
    public class ProcedimientoesController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: Procedimientoes
        public ActionResult Index()
        {
            return View(db.Procedimientoes.ToList());
        }

        // GET: Procedimientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedimiento procedimiento = db.Procedimientoes.Find(id);
            if (procedimiento == null)
            {
                return HttpNotFound();
            }
            return View(procedimiento);
        }

        // GET: Procedimientoes/Create
        public ActionResult Create()
        {
            ViewBag.idProcedimiento = new SelectList(db.Procedimientoes, "id", "nombre"); //agregado
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo");//agregado
            ViewBag.idTipoPaso = new SelectList(db.TipoPasoes, "id", "tipoPaso1");//agregado
            return View();
        }

        // POST: Procedimientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,objetivoInicial,objetvioFinal")] Procedimiento procedimiento)
        {
            if (ModelState.IsValid)
            {
                
                db.Procedimientoes.Add(procedimiento);
                db.SaveChanges();
                Response.Cookies["llave"]["idProcedimiento"] = procedimiento.id.ToString();
                Response.Cookies["llave"].Expires = DateTime.Now.AddMinutes(45);
                return RedirectToAction("Create","Pasoes", new { idPuestoTrabajo = procedimiento.id });
            }

            return View(procedimiento);
        }

        // GET: Procedimientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedimiento procedimiento = db.Procedimientoes.Find(id);
            if (procedimiento == null)
            {
                return HttpNotFound();
            }
            return View(procedimiento);
        }

        // POST: Procedimientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,objetivoInicial,objetvioFinal")] Procedimiento procedimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(procedimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procedimiento);
        }

        // GET: Procedimientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedimiento procedimiento = db.Procedimientoes.Find(id);
            if (procedimiento == null)
            {
                return HttpNotFound();
            }
            return View(procedimiento);
        }

        // POST: Procedimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Procedimiento procedimiento = db.Procedimientoes.Find(id);
            db.Procedimientoes.Remove(procedimiento);
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
