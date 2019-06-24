using Movit.Application.Code;
using SqlSugar;
using System;

namespace Movit.Application.Entity.AuthorizeManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2016.04.26 09:16
    /// 描 述：系统表单实例
    /// </summary>
    [SugarTable("Base_ModuleFormInstance")]
    public class ModuleFormInstanceEntity  
    {
        #region 实体成员
        /// <summary>
        /// 表单主键
        /// </summary>
         [SugarColumn(IsPrimaryKey = true)]
        public string FormInstanceId { set; get; }
        /// <summary>
        /// 功能主键
        /// </summary>
        public string FormId { set; get; }
        /// <summary>
        /// 编码
        /// </summary>
        public string FormInstanceJson { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ObjectId { set; get; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? SortCode { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { set; get; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public   void Create()
        {
            this.FormInstanceId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public   void Modify(string keyValue)
        {
            this.FormInstanceId = keyValue;
        }
        #endregion
    }
}


