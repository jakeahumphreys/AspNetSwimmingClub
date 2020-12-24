using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCWebAssignment1.Customisations
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    // user is logged-in, so redirecting to login page won't help, must be premium
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Area = "Index", Controller = "Error", Action = "" }));
                }
                else
                {
                    // let the base implementation redirect the user
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }
    }
}