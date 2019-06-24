using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Sys.Api.Code.Entity
{
    /// <summary>
    /// 资金占用情况
    /// 日期:20180625
    /// 作者:姚栋
    /// </summary>

    public class InputPaymentuse
    {
        /// <summary>
        /// 付款单编码
        /// </summary>
        //[RegularExpression(@"[^%&',;=?$\x22]+", ErrorMessage = "付款单编码不能有特殊字符")]
        [Required(ErrorMessage = "pay_info_code付款单编码不能为空!")]
        [StringLength(25, ErrorMessage = "付款单编码长度过长")]
        public string pay_info_code { get; set; }

        /// <summary>
        /// 付款单ID
        /// </summary>
        [Required(ErrorMessage = "pay_info_id付款单ID不能为空!")]
        [StringLength(50, ErrorMessage = "付款单ID长度过长")]
        public string pay_info_id { get; set; }
        /// <summary>
        /// 电商ID
        /// </summary>
        [Required(ErrorMessage = "electricity_supplier_id电商ID不能为空!")]
        [StringLength(50, ErrorMessage = "电商ID长度过长")]
        public string electricity_supplier_id { get; set; }
        /// <summary>
        /// 电商名称
        /// </summary>
        [Required(ErrorMessage = "electricity_supplier_name电商名称不能为空!")]
        [StringLength(50, ErrorMessage = "电商名称长度过长")]
        public string electricity_supplier_name { get; set; }
        /// <summary>
        /// 电商编码
        /// </summary>
        [Required(ErrorMessage = "electricity_supplier_code电商编码不能为空!")]
        [StringLength(50, ErrorMessage = "电商编码长度过长")]
        public string electricity_supplier_code { get; set; }
        /// <summary>
        /// 电商简称名称
        /// </summary>
        [Required(ErrorMessage = " electricity_supplier_ad电商简称名称不能为空!")]
        [StringLength(50, ErrorMessage = " 电商简称名称长度过长")]
        public string electricity_supplier_ad { get; set; }
        /// <summary>
        /// 电商简称ID
        /// </summary>
        [Required(ErrorMessage = " electricity_supplier_ad_id电商简称ID不能为空!")]
        [StringLength(50, ErrorMessage = " 电商简称ID长度过长")]
        public string electricity_supplier_ad_id { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        [Required(ErrorMessage = "project_code项目编码不能为空!")]
        [StringLength(50, ErrorMessage = "项目编码长度过长")]
        public string project_code { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        [Required(ErrorMessage = "project_id项目ID不能为空!")]
        [StringLength(50, ErrorMessage = "项目ID长度过长")]
        public string project_id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(ErrorMessage = "project_name项目名称不能为空!")]
        [StringLength(50, ErrorMessage = "项目名称长度过长")]
        public string project_name { get; set; }
        /// <summary>
        /// 付款事由
        /// </summary>
        [Required(ErrorMessage = "pay_reason付款事由不能为空!")]
        [StringLength(1000, ErrorMessage = "付款事由长度过长")]
        public string pay_reason { get; set; }
        /// <summary>
        /// 付款单类型编码
        /// EC:凭票付款；LM：预付款；PA：余票付款；
        /// </summary>
        [Required(ErrorMessage = "pay_info_type付款单类型编码不能为空!")]
        [StringLength(50, ErrorMessage = "付款单类型编码长度过长")]
        public string pay_info_type { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        //[Required(ErrorMessage = "contract_name合同名称不能为空!")]
        [StringLength(160, ErrorMessage = "合同名称长度过长")]
        public string contract_name { get; set; }
        /// <summary>
        /// 合同编码
        /// </summary>
        //[Required(ErrorMessage = " 合同编码不能为空!")]
        [StringLength(50, ErrorMessage = " 合同编码长度过长")]
        public string contract_code { get; set; }
        /// <summary>
        /// 付款金额(￥)
        /// </summary>
        [DefaultValue(0)]
        [Required(ErrorMessage = "付款金额(￥)不能为空!")]
        public decimal pay_money { get; set; }
        /// <summary>
        /// 支付流程发起时间
        /// </summary>
       
        [Required(ErrorMessage = "支付流程发起时间不能为空!")]
        public DateTime pay_createtime { get; set; }
        /// <summary>
        /// 支付流程审批通过时间
        /// </summary>
       
        public DateTime? pay_completetime { get; set; }
        /// <summary>
        /// 单据Url
        /// </summary>
        [Required(ErrorMessage = "单据Url不能为空!")]
        public string url { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        [Required(ErrorMessage = "经办人姓名不能为空!")]
        public string login_name { get; set; }
        /// <summary>
        /// 经办人登录帐号
        /// </summary>
        [Required(ErrorMessage = "经办人登录帐号不能为空!")]
        public string login_code { get; set; }
        /// <summary>
        /// 审核状态
        /// 根据状态进行占用、释放、消费
        ///草稿：释放
        ///待审批：占用（注意金额的变化，变化是需要修改）
        ///已审批：消费金额
        ///作废：释放
        /// </summary>
        [Required(ErrorMessage = " 审核状态不能为空!")]
        public string approval_status { get; set; }

    }
}
