using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVeterinaria.Models;

namespace MVeterinaria.Controllers
{
    [Authorize]
    public class CitasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Citas
        public ActionResult Index()
        {

            List<Cita> cits = new List<Cita>();
            var citas = db.Citas;
            foreach (var item in citas)
            {
                DateTime fechaf = Convert.ToDateTime(item.FechaCita);
                if (fechaf < DateTime.Now.Date)
                {
                    item.EstadoCitaId = 2;
                    
                }

            }
            db.SaveChanges();
            
            if (User.IsInRole("Admin"))
            {
                var citasV = (from x in db.Citas
                              where x.EstadoCitaId == 1
                              select x).Include(c => c.Mascota).Include(c => c.Veterinario).Include(c => c.Mascota.Client);
                return View(citasV);
            }
            else
            {
                var user = User.Identity.GetUserId().ToString();
                var citasV = (from x in db.Citas
                              where x.EstadoCitaId == 1 && x.Mascota.ClientId==user
                              select x).Include(c => c.Mascota).Include(c => c.Veterinario).Include(c => c.Mascota.Client);
                return View(citasV);
            }
            
        }
        [Authorize(Roles = "Admin")]
        //Correos Citas
        public ActionResult Email()
        {
            string fecha = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string fecha2 = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");


            var Masc = (from x in db.Citas
                        where x.FechaCita == fecha || x.FechaCita == fecha2
                        select x).ToList();
            foreach (var item in Masc)
            {
                var Clien = (from c in db.Mascotas
                             where c.MascotaId == item.MascotaId
                             select c).ToList();
                foreach (var item2 in Clien)
                {
                    var Em = (from v in db.Users
                              where v.Id == item2.ClientId
                              select v).ToList();
                    foreach (var usuario in Em)
                    {
                        MailMessage Correo = new MailMessage();
                        Correo.From = new MailAddress("happypet.vetsv@gmail.com");
                        Correo.To.Add(usuario.Email);
                        Correo.Subject = ("RECORDATORIO CITA");
                        Correo.Body = "Hola, " + usuario.Nombre + " " + usuario.Apellido + Environment.NewLine + " Se le recuerda que tiene una cita programada de su mascota : " + item2.Nombre + Environment.NewLine + " para la el dia: " + item.FechaCita;
                        Correo.Priority = MailPriority.Normal;
                        // SMPT
                        SmtpClient ServerMail = new SmtpClient();
                        ServerMail.Credentials = new NetworkCredential("happypet.vetsv@gmail.com", "configuracion1");
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
                        Correo.Dispose();

                    }


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
            if (User.IsInRole("Admin"))
            {
                ViewBag.MascotaId = new SelectList(db.Mascotas, "MascotaId", "Nombre");
                ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "VeterinarioId", "Nombre");
            }
            else
            {
                string id = User.Identity.GetUserId();
                var masc = (from ar in db.Mascotas
                            where ar.ClientId == id
                            select ar);
                ViewBag.MascotaId = new SelectList(masc, "MascotaId", "Nombre");
                ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "VeterinarioId", "Nombre");
            }

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
              
                    cita.FechaEmision = DateTime.Now.ToShortDateString();
                    cita.EstadoCitaId = 1;
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
