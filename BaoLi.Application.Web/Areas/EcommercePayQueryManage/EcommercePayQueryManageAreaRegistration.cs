using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.EcommercePayQueryManage
{
    public class EcommercePayQueryManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EcommercePayQueryManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EcommercePayQueryManage_default",
                "EcommercePayQueryManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}