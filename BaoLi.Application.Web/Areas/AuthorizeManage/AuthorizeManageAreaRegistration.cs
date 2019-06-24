using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.AuthorizeManage
{
    public class AuthorizeManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AuthorizeManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AuthorizeManage_default",
                "AuthorizeManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}