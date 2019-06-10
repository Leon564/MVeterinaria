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
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //List<Cita> cits = new List<Cita>();
            //var citas = db.Citas;
            //foreach (var item in citas)
            //{
            //    DateTime fechaf = Convert.ToDateTime(item.FechaCita);
            //    if (fechaf < DateTime.Now.Date)
            //    {
            //        item.EstadoCitaId = 2;

            //    }

            //}
            //db.SaveChanges();
            //if (Request.IsAuthenticated)
            //{
            //    if (User.IsInRole("Admin"))
            //    {
            //        var citasV = (from x in db.Citas
            //                      where x.EstadoCitaId == 1
            //                      select x).Include(c => c.Mascota).Include(c => c.Mascota.Client);
            //        return View(citasV);
            //    }

            //    else
            //    {
            //        var user = User.Identity.GetUserId().ToString();
            //        var citasV = (from x in db.Citas
            //                      where x.EstadoCitaId == 1 && x.Mascota.ClientId == user
            //                      select x).Include(c => c.Mascota).Include(c => c.Mascota.Client);
            //        return View(citasV);
            //    }

            //}
            //else
            //{
            //    return View();
            //}

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
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    var citasV = (from x in db.Citas
                                  where x.EstadoCitaId == 1
                                  select x).Include(c => c.Mascota).Include(c => c.Mascota.Client).Include(c => c.Vet);
                    return View(citasV);
                }

                else
                {
                    var user = User.Identity.GetUserId().ToString();
                    var citasV = (from x in db.Citas
                                  where x.EstadoCitaId == 1 && x.Mascota.ClientId == user
                                  select x).Include(c => c.Mascota).Include(c => c.Mascota.Client).Include(c => c.Vet);
                    return View(citasV);
                }

            }
            else
            {
                return View();
            }


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}