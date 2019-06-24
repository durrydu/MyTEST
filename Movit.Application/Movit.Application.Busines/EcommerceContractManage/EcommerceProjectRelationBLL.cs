﻿using Movit.Application.Code;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.Entity.EcommerceContractManage.ViewModel;
using Movit.Application.IService.EcommerceContractManage;
using Movit.Application.Service.EcommerceContractManage;
using Movit.Sys.Api.Code.Entity;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Busines
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：emily
    /// 日 期：2018-06-19 11:08
    /// 描 述：T_EcommerceProjectRelation
    /// </summary>
    public class EcommerceProjectRelationBLL
    {
        private IEcommerceProjectRelationService service = new EcommerceProjectRelationService();

        #region 获取数据
          /// <summary>
        /// 获取当前用户可以看到的合同信息列表
        /// 作者：姚栋
        /// 日期：20180717
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EcommerceDiscountProgramEntity> GetListByAuthorize()
        {
            return service.GetListByAuthorize();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EcommerceProjectRelationView> GetPageList(Pagination pagination, string queryJson, string urlname)
        {
            return service.GetPageList(pagination, queryJson, urlname);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyValue">查询参数</param>
        /// <param name="state">审批状态</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceProjectRelationEntity> GetList(string keyValue, int state,int istrunk)
        {
            return service.GetList(keyValue, state, istrunk);
        }
        public IEnumerable<EcommerceProjectRelationEntity> GetProjectAndEcom()
        {
            return service.GetProjectAndEcom();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceProjectRelationEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceProjectRelationEntity> GetList(string keyValue)
        {
            return service.GetList(keyValue);
        }
        /// 获取所有审批过和IsTrunk=1的项目列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceProjectRelationEntity> GetAllListByST(int state,int IsTrunk)
        {
            return service.GetAllListByST(state, IsTrunk);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EcommerceProjectRelationEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        ///<summary>
        ///作者：durry
        ///time:2018-06-29 10:06
        ///获取发起流程的url
        /// </summary>
        public IEnumerable<EcommerceProjectRelationView> GetStartUrl(string keyValue, string starturlname)
        {
            return service.GetStartUrl(keyValue, starturlname);
        }
        #region 共享接口
        /// <summary>
        /// 电商平台选择
        /// 作者:姚栋
        /// 日期:20180625
        /// </summary>
        /// <param name="login_name">当前用户登录名</param>
        /// <param name="currency_code">币种编码</param>
        /// <param name="electricity_supplier_name">电商名称</param>
        /// <param name="electricity_supplier_ad">电商简称</param>
        /// <param name="electricity_supplier_code">电商编码</param>
        /// <param name="available_balance_begin">可用余额(范围-开始)</param>
        /// <param name="available_balance_end">可用余额(范围-结束)</param>
        /// <param name="project_code">项目编码</param>
        /// <param name="project_name">项目名称</param>
        public OutOnlieModel GetOnLineMallList(string login_name, Pagination pagination, string currency_code = "CNY",
             string electricity_supplier_name = null,
             string electricity_supplier_ad = null,
            string electricity_supplier_code = null,
             decimal? available_balance_begin = 0,
             decimal? available_balance_end = 0,
             string project_code = null,
             string project_name = null)
        {
            return service.GetOnLineMallList(login_name, pagination, currency_code,
              electricity_supplier_name,
              electricity_supplier_ad,
             electricity_supplier_code,
              available_balance_begin,
             available_balance_end,
              project_code,
              project_name);

        }
        /// <summary>
        /// 资金余额检查
        /// 作者:姚栋
        /// 日期:20180625
        /// </summary>
        /// <param name="electricity_supplier_id">电商ID</param>
        /// <param name="electricity_supplier_code">电商编码</param>
        /// <param name="locked_amount">占用金额</param>
        /// <param name="project_id">项目ID</param>
        /// <param name="project_code">项目编码</param>
        /// <param name="currency_code">币种编码</param>
        /// <returns></returns>
        public bool BalanceCheck(string electricity_supplier_id, string electricity_supplier_code,
            decimal locked_amount,
            string project_id,
            string project_code,
            string currency_code = "CNY")
        {

            return service.BalanceCheck(electricity_supplier_id,
                electricity_supplier_code,
           locked_amount,
           project_id,
          project_code,
           currency_code);
        }
        #endregion
        #endregion
        public IEnumerable<EcommerceProjectRelationEntity> GetAllListByEcomNameList(string EcomNameStr)
        {
            return service.GetAllListByEcomNameList(EcomNameStr);
        }
        public EcommerceProjectRelationEntity GetTrunkEntity(string projectId,string ecomId)
        {
            return service.GetTrunkEntity(projectId, ecomId);
        }
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
        public void SaveForm(string keyValue, EcommerceProjectRelationEntity entity, List<EcommerceDiscountProgramEntity> discountList, List<FileModel> uploadFiles)
        {
            try
            {
                service.SaveForm(keyValue, entity, discountList, uploadFiles);
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
        public string GetKeyValue(string keyValue, EcommerceProjectRelationEntity entity, List<EcommerceDiscountProgramEntity> discountList, List<FileModel> uploadFiles)
        {
            try
            {
                return service.GetKeyValue(keyValue, entity, discountList, uploadFiles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void ApprovalUpdateState(string keyValue, EcommerceProjectRelationEntity entity)
        {
            try
            {
                service.ApprovalUpdateState(keyValue, entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<EcommerceProjectRelationEntity> searchActAmount(string EcommerceID, string ProjectID) {
            return service.searchActAmount(EcommerceID, ProjectID);
        }
        public void UpdateActAmount(string EcommerceID, string ProjectID, decimal FinalAmount, decimal FinalAll) {
            service.UpdateActAmount(EcommerceID, ProjectID, FinalAmount, FinalAll);
        }
        public void updateMoneyAA(List<EcommerceProjectRelationEntity> ecomList) {
            service.updateMoneyAA(ecomList);
        }
        #endregion
        /// <summary>
        /// 查询是否存在电商和项目是否存在istrunk=1
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="ecommerceid"></param>
        /// <param name="istrunk"></param>
        /// <returns></returns>
        public int GetIsTrunkCount(string projectid, string ecommerceid, int istrunk)
        {
            return service.GetIsTrunkCount(projectid, ecommerceid, istrunk);
        }

       
    }
}