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
            ViewBag.idCompetencia = new SelectList(db.CatalogoCompetencias, "Id", "competencia");//agregado para las competencias
            ViewBag.idCategoria = new SelectList(db.Categorias, "id", "categoria1");//Agregado para los requisitos
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo");//Agregado para las funciones de trabajo
            ViewBag.jefeInmediato = new SelectList(db.PuestoTrabajoes, "id", "titulo");
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "Id", "tipo");
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre");
            return View();
        }

        // POST: PerfilTrabajo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,titulo,objetivo,jefeInmediato,idUnidad,idTipoPuesto,fechaCreacion,activo,aprobado")] PuestoTrabajo puestoTrabajo)
        {
            if (ModelState.IsValid)
            {
                db.PuestoTrabajoes.Add(puestoTrabajo);
                db.SaveChanges();
                Response.Cookies["llave"]["idPuestoTrabajo"]=puestoTrabajo.id.ToString();
                Response.Cookies["llave"].Expires = DateTime.Now.AddMinutes(45);
                return RedirectToAction("Create", "FuncionPuestoTrabajo", new { idPuestoTrabajo=puestoTrabajo.id});
            }

            ViewBag.jefeInmediato = new SelectList(db.PuestoTrabajoes, "id", "titulo", puestoTrabajo.jefeInmediato);
            ViewBag.idTipoPuesto = new SelectList(db.TipoPuestoes, "Id", "tipo", puestoTrabajo.idTipoPuesto);
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre", puestoTrabajo.idUnidad);
            return View(puestoTrabajo);
        }

        // GET: PerfilTrabajo/Edit/5
        public ActionResult Edit(int? id)
        {
            PuestoTrabajoViewModel modelo = new PuestoTrabajoViewModel();
          
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //PuestoTrabajo puestoTrabajo = db.PuestoTrabajoes.Find(id);
            //modelo.puesto = puestoTrabajo;
            
            //if (puestoTrabajo == null)
            //{
            //    return HttpNotFound();
            //}
            //modelo.Id = puestoTrabajo.id;
            //modelo.funciones = db.FuncionPuestoTrabajoes.Where(r => r.idPuestoTrabajo == id).ToList();
            //modelo.requisitos = db.Requisitoes.Where(r => r.idPuestoTrabajo == id).ToList();
            //modelo.competencias = db.CompetenciaPuestoTrabajoes.Where(r => r.idPuestoTrabajo == id).ToList();

            //modelo.cmbTipoPuesto = db.TipoPuestoes.ToList();
            //modelo.cmbJefeInmediato = db.PuestoTrabajoes.ToList();
            //modelo.cmbUnidades = db.Unidads.ToList();
            //modelo.cmbPuesto = db.PuestoTrabajoes.ToList();
            //modelo.cmbCategoria = db.Categorias.ToList();
            //modelo.cmbCatalogoCompetencia = db.CatalogoCompetencias.ToList();

            return View(modelo);
        }

       
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SaveEdit(PuestoTrabajoViewModel modelo)
        {
            int id = modelo.Id;
            PuestoTrabajo puesto = db.PuestoTrabajoes.SingleOrDefault(r => r.id == id);
            if (puesto != null) {
                puesto.titulo = modelo.puesto.titulo;
                puesto.objetivo = modelo.puesto.objetivo;
                puesto.jefeInmediato = modelo.puesto.jefeInmediato;
                puesto.idUnidad = modelo.puesto.idUnidad;
                puesto.idTipoPuesto = modelo.puesto.idTipoPuesto;
                puesto.fechaCreacion = modelo.puesto.fechaCreacion;
                puesto.activo = modelo.puesto.activo;
                puesto.aprobado = modelo.puesto.aprobado;
                
                db.SaveChanges();
            }

            return  RedirectToAction("Index");
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
        //[ValidateAntiForgeryToken]
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

        public ActionResult GestionPuestoTrabajo(int? id) {
            PuestoTrabajoViewModel modelo = new PuestoTrabajoViewModel();
           
            modelo.cmbTipoPuesto = db.TipoPuestoes.ToList();
            modelo.cmbJefeInmediato = db.PuestoTrabajoes.ToList();
            modelo.cmbUnidades = db.Unidads.ToList();
            modelo.cmbPuesto = db.PuestoTrabajoes.ToList();
            modelo.cmbCategoria = db.Categorias.ToList();
            modelo.cmbCatalogoCompetencia = db.CatalogoCompetencias.ToList();


            if (id != null)
            {
                PuestoTrabajo puestoTrabajo = db.PuestoTrabajoes.Find(id);
                if (puestoTrabajo == null)
                    return HttpNotFound();
                modelo.puesto = puestoTrabajo;
                modelo.Id = puestoTrabajo.id;                
            }
                  
            /*
            modelo.funciones = db.FuncionPuestoTrabajoes.Where(r => r.idPuestoTrabajo == id).ToList();
            modelo.requisitos = db.Requisitoes.Where(r => r.idPuestoTrabajo == id).ToList();
            modelo.competencias = db.CompetenciaPuestoTrabajoes.Where(r => r.idPuestoTrabajo == id).ToList();
            */        

            return View(modelo);
           
        }




    }
}
