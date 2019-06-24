using Movit.Application.Code;
using SqlSugar;
using System;

namespace Movit.Application.Entity.SystemManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.12.21 16:19
    /// 描 述：编号规则格式
    /// </summary>
    [SugarTable("Base_CodeRuleFormat")]
    public class CodeRuleFormatEntity  
    {
        #region 实体成员
        /// <summary>
        /// 编号规则格式主键
        /// </summary>		
        [SugarColumn(IsPrimaryKey = true)]
        public string RuleFormatId { get; set; }
        /// <summary>
        /// 编码规则主键
        /// </summary>		
        public string RuleId { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>		
        public int? ItemType { get; set; }
        /// <summary>
        /// 项目类型名称
        /// </summary>		
        public string ItemTypeName { get; set; }
        /// <summary>
        /// 格式化字符串
        /// </summary>		
        public string FormatStr { get; set; }
        /// <summary>
        /// 步长
        /// </summary>		
        public int? StepValue { get; set; }
        /// <summary>
        /// 初始值
        /// </summary>		
        public int? InitValue { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string Description { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.RuleFormatId = Guid.NewGuid().ToString();
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {

        }
        #endregion
    }
}