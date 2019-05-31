using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVeterinaria.Models
{
    public class Cita
    {
        public int CitaId { get; set; }
        public string FechaEmision { get; set; }
        public string FechaCita { get; set; }

        public Mascota Mascota { get; set; }
        public int MascotaId { get; set; }


        
        public Veterinario Veterinario { get; set; }
        public int VeterinarioId { get; set; }

        public int EstadoCitaId { get; set; }
        public EstadoCita EstadoCita { get; set; }
        

       

    }
    public class EstadoCita
    {
        public int EstadoCitaId { get; set; }
        public string Estado { get; set; }
    }
        
}