using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCWebAssignment1.Startup))]
namespace MVCWebAssignment1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
