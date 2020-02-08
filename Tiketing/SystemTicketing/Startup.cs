using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SystemTicketing.Startup))]
namespace SystemTicketing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
