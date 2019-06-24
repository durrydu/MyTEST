using Movit.Application.Busines.BaseManage;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movit.Util.Ioc;
using Movit.Application.Code;

namespace BaoLi.Application.Web.Areas.PublicInfoManage.Controllers
{
    /// <summary>
    /// 文件上传控制器
    /// 作者：姚栋
    /// 日期：20180520
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public class UploadController : MvcControllerBase
    {
        private T_AttachmentBLL attBll = new T_AttachmentBLL();

        [DependInjection(typeof(IAttachmentHandler), "附件上传实现程序")]
        public static IAttachmentHandler GetAttachmentHandler()
        {
            if (IocHelper.IsRegistered(typeof(IAttachmentHandler)))
            {
                var hander = IocHelper.Resolve<IAttachmentHandler>();
                return hander;
            }
            else
            {
                return new DefaultAttachmentHandler();
            }
        }


        //
        // GET: /BusinesManage/Upload/
        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #region 文件上传
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload()
        {
            IAttachmentHandler handler = GetAttachmentHandler();
            handler.BreakPointTransmission(Request);
            return Json(new { status = true });
        }
        /// <summary>
        /// 合并文件分片
        /// </summary>
        /// <returns></returns>
        public ActionResult Merge()
        {
            try
            {

                IAttachmentHandler handler = GetAttachmentHandler();
                var AttachModel = handler.SaveAttach(Request, Server);
                return Json(new
                {
                    status = true,
                    filepath = AttachModel.Path,
                    filename = AttachModel.AttachmentName,
                    objecttype = AttachModel.ObjectType,
                    Id = AttachModel.AttachmentID,
                });//随便返回个值，实际中根据需要返回
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult DownFile(string filePath, string fileName)
        {
            IAttachmentHandler handler = GetAttachmentHandler();
            handler.DownFile(filePath, fileName, Response, Server);
            return new EmptyResult();

        }
        #endregion
    }
}