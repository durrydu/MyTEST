using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.EcommerceTransferManage
{
    public class EcommerceTransferManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EcommerceTransferManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EcommerceTransferManage_default",
                "EcommerceTransferManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}