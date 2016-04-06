using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IPM.Startup))]
namespace IPM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
