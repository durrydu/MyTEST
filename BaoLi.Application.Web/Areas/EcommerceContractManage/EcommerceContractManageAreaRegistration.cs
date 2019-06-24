using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.EcommerceContractManage
{
    public class EcommerceContractManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EcommerceContractManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EcommerceContractManage_default",
                "EcommerceContractManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}