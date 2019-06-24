using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Sys.Api.Code.Entity
{
    /// <summary>
    /// 描述：共享平台付款单流水
    /// 作者:姚栋
    /// 日期:20180625
    public class OutPayInfoDetails
    {
        /// <summary>
        /// 付款单ID
        /// </summary>
        public string pay_info_id { get; set; }
        /// <summary>
        /// 付款单编号
        /// </summary>

        public string pay_info_code { get; set; }
        /// <summary>
        /// 流水时间
        /// </summary>

        public DateTime createtime { get; set; }
        /// <summary>
        /// 流水类型名称
        /// </summary>

        public string details_name { get; set; }
        /// <summary>
        /// 流水金额
        /// </summary>

        public decimal amount { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string project_id { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string project_code { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string project_name { get; set; }
        /// <summary>
        /// 电商ID
        /// </summary>

        public string electricity_supplier_id { get; set; }
        /// <summary>
        /// 电商编码
        /// </summary>

        public string electricity_supplier_code { get; set; }
        /// <summary>
        /// 电商名称
        /// </summary>
        public string electricity_supplier_name { get; set; }

    }
}
