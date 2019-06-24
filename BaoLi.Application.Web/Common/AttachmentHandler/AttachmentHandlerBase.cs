using Movit.Application.Entity.BaseManage;
using Movit.Application.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace BaoLi.Application.Web
{
    public abstract class AttachmentHandlerBase : IAttachmentHandler
    {

        public abstract void BreakPointTransmission(HttpRequestBase RequstBase);

        public abstract T_AttachmentEntity SaveAttach(HttpRequestBase RequstBase, HttpServerUtilityBase ServerBase);

        public abstract void DeleteAttach(HttpRequestBase RequstBase, string AttchId);

        public abstract void DownFile(string filePath, string fileName, HttpResponseBase ResponseBase, HttpServerUtilityBase ServerBase);
        /// <summary>
        /// 获取分块存储路径
        /// </summary>
        /// <param name="RequstBase"></param>
        /// <returns></returns>
        protected virtual string GetAttachmentSectionUrl(HttpRequestBase RequstBase)
        {
            string fileName = RequstBase["name"];
            int lastIndex = fileName.LastIndexOf('.');
            string fileRelName = lastIndex == -1 ? fileName : fileName.Substring(0, fileName.LastIndexOf('.'));
            fileRelName = fileRelName.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace(",", "");
            int index = Convert.ToInt32(RequstBase["chunk"]);//当前分块序号
            var guid = RequstBase["guid"];//前端传来的GUID号
            var dir = RequstBase.MapPath("~/Upload/file");//文件上传目录
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
            dir += "\\" + currentTime;
            dir = Path.Combine(dir, fileRelName);//临时保存分块的目录
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
            string filePath = Path.Combine(dir, index.ToString());//分块文件名为索引名，更严谨一些可以加上是否存在的判断，防止多线程时并发冲突

            return filePath;
        }


        //protected virtual string GetAttachmentName(string fileExtension)
        //{
        //    var now = DateTime.Now;
        //    int randKey = new Random().Next(1000, 9999);

        //    if (!fileExtension.StartsWith("."))
        //    {
        //        fileExtension = string.Format(".{0}", fileExtension);
        //    }

        //    return string.Format("{0:yyyyMMddHHmmssms}{1}{2}", now, randKey, fileExtension);
        //}

        //protected virtual string GetAttachmentDirectory(BaseBizController controller)
        //{
        //    var now = DateTime.Now;
        //    string rootDirectory = controller.Server.MapPath("~/UploadFiles");
        //    if (!string.IsNullOrWhiteSpace(UploadDirectory))
        //    {
        //        rootDirectory = UploadDirectory;
        //    }

        //    string innerDirectory = string.Format("{0:yyyy-MM-dd}", now);
        //    if (controller.UserIdentity != null && controller.UserIdentity.CompanyID != null)
        //    {
        //        innerDirectory = string.Format("company_{0}\\{1:yyyy-MM-dd}", controller.UserIdentity.CompanyID, now);
        //    }

        //    string directory = Path.Combine(rootDirectory, innerDirectory);
        //    if (!Directory.Exists(directory))
        //    {
        //        Directory.CreateDirectory(directory);
        //    }

        //    return directory;
        //}







    }


}