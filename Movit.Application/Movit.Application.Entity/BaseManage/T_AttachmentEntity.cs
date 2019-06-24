using System;
using Movit.Application.Code;
using SqlSugar;

namespace Movit.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2018 盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-06-03 18:38
    /// 描 述：T_Attachment
    /// </summary>
    [SugarTable("T_Attachment")]
    public class T_AttachmentEntity
    {
        #region 实体成员
        /// <summary>
        /// AttachmentID
        /// </summary>
        /// <returns></returns>
          [SugarColumn(IsPrimaryKey = true)]
        public string AttachmentID { get; set; }
        /// <summary>
        /// Path
        /// </summary>
        /// <returns></returns>
        public string Path { get; set; }
        /// <summary>
        /// AttachmentName
        /// </summary>
        /// <returns></returns>
        public string AttachmentName { get; set; }
        /// <summary>
        /// Extansion
        /// </summary>
        /// <returns></returns>
        public string Extansion { get; set; }
        /// <summary>
        /// ObjectID
        /// </summary>
        /// <returns></returns>
        public string ObjectID { get; set; }
        /// <summary>
        /// ObjectType
        /// </summary>
        /// <returns></returns>
        public string ObjectType { get; set; }
        /// <summary>
        /// DeleteMark
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CreateUserId
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// ModifyUserId
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// ModifyUserName
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }

        public string FileType { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {

            this.AttachmentID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.DeleteMark = 0;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.AttachmentID = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }

    public class FileModel
    {
        /// <summary>
        /// 错误代码 false:表示错误 true:表示成功
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 文件路径: Upload/file/2018-06-03/如果在保存时获取单号一定要用事物.png
        /// </summary>
        public string filepath { get; set; }
        /// <summary>
        /// 文件明:如果在保存时获取单号一定要用事物.png
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 附件主键
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 表单分组类型
        /// </summary>
        public string ObjectType { get; set; }
    }
}