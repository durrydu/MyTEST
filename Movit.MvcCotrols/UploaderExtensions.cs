using Movit.Application.Busines.BaseManage;
using Movit.Application.Entity.BaseManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Movit.MvcCotrols
{
    /// <summary>
    /// 作者:姚栋
    /// 日期:2019.6.3
    /// 描述:上传控件
    /// </summary>
    public static class UploaderExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="name">控件id</param>
        /// <param name="selectFileButtonText">选择文件按钮文字</param>
        /// <param name="stratUpFileButtonText">开始上传按钮文字</param>
        ///  <param name="objectType">表单类型</param>
        /// <param name="multiple">是否支持多文件</param>
        /// <param name="allowedFileExtensions">允许上传的格式 如"png,jpg,pdf"</param>
        /// <param name="fileCount">最大文件数</param>
        /// <param name="fileSize">最大单文件大小,以KB为单位</param>
        /// <param name="selectEnvet">选中触发的事件</param>
        ///// <param name="uploadedEnvet">上传后触发的事件</param>
        /// <param name="readOnly">上传后触发的事件</param>
        /// <returns></returns>
        public static MvcHtmlString MovitUploader(this HtmlHelper html,
            string name,
            string objectType,
            string selectFileButtonText = "选择文件",
            string stratUpFileButtonText = "开始上传",
            bool multiple = true,
            string keyValue = null,
            int fileCount = 100,
            long fileSize = 10240,
            string allowedFileExtensions = "gif,jpg,jpeg,bmp,png,doc,docx,xls,xlsx,pdf,rar,zip",
            string selectEnvet = null,
            bool readOnly = false,
            string uploadedEnvet = null,
            bool isSingel = false,
            bool duplicate = true
            )
        {
            UploaderModel model = new UploaderModel();
            model.name = name;
            model.multiple = multiple == true ? 1 : 0;
            model.selectFileButtonText = selectFileButtonText;
            model.stratUpFileButtonText = stratUpFileButtonText;
            model.objectType = objectType;
            model.fileCount = fileCount;
            model.allowedFileExtensions = allowedFileExtensions;
            model.fileSize = fileSize * 1024;
            model.selectEnvet = selectEnvet;
            model.uploadedEnvet = uploadedEnvet;
            model.duplicate = duplicate;
            model.keyValue = keyValue;
            if (!string.IsNullOrEmpty(keyValue))
            {
                T_AttachmentBLL attBll = new T_AttachmentBLL();
                model.attachments = attBll.GetFormList(keyValue, objectType);
            }
            else
            {
                model.attachments = new List<T_AttachmentEntity>();
            }
            if (isSingel)
            {
                html.RenderPartial("~/Views/ControlScripts/_UploaderSingleScripts.cshtml", model);
            }
            else
            {
                if (readOnly)
                {
                    html.RenderPartial("~/Views/ControlScripts/_UploaderReadonlyScripts.cshtml", model);
                }
                else
                {
                    html.RenderPartial("~/Views/ControlScripts/_UploaderScripts.cshtml", model);
                }
            }


            return MvcHtmlString.Create(string.Empty);
        }
    }



    public class UploaderModel
    {
        public bool duplicate { get; set; }
        public string keyValue { get; set; }
        public string name { get; set; }
        /// <summary>
        /// 选择上传的按钮文字：默认是"选择文件"
        /// </summary>
        public string selectFileButtonText { get; set; }

        /// <summary>
        /// 开始上传按钮文字：默认是"开始上传"
        /// </summary>
        public string stratUpFileButtonText { get; set; }
        /// <summary>
        /// 多选模式 0:单选 1：多选
        /// </summary>
        public int multiple { get; set; }
        /// <summary>
        /// 文件数量
        /// </summary>
        public int fileCount { get; set; }
        /// <summary>
        /// KB为单位
        /// </summary>
        public long fileSize { get; set; }
        /// <summary>
        /// 允许上传文件的格式 示例"jpg,png,text"
        /// </summary>
        public string allowedFileExtensions { get; set; }
        /// <summary>
        /// 选中后需要出发的事件
        /// </summary>
        public string selectEnvet { get; set; }
        /// <summary>
        /// 上传完成后的事件
        /// </summary>
        public string uploadedEnvet { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string objectType { get; set; }
        /// <summary>
        /// 需要显示的附件
        /// </summary>
        public List<T_AttachmentEntity> attachments { get; set; }
        /// <summary>

        /// <summary>
        /// 允许上传文件的格式for渲染页面
        /// </summary>
        public string[] allowedFileExtensionsArray
        {
            get
            {
                if (!string.IsNullOrEmpty(allowedFileExtensions))
                    return allowedFileExtensions.Split(',');
                else
                    return new string[] { };
            }
        }
    }
}
