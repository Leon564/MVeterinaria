using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVeterinaria.Models
{
    public class veterinario
    {
        public int VeterinarioId { get; set; }
        public virtual string UsId { get; set; }

        public virtual ApplicationUser Us { get; set; }
    }
}