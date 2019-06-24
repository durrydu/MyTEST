using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.CapitalFlowManage
{
    public class CapitalFlowManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CapitalFlowManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CapitalFlowManage_default",
                "CapitalFlowManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}