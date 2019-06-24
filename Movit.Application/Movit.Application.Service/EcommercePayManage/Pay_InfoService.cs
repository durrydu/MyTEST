using Movit.Application.Entity;
using Movit.Application.IService;
using Movit.Data.Repository;
using Movit.Util.Extension;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using Movit.Util;
using System.Text;
using Movit.Application.Service.MoneyManager;
using Movit.Application.Interface;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.Service.EcommerceContractManage;
using Movit.Application.Service.AuthorizeManage;
using Movit.Application.Code;

namespace Movit.Application.Service
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-06-22 09:54
    /// 描 述：Pay_Info
    /// </summary>
    public class Pay_InfoService : RepositoryFactory<Pay_InfoEntity>, IPay_InfoService
    {
        private Base_ProjectInfoService projectService = new Base_ProjectInfoService();
        private T_Pay_Info_DetailsService payDeailsService = new T_Pay_Info_DetailsService();
        private EcommerceProjectRelationService eprService = new EcommerceProjectRelationService();
        #region 获取数据
        ///<summary>
        ///作者：杜强
        ///Time:2018-06-22 13:40
        ///获取分页列表
        ///</summary>>
        public IEnumerable<Pay_InfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            sqlstr.Append(@"select *,project_id as projectid from T_Pay_Info WHERE 1=1 and
            DeleteMark='0'");
            if (!queryParam["Electricity_Supplier_Name"].IsEmpty())
            {
                sqlstr.Append(" and Electricity_Supplier_Name like '%" + queryParam["Electricity_Supplier_Name"].ToString() + "%'");
            }
            if (!queryParam["CompanyID"].IsEmpty())
            {
                sqlstr.Append(" and CompanyID='" + queryParam["CompanyID"].ToString() + "'");
            }
            if (!queryParam["ProjectID"].IsEmpty())
            {
                sqlstr.Append(" and Project_Id='" + queryParam["ProjectID"].ToString() + "'");
            }
            if (!queryParam["Pay_Createtime"].IsEmpty())
            {
                sqlstr.Append(" and CONVERT(varchar(10), Pay_Createtime, 120)>='" + queryParam["Pay_Createtime"].ToString() + "'");
            }
            if (!queryParam["Pay_EndTime"].IsEmpty())
            {
                sqlstr.Append("and CONVERT(varchar(10), Pay_Createtime, 120)<='" + queryParam["Pay_EndTime"].ToString() + "'");
            }
            //return this.BaseRepository().FindList(sqlstr.ToString(), pagination);
            return new AuthorizeService<Pay_InfoEntity>().FindList(sqlstr.ToString(), pagination, "ProjectID");
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Pay_InfoEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Pay_InfoEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["Approval_Status"].IsEmpty())
            {
                string Approval_Status = queryParam["Approval_Status"].ToString();
                expression = expression.And(t => t.Approval_Status == Approval_Status);

            }
            if (!queryParam["Pay_CompletetimeBegin"].IsEmpty())
            {
                var Pay_CompletetimeBegin = DateTime.Parse(queryParam["Pay_CompletetimeBegin"].ToString());
                expression = expression.And(t => t.Pay_Completetime >= Pay_CompletetimeBegin);

            }
            if (!queryParam["Pay_CompletetimeEnd"].IsEmpty())
            {
                var Pay_CompletetimeEnd = DateTime.Parse(queryParam["Pay_CompletetimeEnd"].ToString());
                expression = expression.And(t => t.Pay_Completetime <= Pay_CompletetimeEnd);

            }
            expression = expression.And(t => t.DeleteMark == 0);
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Pay_InfoEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
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
                this.BaseRepository().Delete(keyValue);
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
        public void SaveForm(string keyValue, Pay_InfoEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region 共享平台接口
        /// <summary>
        /// 共享平台推送付款单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void Paymentuse(string keyValue, Pay_InfoEntity entity)
        {


            try
            {

                AdjustmentAmountByApprovalStatus(entity);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 根据审批状态进行金额变更
        /// AUDITED 已审批 消费金额
        /// DRAFT 草稿 释放
        /// SUBMITED 待审批 占用（注意金额的变化，变化是需要修改）
        /// DISABLED 已作废 释放   
        /// </summary>
        /// <param name="ApprovalStatus"></param>
        public void AdjustmentAmountByApprovalStatus(Pay_InfoEntity inputPayEntity)
        {
            try
            {
               
                InterfacePay PayHelpBll = null;
                switch (inputPayEntity.Approval_Status)
                {

                    case "4": // 已审批 消费金额
                        PayHelpBll = new PayConsumptionHelp(inputPayEntity);
                        PayHelpBll.Execute();

                        break;
                    case "1": // DRAFT 草稿 释放
                        PayHelpBll = new PayUnLockHelp(inputPayEntity);
                        PayHelpBll.Execute();
                        break;
                    case "3": //SUBMITED 待审批 占用（注意金额的变化，变化是需要修改）
                        PayHelpBll = new PayLockHelp(inputPayEntity);
                        PayHelpBll.Execute();
                        break;
                    case "2":// DISABLED 已作废 释放   
                        PayHelpBll = new PayUnLockHelp(inputPayEntity);
                        PayHelpBll.Execute();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion
    }
}