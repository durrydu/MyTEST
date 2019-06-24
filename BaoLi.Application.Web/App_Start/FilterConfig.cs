using BaoLi.Application.Web.App_Start;
using System.Web.Mvc;

namespace BaoLi.Application.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandlerErrorAttribute());
            //filters.Add(new GZipAttribute());
        }
    }
}