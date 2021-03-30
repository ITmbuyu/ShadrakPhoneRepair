using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShadrackPhoneRepair.Startup))]
namespace ShadrackPhoneRepair
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
