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
    public class CriteriosController : Controller
    {
        private GestionEntities db = new GestionEntities();

        // GET: Criterios
        public async Task<ActionResult> Index()
        {
            return View(await db.Criterios.ToListAsync());
        }

        // GET: Criterios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criterio criterio = await db.Criterios.FindAsync(id);
            if (criterio == null)
            {
                return HttpNotFound();
            }
            return View(criterio);
        }

        // GET: Criterios/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Criterios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,criterio1")] Criterio criterio)
        {
            if (ModelState.IsValid)
            {
                db.Criterios.Add(criterio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(criterio);
        }

        // GET: Criterios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criterio criterio = await db.Criterios.FindAsync(id);
            if (criterio == null)
            {
                return HttpNotFound();
            }
            return View(criterio);
        }

        // POST: Criterios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,criterio1")] Criterio criterio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(criterio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(criterio);
        }

        // GET: Criterios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criterio criterio = await db.Criterios.FindAsync(id);
            if (criterio == null)
            {
                return HttpNotFound();
            }
            return View(criterio);
        }

        // POST: Criterios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Criterio criterio = await db.Criterios.FindAsync(id);
            db.Criterios.Remove(criterio);
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
