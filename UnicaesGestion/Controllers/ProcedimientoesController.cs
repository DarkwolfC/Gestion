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
            ProcedimientoViewModel modelo = new ProcedimientoViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedimiento procedimiento = db.Procedimientoes.Find(id);
            if (procedimiento == null)
            {
                return HttpNotFound();
            }
            modelo.id = procedimiento.id;
            modelo.cmbtipoPasos = db.TipoPasoes.ToList();
            modelo.cmbPuestos = db.PuestoTrabajoes.ToList();
            return View(modelo);
        }

        // POST: Procedimientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ProcedimientoViewModel modelo)
        {
            int id = modelo.id;
            Procedimiento procedimiento = db.Procedimientoes.SingleOrDefault(r => r.id == id);
            if (procedimiento!=null)
            {
                procedimiento.nombre = modelo.procedimiento.nombre;
                procedimiento.objetivoInicial = modelo.procedimiento.objetivoInicial;
                procedimiento.objetvioFinal = modelo.procedimiento.objetvioFinal;

                db.SaveChanges();
             
            }
            return RedirectToAction("Index");
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


        public ActionResult GestionProcedimiento(int? id)
        {
            ProcedimientoViewModel modelo = new ProcedimientoViewModel();
            modelo.cmbtipoPasos = db.TipoPasoes.ToList();
            modelo.cmbPuestos = db.PuestoTrabajoes.ToList();       
            
            if (id != null)
            {
                Procedimiento procedimiento = db.Procedimientoes.Find(id);               
                if (procedimiento == null)
                    return HttpNotFound();
                modelo.procedimiento = procedimiento;
                modelo.id = procedimiento.id;
            }
            
            return View(modelo);
        }

        public ActionResult CrearProcedimiento(string nombre, string objetivoInicial, string objetivoFinal)
        {
            try
            {
                Procedimiento p = new Procedimiento();
                p.nombre = nombre;
                p.objetivoInicial = objetivoInicial;
                p.objetvioFinal = objetivoFinal;               
                db.Procedimientoes.Add(p);

                if (db.SaveChanges() > 0)
                    return Json(new { result = "success", data = p.id }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { result = "fail", data = "Error creando nuevo procedimiento" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error creando nuevo procedimiento.." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ModificarProcedimiento(int id, string nombre, string objetivoInicial, string objetivoFinal)
        {
            try
            {
                Procedimiento p = db.Procedimientoes.FirstOrDefault(r => r.id == id);
                
                if (p != null)
                {
                    p.nombre = nombre;
                    p.objetivoInicial = objetivoInicial;
                    p.objetvioFinal = objetivoFinal;                 
                    db.SaveChanges();
                    return Json(new { result = "success", data = id }, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json(new { result = "fail", data = "Error actualizando procedimiento" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error creando nuevo procedimiento.." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult PasosProcedimiento(int? id)
        {
            List<Paso> lst = new List<Paso>();           
            if (id != null)
                lst = db.Pasoes.Where(r => r.idProcedimiento == id).ToList();
            return View(lst);
        }

        public ActionResult AgregarPaso(int idProcedimiento, int numero, string descripcion, string predecesores, string idTipoPaso, string idPuestoTrabajo)
        {
            try
            {
                Procedimiento p = db.Procedimientoes.FirstOrDefault(r => r.id == idProcedimiento);

                if (p != null)
                {
                    Paso paso = new Paso
                    {
                        idProcedimiento = idProcedimiento,
                        numero = numero,
                        descripcion = descripcion,
                        predecesores = predecesores,
                        idTipoPaso = int.Parse(idTipoPaso),
                        idPuestoTrabajo = int.Parse(idPuestoTrabajo)

                    };

                    db.Pasoes.Add(paso);

                    if (db.SaveChanges() > 0)
                        return Json(new { result = "success", data = "Paso agregado satisfactoriamente. " }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { result = "fail", data = "Error agregando paso.Identificador de procedimiento no válido. " }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { result = "fail", data = "Error agregando paso.Identificador de procedimiento no válido. " }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error agregando paso.." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult EditarPaso(int idpaso , int numero, string descripcion, string predecesores, int tipoPaso, int puestoTrabajo)
        {

            Paso p = db.Pasoes.FirstOrDefault(r => r.id == idpaso);
            try
            {
                if (p != null)
                {
                    p.numero= numero;
                    p.descripcion = descripcion;
                    p.predecesores = predecesores;
                    p.idTipoPaso = tipoPaso;
                    p.idPuestoTrabajo = puestoTrabajo;
                    db.SaveChanges();
                    return Json(
                        new { result = "success", data = "Paso agregado satisfactoriamente. " },
                        JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(
                        new
                        {
                            result = "fail",
                            data =
                                "Error agregando paso.Identificador de procedimiento no válido. "
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error editando paso. " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EliminarPaso(int id)
        {
            try
            {
                Paso p = db.Pasoes.FirstOrDefault(r => r.id == id);               
                if (p != null)
                {
                    db.Pasoes.Remove(p);
                    db.SaveChanges();
                    return Json(new { result = "success", data = "Paso eliminado satisfactoriamente. " }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "fail", data = "Error agregando paso.Identificador de procedimiento no válido. " }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error eliminando paso. " + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
