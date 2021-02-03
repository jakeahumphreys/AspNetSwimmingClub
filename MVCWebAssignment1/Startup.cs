using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using WebGrease;

[assembly: OwinStartupAttribute(typeof(MVCWebAssignment1.Startup))]
namespace MVCWebAssignment1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigureAuth(app);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
;        }
    }
}
