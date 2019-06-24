using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.EcommercePayManage
{
    public class EcommercePayManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EcommercePayManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EcommercePayManage_default",
                "EcommercePayManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}