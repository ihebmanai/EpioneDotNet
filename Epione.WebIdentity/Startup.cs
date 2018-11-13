using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Epione.WebIdentity.Startup))]
namespace Epione.WebIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
