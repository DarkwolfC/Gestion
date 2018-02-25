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
            modelo.cmbJefeInmediato = db.PuestoTrabajoes.ToList();
            modelo.cmbUnidades = db.Unidads.ToList();
            modelo.cmbTipoPuesto = db.TipoPuestoes.ToList();

            /*
            modelo.cmbPuesto = db.PuestoTrabajoes.ToList();
            modelo.cmbCategoria = db.Categorias.ToList();
            modelo.cmbCatalogoCompetencia = db.CatalogoCompetencias.ToList();
            */

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

        public ActionResult CrearPuestoTrabajo(string titulo, string objetivo, string jefe, string unidad, string tipo, string estado, string aprobacion, string fecha  )
        {
            try
            {
                PuestoTrabajo p = new PuestoTrabajo();
                p.titulo = titulo;
                p.objetivo = objetivo;
                if (int.Parse(jefe) != 0)
                    p.jefeInmediato = int.Parse(jefe);
                p.idUnidad = int.Parse(unidad);
                p.idTipoPuesto = int.Parse(tipo);
                p.fechaCreacion = DateTime.Parse(fecha);
                p.activo = (estado.Equals("1")) ? true : false;
                p.aprobado = (aprobacion.Equals("1")) ? true : false;
                db.PuestoTrabajoes.Add(p);

                if (db.SaveChanges() > 0) 
                   return Json(new {result = "success", data = p.id}, JsonRequestBehavior.AllowGet);
                else
                   return Json(new { result = "fail", data = "Error creando nuevo puesto de trabajo" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error creando nuevo puesto de trabajo.."+ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ModificarPuestoTrabajo(int id, string titulo, string objetivo, string jefe, string unidad, string tipo, string estado, string aprobacion, string fecha)
        {
            try
            {
                PuestoTrabajo p = db.PuestoTrabajoes.FirstOrDefault(r => r.id == id);
                if (p != null)
                {
                    p.titulo = titulo;
                    p.objetivo = objetivo;
                    if (int.Parse(jefe) != 0)
                        p.jefeInmediato = int.Parse(jefe);
                    p.idUnidad = int.Parse(unidad);
                    p.idTipoPuesto = int.Parse(tipo);
                    p.fechaCreacion = DateTime.Parse(fecha);
                    p.activo = (estado.Equals("1")) ? true : false;
                    p.aprobado = (aprobacion.Equals("1")) ? true : false;
                    db.SaveChanges();
                    return Json(new { result = "success", data = id }, JsonRequestBehavior.AllowGet);
                   
                }
                else
                    return Json(new { result = "fail", data = "Error actualizando puesto de trabajo" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error creando nuevo puesto de trabajo.." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FuncionesPuestoTrabajo(int? id)
        {
            List<FuncionPuestoTrabajo> lst = new List<FuncionPuestoTrabajo>();
            if (id != null)
                lst = db.FuncionPuestoTrabajoes.Where(r => r.idPuestoTrabajo == id).ToList();
            return View(lst);
        }

        public ActionResult AgregarFuncion(int idpuesto, String funcion)
        {
            try
            {
                PuestoTrabajo p = db.PuestoTrabajoes.FirstOrDefault(r => r.id == idpuesto);

                if (p != null)
                {
                    FuncionPuestoTrabajo f = new FuncionPuestoTrabajo
                    {
                        idPuestoTrabajo = idpuesto,
                        funcion = funcion
                    };

                    db.FuncionPuestoTrabajoes.Add(f);

                    if (db.SaveChanges()>0)
                        return Json(new { result = "success", data = "Funcion de puesto de trabajo agregada satisfactoriamente. " }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { result = "fail", data = "Error agregando funcion puesto de trabajo.Identificador de puesto de trabajo no válido. " }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { result = "fail", data = "Error agregando funcion puesto de trabajo.Identificador de puesto de trabajo no válido. " }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error agregando funcion puesto de trabajo.." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult EditarFuncion(int idfuncion, string funcion)
        {
            FuncionPuestoTrabajo f = db.FuncionPuestoTrabajoes.FirstOrDefault(r => r.id == idfuncion);
            try
            {
                if (f != null)
                {
                    f.funcion = funcion;
                    db.SaveChanges();
                    return Json(
                        new {result = "success", data = "Funcion de puesto de trabajo editada satisfactoriamente. "},
                        JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(
                        new
                        {
                            result = "fail",
                            data =
                                "Error editando funcion puesto de trabajo.Identificador de puesto de trabajo no válido. "
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error editando funcion puesto de trabajo. " +ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EliminarFuncion(int id)
        {
            try
            {
                FuncionPuestoTrabajo f = db.FuncionPuestoTrabajoes.FirstOrDefault(r => r.id == id);
                if (f != null)
                {
                    db.FuncionPuestoTrabajoes.Remove(f);
                    db.SaveChanges();
                    return Json(new { result = "success", data = "Funcion eliminada satisfactoriamente. " }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "fail", data = "Error eliminando funcion puesto de trabajo.Identificador no válido. " }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error eliminando funcion puesto de trabajo. " + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult RequisitosPuestoTrabajo(int idpuesto)
        {
            List<RequisitosPuestoViewModel> lst = (from r in db.Requisitoes
                join c in db.Categorias on r.idCategoria equals c.id
                where r.idPuestoTrabajo == idpuesto
                select new RequisitosPuestoViewModel { IdRequisito= r.id, Descripcion=r.descripcion, IdCategoria=r.idCategoria, Categoria = c.categoria1}).ToList();
            
            return View(lst);
        }


    }
}
