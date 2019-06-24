
using SqlSugar;
namespace Movit.Application.Entity.SystemManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.11.24 13:32
    /// 描 述：数据库表字段
    /// </summary>
    [SugarTable("Base_DataBaseTableField")]
    public class DataBaseTableFieldEntity
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string column { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string datatype { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int? length { get; set; }
        /// <summary>
        /// 允许空
        /// </summary>
        public string isnullable { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        public string identity { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string defaults { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string remark { get; set; }
    }
}
