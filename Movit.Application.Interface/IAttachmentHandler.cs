using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.IO;
using Movit.Application.Entity.BaseManage;

namespace Movit.Application.Interface
{
    /// <summary>
    /// 附件处理
    /// 作者：姚栋
    /// 日期：20180628
    /// </summary>
    public interface IAttachmentHandler
    {
        /// <summary>
        /// 分块上传
        /// </summary>
        /// <param name="RequstBase"></param>
        void BreakPointTransmission(HttpRequestBase RequstBase);
        /// <summary>
        /// 上传完成
        /// </summary>
        /// <param name="RequstBase"></param>
        /// <param name="ServerBase"></param>
        /// <returns></returns>
        T_AttachmentEntity SaveAttach(HttpRequestBase RequstBase, HttpServerUtilityBase ServerBase);
        /// <summary>
        /// 删除附件，暂未实现
        /// </summary>
        /// <param name="RequstBase"></param>
        /// <param name="AttchId"></param>
        void DeleteAttach(HttpRequestBase RequstBase, string AttchId);
        /// <summary>
        /// 下载附件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="ResponseBase"></param>
        /// <param name="ServerBase"></param>
        void DownFile(string filePath, string fileName, HttpResponseBase ResponseBase, HttpServerUtilityBase ServerBase);

    }



   



}
