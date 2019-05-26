using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVeterinaria.Models
{
    public class Cita
    {
        public int CitaId { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaCita { get; set; }

        public virtual Mascota Mascota { get; set; }
        public int MascotaId { get; set; }


        
        public Veterinario Veterinario { get; set; }
        public int VeterinarioId { get; set; }

    }
}