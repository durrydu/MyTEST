using Movit.Application.Code;
using SqlSugar;
using System;

namespace Movit.Application.Entity.AuthorizeManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.08.01 14:00
    /// 描 述：系统按钮
    /// </summary>
    [SugarTable("Base_ModuleButton")]
    public class ModuleButtonEntity  
    {
        #region 实体成员
        /// <summary>
        /// 按钮主键
        /// </summary>	
          [SugarColumn(IsPrimaryKey = true)]
        public string ModuleButtonId { get; set; }
        /// <summary>
        /// 功能主键
        /// </summary>		
        public string ModuleId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public string ParentId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>		
        public string Icon { get; set; }
        /// <summary>
        /// 编码
        /// </summary>		
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>		
        public string FullName { get; set; }
        /// <summary>
        /// Action地址
        /// </summary>		
        public string ActionAddress { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public   void Create()
        {
            this.ModuleButtonId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public   void Modify(string keyValue)
        {
            this.ModuleButtonId = keyValue;
        }
        #endregion
    }
}