using Movit.Application.Busines.BaseManage;
using Movit.Application.Entity.BaseManage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BaoLi.Application.Web
{
    /// <summary>
    /// 附件默认处理类
    /// 作者：姚栋
    /// 日期：20180628
    /// </summary>
    public class DefaultAttachmentHandler : AttachmentHandlerBase
    {
        private T_AttachmentBLL attBll = new T_AttachmentBLL();
        public override void BreakPointTransmission(HttpRequestBase RequstBase)
        {
            var data = RequstBase.Files["file"];//表单中取得分块文件
            var filePath = GetAttachmentSectionUrl(RequstBase);
            data.SaveAs(filePath);

        }

        public override T_AttachmentEntity SaveAttach(HttpRequestBase RequstBase, HttpServerUtilityBase ServerBase)
        {
            try
            {
                var objectType = RequstBase["objectType"];//附件表单类型
                //保存到数据库的文件扩展名
                string extName = RequstBase["ext"];
                string fileType = RequstBase["fileType"];
                var uploadDir = ServerBase.MapPath("~/Upload/file");//Upload/file 文件夹
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
                uploadDir += "\\" + currentTime;
                var fileName = RequstBase["fileName"];//文件名
                string fileRelName = fileName.Substring(0, fileName.LastIndexOf('.'));
                fileRelName = fileRelName.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace(",", "");
                fileName = fileName.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace(",", "");
                var dir = Path.Combine(uploadDir, fileRelName);//临时文件夹            
                var files = System.IO.Directory.GetFiles(dir);//获得下面的所有文件
                var finalPath = Path.Combine(uploadDir, fileName);//最终的文件名（demo中保存的是它上传时候的文件名，实际操作肯定不能这样）
                var fs = new FileStream(finalPath, FileMode.Create);
                foreach (var part in files.OrderBy(x => x.Length).ThenBy(x => x))//排一下序，保证从0-N Write
                {
                    var bytes = System.IO.File.ReadAllBytes(part);
                    fs.Write(bytes, 0, bytes.Length);
                    bytes = null;
                    System.IO.File.Delete(part);//删除分块
                }
                fs.Flush();
                fs.Close();
                var filepath = finalPath.Substring(finalPath.IndexOf("Upload\\")).Replace(@"\", @"/");
                T_AttachmentEntity attModel = new T_AttachmentEntity()
                {

                    AttachmentName = fileName,
                    Extansion = extName,
                    ObjectType = objectType,
                    Path = filepath,
                    FileType = fileType

                };

                attBll.SaveForm("", attModel);
                System.IO.Directory.Delete(dir);//删除文件夹
                return attModel;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public override void DownFile(string filePath, string fileName, HttpResponseBase ResponseBase, HttpServerUtilityBase ServerBase)
        {
            try
            {
                filePath = ServerBase.MapPath("~/" + filePath);
                FileStream fs = new FileStream(filePath, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                ResponseBase.Charset = "UTF-8";
                ResponseBase.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                ResponseBase.ContentType = "application/octet-stream";

                ResponseBase.AddHeader("Content-Disposition", "attachment; filename=" + ServerBase.UrlEncode(fileName));
                ResponseBase.BinaryWrite(bytes);
                ResponseBase.Flush();
                ResponseBase.End();

            }
            catch (Exception ex)
            {

            }
        }
        public override void DeleteAttach(HttpRequestBase RequstBase, string AttchId)
        {
            throw new NotImplementedException();
        }


    }
}