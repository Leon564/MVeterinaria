using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVeterinaria.Models;
using Microsoft.AspNet.Identity;

namespace MVeterinaria.Controllers
{
    public class BoletasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Boletas
        public ActionResult Index()
        {
            var boletas = db.Boletas.Include(b => b.Cita);
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
            string us = User.Identity.GetUserId();
            var lista = (from x in db.Citas
                         where x.EstadoCitaId==1 && x.VetId==us

                         select x).Include(x => x.Mascota);
            ViewBag.CitaId = new SelectList(lista, "CitaId", "Mascota.Nombre");
            //ViewBag.CitaId = new SelectList(db.Citas, "CitaId", "FechaEmision");
            return View();
        }

        // POST: Boletas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BoletaId,CitaId,Diagnostico,Comentarios,Costo")] Boleta boleta)
        {
            if (ModelState.IsValid)
            {
                Cita cita = new Cita();
                var cits = (from x in db.Citas
                            where x.CitaId == boleta.CitaId
                            select x);
                foreach (var item in cits)
                {
                    item.EstadoCitaId = 2;
                    
                }
                db.Boletas.Add(boleta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var lista = (from x in db.Citas
                         select x).Include(x => x.Mascota);
            ViewBag.CitaId = new SelectList(lista, "CitaId", "Mascota.Nombre", boleta.CitaId);
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
            ViewBag.CitaId = new SelectList(db.Citas, "CitaId", "FechaEmision", boleta.CitaId);
            return View(boleta);
        }

        // POST: Boletas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BoletaId,CitaId,Diagnostico,Comentarios,Costo")] Boleta boleta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boleta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CitaId = new SelectList(db.Citas, "CitaId", "FechaEmision", boleta.CitaId);
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
