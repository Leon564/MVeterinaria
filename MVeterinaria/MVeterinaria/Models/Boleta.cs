using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVeterinaria.Models
{
    public class Boleta
    {
        public int BoletaId { get; set; }
        public string FechaEmision { get; set; }
        public Veterinario Veterinario { get; set; }
        public int VeterinarioId { get; set; }
        public Mascota Mascota { get; set; }
        public int MascotaId { get; set; }
        
     

    }
}