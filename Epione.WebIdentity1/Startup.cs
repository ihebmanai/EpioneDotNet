using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Epione.WebIdentity1.Startup))]
namespace Epione.WebIdentity1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
