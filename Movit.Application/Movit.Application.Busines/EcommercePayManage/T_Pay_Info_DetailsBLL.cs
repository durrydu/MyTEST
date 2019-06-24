using Movit.Application.Entity;
using Movit.Application.IService;
using Movit.Application.Service;
using Movit.Util.WebControl;
using System.Collections.Generic;
using System;
using Movit.Sys.Api.Code.Entity;
using System.Linq;

namespace Movit.Application.Busines
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-06-25 19:32
    /// 描 述：T_Pay_Info_Details
    /// </summary>
    public class T_Pay_Info_DetailsBLL
    {
        private T_Pay_Info_DetailsIService service = new T_Pay_Info_DetailsService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<T_Pay_Info_DetailsEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_Pay_Info_DetailsEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        #region 共享接口
        /// <summary>
        /// 描述：通过付款单编号获取信息
        /// 作者:姚栋
        /// 日期:20180625
        /// </summary>
        /// <param name="pay_info_code">付款单编号</param>
        /// <returns></returns>
        public List<OutPayInfoDetails> GetBillingDetails(string pay_info_code)
        {
            var result = service.GetBillingDetails(pay_info_code).Select(p => new OutPayInfoDetails
            {

                electricity_supplier_code = p.Electricity_Supplier_Code,
                amount = p.Amount,
                createtime = p.Createtime,
                details_name = p.Details_Name,
                electricity_supplier_id = p.Electricity_Supplier_Id,
                electricity_supplier_name = p.Electricity_Supplier_Name,
                pay_info_code = p.Pay_Info_Code,
                pay_info_id = p.Pay_Info_ID,
                project_code = p.Project_Code,
                project_id = p.Project_ID,
                project_name = p.Project_Name

            }).ToList();

            return result;


        }
        #endregion
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, T_Pay_Info_DetailsEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}