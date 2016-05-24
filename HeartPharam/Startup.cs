using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HeartPharam.Startup))]
namespace HeartPharam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
