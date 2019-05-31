using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVeterinaria.Models;


namespace MVeterinaria.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
       
        public string Nombre { get; set; }
        public string Apellido { get; set; }
       
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DBVeterinaria", throwIfV1Schema: false)
        {
        }
        

        //DbSet<Cita> Citas { get; set; }
        //DbSet<Boleta> Boletas { get; set; }
        //DbSet<Administrador> Administradores { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<MVeterinaria.Models.Cita> Citas { get; set; }

       

      

        public System.Data.Entity.DbSet<MVeterinaria.Models.Veterinario> Veterinarios { get; set; }

        

        

        public System.Data.Entity.DbSet<MVeterinaria.Models.Mascota> Mascotas { get; set; }

        public System.Data.Entity.DbSet<MVeterinaria.Models.Raza> Razas { get; set; }

        public System.Data.Entity.DbSet<MVeterinaria.Models.Sexo> Sexos{ get; set; }
    }
}