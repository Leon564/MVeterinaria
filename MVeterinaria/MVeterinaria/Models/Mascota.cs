using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVeterinaria.Models
{
    public class Mascota
    {
        public int MascotaId { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public string Especie { get; set; }
        public string Sexo { get; set; }
        //public ApplicationUser user { get; set; }
        //public int userId { get; set; }
        public Cliente Ciente { get; set; }
        public int ClienteId { get; set; }
        
    }
}