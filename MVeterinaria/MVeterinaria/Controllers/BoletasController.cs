using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVeterinaria.Models;

namespace MVeterinaria.Controllers
{
    public class BoletasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Boletas
        public ActionResult Index()
        {
            var boletas = db.Boletas.Include(b => b.Mascota).Include(b => b.Veterinario);
            return View(boletas.ToList());
        }

        // GET: Boletas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boleta boleta = db.Boletas.Find(id);
            if (boleta == null)
            {
                return HttpNotFound();
            }
            return View(boleta);
        }

        // GET: Boletas/Create
        public ActionResult Create()
        {
            ViewBag.MascotaId = new SelectList(db.Mascotas, "MascotaId", "Nombre");
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "VeterinarioId", "Nombre");
            return View();
        }

        // POST: Boletas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BoletaId,FechaEmision,VeterinarioId,MascotaId")] Boleta boleta)
        {
            if (ModelState.IsValid)
            {
                db.Boletas.Add(boleta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MascotaId = new SelectList(db.Mascotas, "MascotaId", "Nombre", boleta.MascotaId);
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "VeterinarioId", "Nombre", boleta.VeterinarioId);
            return View(boleta);
        }

        // GET: Boletas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boleta boleta = db.Boletas.Find(id);
            if (boleta == null)
            {
                return HttpNotFound();
            }
            ViewBag.MascotaId = new SelectList(db.Mascotas, "MascotaId", "Nombre", boleta.MascotaId);
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "VeterinarioId", "Nombre", boleta.VeterinarioId);
            return View(boleta);
        }

        // POST: Boletas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BoletaId,FechaEmision,VeterinarioId,MascotaId")] Boleta boleta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boleta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MascotaId = new SelectList(db.Mascotas, "MascotaId", "Nombre", boleta.MascotaId);
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "VeterinarioId", "Nombre", boleta.VeterinarioId);
            return View(boleta);
        }

        // GET: Boletas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boleta boleta = db.Boletas.Find(id);
            if (boleta == null)
            {
                return HttpNotFound();
            }
            return View(boleta);
        }

        // POST: Boletas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Boleta boleta = db.Boletas.Find(id);
            db.Boletas.Remove(boleta);
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
