using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestauranteWeb.Startup))]
namespace RestauranteWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
