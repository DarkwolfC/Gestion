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
    public class PasoesController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: Pasoes
        public ActionResult Index()
        {
            if (Request.Cookies["llave"] != null)
            {
                int llave;
                if (Request.Cookies["llave"]["idProcedimiento"] != null)
                {
                    llave = int.Parse(Request.Cookies["llave"]["idProcedimiento"].ToString());                   
                    var pasoes = db.Pasoes.Where(p => p.Procedimiento.id==llave).Include(p => p.PuestoTrabajo).Include(p => p.TipoPaso);
                    return View(pasoes.ToList());
                }
            }
            //var pasoes = db.Pasoes.Include(p => p.Procedimiento).Include(p => p.PuestoTrabajo).Include(p => p.TipoPaso);
            return View();
        }

        // GET: Pasoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paso paso = db.Pasoes.Find(id);
            if (paso == null)
            {
                return HttpNotFound();
            }
            return View(paso);
        }

        // GET: Pasoes/Create
        public ActionResult Create()
        {
            if (Request.Cookies["llave"] != null)
            {
                int llave;
                if (Request.Cookies["llave"]["idProcedimiento"] != null)
                {
                    llave = int.Parse(Request.Cookies["llave"]["idProcedimiento"].ToString());
                    ViewBag.idProcedimiento = new SelectList(db.Procedimientoes.Where(x => x.id == llave), "id", "nombre");
                    ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo");
                    ViewBag.idTipoPaso = new SelectList(db.TipoPasoes, "id", "tipoPaso1");
                }
                else
                {
                    ViewBag.idProcedimiento = new SelectList(db.Procedimientoes, "id", "nombre");
                    ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo");
                    ViewBag.idTipoPaso = new SelectList(db.TipoPasoes, "id", "tipoPaso1");
                }
            }
            
            return View();
        }

        // POST: Pasoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,numero,descripcion,predecesores,idProcedimiento,idTipoPaso,idPuestoTrabajo")] Paso paso)
        {
            if (ModelState.IsValid)
            {
                db.Pasoes.Add(paso);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.idProcedimiento = new SelectList(db.Procedimientoes, "id", "nombre", paso.idProcedimiento);
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", paso.idPuestoTrabajo);
            ViewBag.idTipoPaso = new SelectList(db.TipoPasoes, "id", "tipoPaso1", paso.idTipoPaso);
            return View(paso);
        }

        // GET: Pasoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paso paso = db.Pasoes.Find(id);
            if (paso == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProcedimiento = new SelectList(db.Procedimientoes, "id", "nombre", paso.idProcedimiento);
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", paso.idPuestoTrabajo);
            ViewBag.idTipoPaso = new SelectList(db.TipoPasoes, "id", "tipoPaso1", paso.idTipoPaso);
            return View(paso);
        }

        // POST: Pasoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,numero,descripcion,predecesores,idProcedimiento,idTipoPaso,idPuestoTrabajo")] Paso paso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProcedimiento = new SelectList(db.Procedimientoes, "id", "nombre", paso.idProcedimiento);
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", paso.idPuestoTrabajo);
            ViewBag.idTipoPaso = new SelectList(db.TipoPasoes, "id", "tipoPaso1", paso.idTipoPaso);
            return View(paso);
        }

        // GET: Pasoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paso paso = db.Pasoes.Find(id);
            if (paso == null)
            {
                return HttpNotFound();
            }
            return View(paso);
        }

        // POST: Pasoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paso paso = db.Pasoes.Find(id);
            db.Pasoes.Remove(paso);
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
