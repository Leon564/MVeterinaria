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
    public class VeterinariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Veterinarios
        public ActionResult Index()
        {
            return View(db.Veterinarios.ToList());
        }

        // GET: Veterinarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinario veterinario = db.Veterinarios.Find(id);
            if (veterinario == null)
            {
                return HttpNotFound();
            }
            return View(veterinario);
        }

        // GET: Veterinarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Veterinarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VeterinarioId,Nombre,Apellido,Direccion,Telefono")] Veterinario veterinario)
        {
            if (ModelState.IsValid)
            {
                db.Veterinarios.Add(veterinario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(veterinario);
        }

        // GET: Veterinarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinario veterinario = db.Veterinarios.Find(id);
            if (veterinario == null)
            {
                return HttpNotFound();
            }
            return View(veterinario);
        }

        // POST: Veterinarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VeterinarioId,Nombre,Apellido,Direccion,Telefono")] Veterinario veterinario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veterinario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(veterinario);
        }

        // GET: Veterinarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinario veterinario = db.Veterinarios.Find(id);
            if (veterinario == null)
            {
                return HttpNotFound();
            }
            return View(veterinario);
        }

        // POST: Veterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Veterinario veterinario = db.Veterinarios.Find(id);
            db.Veterinarios.Remove(veterinario);
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
