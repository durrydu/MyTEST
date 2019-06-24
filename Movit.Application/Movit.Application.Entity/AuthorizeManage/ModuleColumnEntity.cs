using Movit.Application.Code;
using SqlSugar;
using System;

namespace Movit.Application.Entity.AuthorizeManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.10.29 15:13
    /// 描 述：系统视图
    /// </summary>
    [SugarTable("Base_ModuleColumn")]
    public class ModuleColumnEntity  
    {
        #region 实体成员
        /// <summary>
        /// 列主键
        /// </summary>	
        [SugarColumn(IsPrimaryKey = true)]
        public string ModuleColumnId { get; set; }
        /// <summary>
        /// 功能主键
        /// </summary>		
        public string ModuleId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public string ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>		
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>		
        public string FullName { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string Description { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public   void Create()
        {
            this.ModuleColumnId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public   void Modify(string keyValue)
        {
            this.ModuleColumnId = keyValue;
        }
        #endregion
    }
}
