using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVeterinaria.Models
{
    public class Mascota
    {
        public int MascotaId { get; set; }
        public string Nombre { get; set; }

        public Raza Raza { get; set; }
        public int RazaId { get; set; }


        public Sexo Sexo { get; set; }
        public int SexoId { get; set; }





        public virtual string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }











    }
}