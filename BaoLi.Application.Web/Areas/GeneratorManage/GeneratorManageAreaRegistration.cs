using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.GeneratorManage
{
    public class GeneratorManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GeneratorManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GeneratorManage_default",
                "GeneratorManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}