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
    public class PuestoTrabajoesController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: PuestoTrabajoes
        public ActionResult Index()
        {
            var puestoTrabajoes = db.PuestoTrabajoes.Include(p => p.PerfilContratacion).Include(p => p.PuestoTrabajo2).Include(p => p.TipoPuesto).Include(p => p.Unidad);
            return View(puestoTrabajoes.ToList());
        }

        // GET: PuestoTrabajoes/Details/5
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

        // GET: PuestoTrabajoes/Create
        public ActionResult AddJob()
        {
            ViewBag.idPerfilContratacion = new SelectList(db.PerfilContratacions, "id", "analista");
            ViewBag.jefeInmediato = new SelectList(db.PuestoTrabajoes, "id", "titulo");
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "id", "tipo");
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre");
            return View();
        }

        // POST: PuestoTrabajoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddJob([Bind(Include = "id,titulo,objetivo,jefeInmediato,idUnidad,idPerfilContratacion,idTipoPuesto,funcionPuesto")]FuncionTrabajoViewModel TrabajoFuncion )
        {
            if (ModelState.IsValid)
            {
                PuestoTrabajo puestotrabajo = new PuestoTrabajo
                {
                    id = TrabajoFuncion.id,
                    titulo = TrabajoFuncion.objetivo,
                    objetivo = TrabajoFuncion.objetivo,
                    jefeInmediato = TrabajoFuncion.jefeInmediato,
                    idPerfilContratacion = TrabajoFuncion.idPerfilContratacion,
                    idTipoPuesto = TrabajoFuncion.idTipoPuesto
                };
                    
                                    
                db.PuestoTrabajoes.Add(puestotrabajo);
                db.SaveChanges();

                String[] cadenas= TrabajoFuncion.funcionPuesto.Split('.');

                foreach (var cadena  in cadenas)
                {
                    FuncionPuestoTrabajo funcion = new FuncionPuestoTrabajo {
                       id= puestotrabajo.id, funcion=cadena                     
                    };
                    db.FuncionPuestoTrabajoes.Add(funcion);                 
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            //ViewBag.idPerfilContratacion = new SelectList(db.PerfilContratacions, "id", "analista", puestoTrabajo.idPerfilContratacion);
            //ViewBag.jefeInmediato = new SelectList(db.PuestoTrabajoes, "id", "titulo", puestoTrabajo.jefeInmediato);
            //ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "id", "tipo", puestoTrabajo.idTipoPuesto);
            //ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre", puestoTrabajo.idUnidad);

            return View(TrabajoFuncion);
        }

        // GET: PuestoTrabajoes/Edit/5
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
            ViewBag.idPerfilContratacion = new SelectList(db.PerfilContratacions, "id", "analista", puestoTrabajo.idPerfilContratacion);
            ViewBag.jefeInmediato = new SelectList(db.PuestoTrabajoes, "id", "titulo", puestoTrabajo.jefeInmediato);
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "id", "tipo", puestoTrabajo.idTipoPuesto);
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre", puestoTrabajo.idUnidad);
            return View(puestoTrabajo);
        }

        // POST: PuestoTrabajoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,titulo,objetivo,jefeInmediato,idUnidad,idPerfilContratacion,idTipoPuesto")] PuestoTrabajo puestoTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(puestoTrabajo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPerfilContratacion = new SelectList(db.PerfilContratacions, "id", "analista", puestoTrabajo.idPerfilContratacion);
            ViewBag.jefeInmediato = new SelectList(db.PuestoTrabajoes, "id", "titulo", puestoTrabajo.jefeInmediato);
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "id", "tipo", puestoTrabajo.idTipoPuesto);
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre", puestoTrabajo.idUnidad);
            return View(puestoTrabajo);
        }

        // GET: PuestoTrabajoes/Delete/5
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

        // POST: PuestoTrabajoes/Delete/5
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

        //mi codigo
        
        public ActionResult Prueba()
        {
            List<SelectListItem> listSelectItem = new List<SelectListItem>();
            foreach (var Unidades  in db.PuestoTrabajoes)
            {
                SelectListItem selectlistItem = new SelectListItem() {
                    Text = Unidades.titulo,
                    Value = Unidades.id.ToString(),
                   
                };
                listSelectItem.Add(selectlistItem);
            }
            return View();
        }


        //end



    }
}
