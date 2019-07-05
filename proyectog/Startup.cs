using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoG.Startup))]
namespace ProyectoG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
