using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.PublicInfoManage
{
    public class PublicInfoManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PublicInfoManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PublicInfoManage_default",
                "PublicInfoManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}