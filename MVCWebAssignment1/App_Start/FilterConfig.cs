using MVCWebAssignment1.Customisations;
using System.Web;
using System.Web.Mvc;

namespace MVCWebAssignment1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
}
