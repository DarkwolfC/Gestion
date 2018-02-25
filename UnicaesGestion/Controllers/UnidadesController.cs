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
    public class UnidadesController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: Unidades
        public ActionResult Index()
        {
            var unidads = db.Unidads.Include(u => u.Unidad2);
            return View(unidads.ToList());
        }

        // GET: Unidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidad unidad = db.Unidads.Find(id);
            if (unidad == null)
            {
                return HttpNotFound();
            }
            return View(unidad);
        }

        // GET: Unidades/Create
        public ActionResult Create()
        {
            ViewBag.idUnidad = new SelectList(db.Unidads, "id", "nombre");//Agregado para las funciones
            ViewBag.depende = new SelectList(db.Unidads, "id", "nombre");
            return View();
        }

        // POST: Unidades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,objetivo,depende")] Unidad unidad)
        {
            if (ModelState.IsValid)
            {
                db.Unidads.Add(unidad);
                db.SaveChanges();
                Response.Cookies["llave"]["idUnidad"] = unidad.id.ToString();
                Response.Cookies["llave"].Expires = DateTime.Now.AddMinutes(45);
                return RedirectToAction("Create","FuncionUnidads", new { idPuestoTrabajo = unidad.id });
            }

            ViewBag.depende = new SelectList(db.Unidads, "id", "nombre", unidad.depende);
            return View(unidad);
        }

        // GET: Unidades/Edit/5
        public ActionResult Edit(int? id)
        {
            UnidadViewModel modelo = new UnidadViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidad unidad = db.Unidads.Find(id);
            if (unidad == null)
            {
                return HttpNotFound();
            }
            modelo.FuncionUnidades = db.FuncionUnidads.Where(r => r.idUnidad == id).ToList();
            //modelo.FuncionUnidades = db.FuncionUnidads.ToList();
            return View(modelo);
        }

        // POST: Unidades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(UnidadViewModel modelo)
        {
            int id = modelo.id;
            Unidad unidad = db.Unidads.SingleOrDefault(r => r.id == id);
            if (unidad !=null)
            {
                unidad.nombre = modelo.unidad.nombre;
                unidad.objetivo = modelo.unidad.objetivo;
                unidad.depende = modelo.unidad.depende;
                //modelo.FuncionUnidades = ;
                db.SaveChanges();
              
            }

            return RedirectToAction("Index");
        }

        // GET: Unidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidad unidad = db.Unidads.Find(id);
            if (unidad == null)
            {
                return HttpNotFound();
            }
            return View(unidad);
        }

        // POST: Unidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Unidad unidad = db.Unidads.Find(id);
            db.Unidads.Remove(unidad);
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

        //Mi codigo

        public ActionResult GestionUnidad(int? id)
        {
            UnidadViewModel modelo = new UnidadViewModel();
            modelo.cmbUnidad = db.Unidads.ToList();       
            
            if (id != null)
            {
                Unidad unidad = db.Unidads.Find(id);
                if (unidad == null)
                    return HttpNotFound();
                modelo.unidad = unidad;
                modelo.id= unidad.id;
            }
            
            return View(modelo);

        }

        public ActionResult CrearUnidad(string nombre, string objetivo, string depende)
        {
            try
            {
                Unidad u = new Unidad();
                u.nombre = nombre;
                u.objetivo = objetivo;
                if (int.Parse(depende) != 0)
                    u.depende = int.Parse(depende);              
                db.Unidads.Add(u);

                if (db.SaveChanges() > 0)
                    return Json(new { result = "success", data = u.id }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { result = "fail", data = "Error creando la nueva unidad" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error creando la nueva unidad.." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ModificarUnidad(int id, string nombre, string objetivo, string depende)
        {
            try
            {
                Unidad u = db.Unidads.FirstOrDefault(r => r.id == id);
                
                if (u != null)
                {
                    u.nombre = nombre;
                    u.objetivo = objetivo;
                    if (int.Parse(depende) != 0)
                        u.depende = int.Parse(depende);
                    db.SaveChanges();
                    return Json(new { result = "success", data = id }, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json(new { result = "fail", data = "Error actualizando unidad" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error creando la nueva unidad.." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FuncionesUnidad(int? id)
        {
            List<FuncionUnidad> lst = new List<FuncionUnidad>();
            if (id != null)
                lst = db.FuncionUnidads.Where(r => r.idUnidad == id).ToList();
            return View(lst);
        }

        public ActionResult AgregarFuncion(int idunidad, String funcion)
        {
            try
            {
                Unidad u = db.Unidads.FirstOrDefault(r => r.id == idunidad);
                
                if (u != null)
                {
                    FuncionUnidad f = new FuncionUnidad
                    {
                        
                        idUnidad = idunidad,
                        descripcion = funcion
                    };

                    db.FuncionUnidads.Add(f);

                    if (db.SaveChanges() > 0)
                        return Json(new { result = "success", data = "Funcion de puesto de trabajo agregada satisfactoriamente. " }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { result = "fail", data = "Error agregando funcion de unidad.Identificador de unidad no válido. " }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { result = "fail", data = "Error agregando funcion de unidad.Identificador de unidad no válido.  " }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error agregando funcion de la unidad.." + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult EditarFuncion(int idfuncion, string funcion)
        {
            FuncionUnidad f = db.FuncionUnidads.FirstOrDefault(r => r.id == idfuncion);           
            try
            {
                if (f != null)
                {
                    f.descripcion = funcion;
                    db.SaveChanges();
                    return Json(
                        new { result = "success", data = "Funcion de puesto de unidad editada satisfactoriamente. " },
                        JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(
                        new
                        {
                            result = "fail",
                            data =
                                "Error editando funcion de unidad.Identificador de unidad no válido. "
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error editando funcion de unidad. " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EliminarFuncion(int id)
        {
            try
            {
                FuncionUnidad f = db.FuncionUnidads.FirstOrDefault(r => r.id == id);               
                if (f != null)
                {
                    db.FuncionUnidads.Remove(f);
                    db.SaveChanges();
                    return Json(new { result = "success", data = "Funcion eliminada satisfactoriamente. " }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "fail", data = "Error eliminando funcion de unidad.Identificador no válido. " }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error eliminando funcion de unidad. " + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }


    }
}
