using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EvaluatingTool.WEB.Startup))]

namespace EvaluatingTool.WEB
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
