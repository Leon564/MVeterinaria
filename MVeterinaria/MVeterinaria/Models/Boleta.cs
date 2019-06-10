using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVeterinaria.Models
{
    public class Boleta
    {
        public int BoletaId { get; set; }
        public Cita Cita { get; set; }
        public int CitaId { get; set; }
        public string Diagnostico { get; set; }
        public string Comentarios { get; set; }
        public decimal Costo { get; set; }
    }
}