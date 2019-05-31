using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVeterinaria.Models;

namespace MVeterinaria.Controllers
{
    [Authorize]
    public class MascotasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Mascotas
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            var masc = (from ar in db.Mascotas
                        where ar.ClientId == id
                        select ar).Include(m => m.Raza).Include(m => m.Sexo);
            //return mascotas;
            return View(masc.ToList());
            //var mascotas = db.Mascotas.Include(m => m.Raza).Include(m => m.Sexo);
            //return View(mascotas.ToList());
        }

        // GET: Mascotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = db.Mascotas.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // GET: Mascotas/Create
        public ActionResult Create()
        {
            ViewBag.RazaId = new SelectList(db.Razas, "RazaId", "Nombre");
            ViewBag.SexoId = new SelectList(db.Sexos, "SexoId", "Nombre");
            return View();
        }

        // POST: Mascotas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MascotaId,Nombre,RazaId,SexoId")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                mascota.ClientId = User.Identity.GetUserId();
                db.Mascotas.Add(mascota);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RazaId = new SelectList(db.Razas, "RazaId", "Nombre", mascota.RazaId);
            ViewBag.SexoId = new SelectList(db.Sexos, "SexoId", "Nombre", mascota.SexoId);
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = db.Mascotas.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            ViewBag.RazaId = new SelectList(db.Razas, "RazaId", "Nombre", mascota.RazaId);
            ViewBag.SexoId = new SelectList(db.Sexos, "SexoId", "Nombre", mascota.SexoId);
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MascotaId,Nombre,RazaId,SexoId")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                mascota.ClientId = User.Identity.GetUserId();
                db.Entry(mascota).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RazaId = new SelectList(db.Razas, "RazaId", "Nombre", mascota.RazaId);
            ViewBag.SexoId = new SelectList(db.Sexos, "SexoId", "Nombre", mascota.SexoId);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = db.Mascotas.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mascota mascota = db.Mascotas.Find(id);
            db.Mascotas.Remove(mascota);
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
