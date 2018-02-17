using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebInventory.Startup))]

namespace WebInventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
