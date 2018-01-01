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
        public ActionResult Create()
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
        public async Task<ActionResult> Create([Bind(Include = "id,nombre,apellido,foto,idPuestoTrabajo")] Personal personal)
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
        public async Task<ActionResult> Edit(int? id)
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
        public async Task<ActionResult> Edit([Bind(Include = "id,nombre,apellido,foto,idPuestoTrabajo")] Personal personal)
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
    }
}
