using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.EcommerceManage
{
    public class EcommerceManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EcommerceManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EcommerceManage_default",
                "EcommerceManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}