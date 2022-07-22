using System.Web;
using System.Web.Mvc;
using WebApplication1.APPLog;

namespace WebApplication1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ActionLog());
            filters.Add(new ExceptionLog());

            filters.Add(new HandleErrorAttribute());
            filters.Add(new ValidateInputAttribute(true));

        }
    }
}
