using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERP_Project.Startup))]
namespace ERP_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
