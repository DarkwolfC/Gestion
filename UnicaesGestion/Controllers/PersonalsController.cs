using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnicaesGestion;
using UnicaesGestion.Models;

namespace UnicaesGestion.Controllers
{
    public class PersonalsController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: Personals
        public async Task<ActionResult> Index()
        {
            var personals = db.Personals.Include(p => p.PuestoTrabajo).Include(p => p.Credential);
            return View(await personals.ToListAsync());
        }

        // GET: Personals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = await db.Personals.FindAsync(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // GET: Personals/Create
        public ActionResult AddUser()
        {
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo");
            ViewBag.id = new SelectList(db.Credentials, "idPersonal", "user");
            return View();
        }

        // POST: Personals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser([Bind(Include = "id,nombre,apellido,foto,idPuestoTrabajo")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                db.Personals.Add(personal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", personal.idPuestoTrabajo);
            ViewBag.id = new SelectList(db.Credentials, "idPersonal", "user", personal.id);
            return View(personal);
        }

        // GET: Personals/Edit/5
        public async Task<ActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = await db.Personals.FindAsync(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", personal.idPuestoTrabajo);
            ViewBag.id = new SelectList(db.Credentials, "idPersonal", "user", personal.id);
            return View(personal);
        }

        // POST: Personals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser([Bind(Include = "id,nombre,apellido,foto,idPuestoTrabajo")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personal).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idPuestoTrabajo = new SelectList(db.PuestoTrabajoes, "id", "titulo", personal.idPuestoTrabajo);
            ViewBag.id = new SelectList(db.Credentials, "idPersonal", "user", personal.id);
            return View(personal);
        }

        // GET: Personals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = await db.Personals.FindAsync(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // POST: Personals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Personal personal = await db.Personals.FindAsync(id);
            db.Personals.Remove(personal);
            await db.SaveChangesAsync();
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



        public ActionResult GestionUsuario(int? id)
        {
            UsuarioViewModel modelo = new UsuarioViewModel();
            modelo.cmbRol = db.Roles.ToList();    
            
            if (id != null)
            {
                Personal personal = db.Personals.Find(id);
              
                if (personal== null)
                    return HttpNotFound();
                modelo.personal = personal;
                modelo.Id = personal.id;
            }

      

            return View(modelo);

        }

        public ActionResult CrearUsuario( string nombre, string apellido, string puesto)
        {
            try
            {
                Personal p = new Personal();
                p.nombre = nombre;
                p.apellido = apellido;
                if (int.Parse(puesto) != 0)
                    p.idPuestoTrabajo = int.Parse(puesto);                       
                db.Personals.Add(p);

                if (db.SaveChanges() > 0)
                    return Json(new { result = "success", data = p.id }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { result = "fail", data = "Error creando nuevo usuario" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error creando nuevo usuario.." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ModificarUsuario(int id, string nombre, string apellido, string puesto)
        {
            try
            {
                Personal p = db.Personals.FirstOrDefault(r => r.id == id);
               if (p != null)
                {
                    p.nombre = nombre;
                    p.apellido = apellido;
                    if (int.Parse(puesto) != 0)
                        p.idPuestoTrabajo = int.Parse(puesto);
                    db.SaveChanges();
                    return Json(new { result = "success", data = id }, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json(new { result = "fail", data = "Error actualizando al usuario" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { result = "fail", data = "Error actualizando al usuario.." + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
