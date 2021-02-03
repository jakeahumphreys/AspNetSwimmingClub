using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace MVCWebAssignment1
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("API Default", "api/{controller}/{id}",
                new {id = RouteParameter.Optional});
        }
    }
}