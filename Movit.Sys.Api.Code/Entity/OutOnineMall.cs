using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Sys.Api.Code.Entity
{
    public class OutOnlieModel
    {

        public pageInfo pageInfo { get; set; }

        public List<OutOnineMall> datalist { get; set; }
    }
   
    /// <summary>
    /// 描述：电商平台选择对象
    /// 作者:姚栋
    /// 日期:20180625
    /// </summary>
    public class OutOnineMall
    {
        public OutOnineMall()
        {

            this.currency_code = "CNY";
        }
        /// <summary>
        /// 电商ID
        /// </summary>
        public string electricity_supplier_id { get; set; }
        /// <summary>
        /// 电商名称
        /// </summary>

        public string electricity_supplier_name { get; set; }
        /// <summary>
        /// 电商编码
        /// </summary>

        public string electricity_supplier_code { get; set; }
        /// <summary>
        /// 电商简称ID
        /// </summary>

        public string electricity_supplier_ab_id { get; set; }
        /// <summary>
        /// 电商简称
        /// </summary>
        public string electricity_supplier_ad { get; set; }
        /// <summary>
        /// 可用余额
        /// </summary>
        public decimal available_balance { get; set; }
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
        /// 币种编码
        /// </summary>

        public string currency_code { get; set; }



    }
}
