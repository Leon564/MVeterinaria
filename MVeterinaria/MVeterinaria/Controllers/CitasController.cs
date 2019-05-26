using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVeterinaria.Models;
using System.Net;
using System.Net.Mail;


namespace MVeterinaria.Controllers
{
    public class CitasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Citas
        public ActionResult Index()
        {
            var citas = db.Citas.Include(c => c.Mascota).Include(c => c.Veterinario);
            return View(citas.ToList());
        }
        public ActionResult Email()
        {
            var use = (from x in db.Users
                       select x.Email).ToList();
            foreach (var item in use)
            {
                //CORREO
                MailMessage Correo = new MailMessage();
                Correo.From = new MailAddress("nleon564@gmail.com");
                Correo.To.Add(item);
                Correo.Subject = ("SYSTEM");
                Correo.Body = "hOLA ";
                Correo.Priority = MailPriority.Normal;
                // SMPT
                SmtpClient ServerMail = new SmtpClient();
                ServerMail.Credentials = new NetworkCredential("nleon564@gmail.com", "otosaka1");
                ServerMail.Host = "smtp.gmail.com";
                ServerMail.Port = 587;
                ServerMail.EnableSsl = true;
                try
                {
                    ServerMail.Send(Correo);
                }
                catch (Exception ex)
                {

                }
                
            }
            return RedirectToAction("Index");
        }

        // GET: Citas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: Citas/Create
        public ActionResult Create()
        {
            ViewBag.MascotaId = new SelectList(db.Mascotas, "MascotaId", "Nombre");
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "VeterinarioId", "Nombre");
            var default_Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T'); 
            ViewBag.AAA = default_Value;
            return View();
        }

        // POST: Citas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CitaId,FechaEmision,FechaCita,MascotaId,VeterinarioId")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Citas.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MascotaId = new SelectList(db.Mascotas, "MascotaId", "Nombre", cita.MascotaId);
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "VeterinarioId", "Nombre", cita.VeterinarioId);
            return View(cita);
        }

        // GET: Citas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.MascotaId = new SelectList(db.Mascotas, "MascotaId", "Nombre", cita.MascotaId);
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "VeterinarioId", "Nombre", cita.VeterinarioId);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CitaId,FechaEmision,FechaCita,MascotaId,VeterinarioId")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MascotaId = new SelectList(db.Mascotas, "MascotaId", "Nombre", cita.MascotaId);
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "VeterinarioId", "Nombre", cita.VeterinarioId);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cita cita = db.Citas.Find(id);
            db.Citas.Remove(cita);
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
