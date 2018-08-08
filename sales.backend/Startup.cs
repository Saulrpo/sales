using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sales.backend.Startup))]
namespace sales.backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
