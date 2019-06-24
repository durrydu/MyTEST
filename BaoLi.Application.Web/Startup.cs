using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Movit.Util.Ioc;
using Movit.Application.Interface;

[assembly: OwinStartup(typeof(BaoLi.Application.Web.Startup))]

namespace BaoLi.Application.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            //IocHelper.RegisterType<IAttachmentHandler, AttachmentTestHandler>();
        }
    }
}
