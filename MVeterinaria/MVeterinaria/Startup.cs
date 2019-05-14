using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVeterinaria.Startup))]
namespace MVeterinaria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
