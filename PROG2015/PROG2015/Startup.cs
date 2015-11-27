using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PROG2015.Startup))]
namespace PROG2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
