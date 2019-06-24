using Movit.Application.Entity.EcommerceTransferManage;
using Movit.Application.Entity.EcommerceTransferManage.ViewModel;
using Movit.Application.IService.EcommerceTransferManage;
using Movit.Application.Service.EcommerceTransferManage;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Busines.EcommerceTransferManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-06-25 14:48
    /// 描 述：T_Transfer_Info
    /// </summary>
    public class Transfer_InfoBLL
    {
        private ITransfer_InfoService service = new Transfer_InfoService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Transfer_InfoEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Transfer_InfoEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Transfer_Project_EcommerceView GetProEcomJson(string keyValue)
        {
            return service.GetProEcomJson(keyValue);
        }
        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Transfer_Project_EcommerceView> GetPageList(string keyValue, Pagination pagination, string queryJson)
        {
            return service.GetPageListJson(keyValue, pagination, queryJson);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteRemark(string keyValue, string queryJson, string ProjectID, string Transfer_Money, string EcommerceID)
        {
            try
            {
                service.DeleteRemark(keyValue, queryJson, ProjectID, Transfer_Money, EcommerceID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 删除表单
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
        public void SaveForm(string keyValue, Transfer_InfoEntity entity, string ProjectID, string EcommerceID, string ActualControlTotalAmount, string Transfer_Code, out string errMsg)
        {
            try
            {
                service.SaveForm(keyValue, entity, ProjectID, EcommerceID, ActualControlTotalAmount,Transfer_Code, out errMsg);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}