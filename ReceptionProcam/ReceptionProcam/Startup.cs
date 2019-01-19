using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReceptionProcam.Startup))]
namespace ReceptionProcam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
