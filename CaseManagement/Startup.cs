using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Host.SystemWeb;

[assembly: OwinStartup(typeof(CaseManagement.Startup))]
namespace CaseManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuthentication(app);
        }
    }
}
