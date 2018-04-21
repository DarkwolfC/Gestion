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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace UnicaesGestion.Controllers
{
    public class PersonalsController : Controller
    {
        private GestionEntities db = new GestionEntities();
        private UserManager<IdentityUser> UserManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();


        // GET: Personals
        public async Task<ActionResult> Index()
        {
            var personals = db.Personals.Include(p => p.PuestoTrabajo).Include(p => p.Credential);
            return View(await personals.ToListAsync());
        }

        public ActionResult GestionUsuario(int? id)
        {
            UsuarioViewModel modelo = new UsuarioViewModel();
            modelo.cmbRol = db.Roles.ToList();
            modelo.cmbPuesto = db.PuestoTrabajoes.ToList();

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


        [HttpPost]
        public async Task<ActionResult> CrearUsuario(UsuarioViewModel model)
        {
            try
            {
                Personal p = new Personal();
                p.nombre = model.personal.nombre;
                p.apellido = model.personal.apellido;
                IdentityResult r = await UserManager.CreateAsync(new IdentityUser(model.user.Username), model.user.Password);
                //agregar rol y claim active 
                
                db.Personals.Add(p);

                if (db.SaveChanges() > 0 && r.Succeeded)
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
