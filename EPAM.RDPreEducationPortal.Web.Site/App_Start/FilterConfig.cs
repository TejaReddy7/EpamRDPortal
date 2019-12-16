using EPAM.RDPreEducationPortal.Web.Site.Handlers;
using System.Web;
using System.Web.Mvc;

namespace EPAM.RDPreEducationPortal.Web.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomExceptionHandler());
        }
    }
}
