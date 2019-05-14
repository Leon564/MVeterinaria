using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVeterinaria.Models
{
    public class Cita
    {
        public int CitaId { get; set; }
        public DateTime FechaCita { get; set; }

        public Boleta Boleta { get; set; }
        public int BoletaId { get; set; }
    }
}