using Owin;
using Microsoft.Owin;
using HighSpeedRail;

[assembly: OwinStartup(typeof(Startup))]
namespace HighSpeedRail
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}