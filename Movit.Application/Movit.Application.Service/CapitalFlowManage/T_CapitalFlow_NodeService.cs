using Movit.Application.Entity.CapitalFlow;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;
using Movit.Application.IService.CapitalFlow;
using Movit.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movit.Util;
using Movit.Util.Extension;
using System.Data.Common;
using Movit.Data;

namespace Movit.Application.Service.CapitalFlow
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 10:58
    /// 描 述：T_CapitalFlow_Node
    /// </summary>
    public class T_CapitalFlow_NodeService : RepositoryFactory, T_CapitalFlow_NodeIService
    {
        private T_CapitalFlowIService t_capservice = new T_CapitalFlowService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<T_CapitalFlow_NodeEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable<T_CapitalFlow_NodeEntity>().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_CapitalFlow_NodeEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<T_CapitalFlow_NodeEntity>(keyValue);
        }
        public IEnumerable<CapitalFlow_CFNodeView> GetEntityList(string keyValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.[CapitalFlow_Details_Id]
                         , a.[Company_Id]
                         , a.[EcommerceID]
                         , ecom.[EcommerceName] as EcommerceName 
                         , a.[EcommerceGroupID]
                         , a.[CapitalFlow_Id]
                         , a.[ProjectID]
                         , a.[EcommerceProjectRelationID]
                         , a.[ProjectName]
                         , a.[IncomeAmount]
                         , a.[ClearingAmount]
                         , a.[Proportion]
                         , a.[PlatformExpensesAmount]
                         , a.[CapitalPoolAdd]
                         , a.[DeleteMark]
                         , a.[CreateDate]
                         , a.[CreateUserId]
                         , a.[CreateUserName]
                         , a.[ModifyDate]
                         , a.[ModifyUserId]
                         , a.[ModifyUserName]
                         , a.[Year]
                         , a.[Month]
                         , a.[UploadDate],b.EcommerceGroupName as EcommerceGroupName,c.fullname as FullName  from T_CapitalFlow_Node
                         a inner join Base_Department c  
                         on a.Company_Id=c.DepartmentId
                         inner join T_Ecommerce ecom on ecom.EcommerceID=a.EcommerceID
                         inner join T_EcommerceGroup b on b.EcommerceGroupID=a.EcommerceGroupID  where a.DeleteMark='0'  ");
            if (!string.IsNullOrEmpty(keyValue))
            {
                strSql.Append(" and a.CapitalFlow_Id='" + keyValue + "' ");

            }
            strSql.Append(" order by a.ProjectName");
            return this.BaseRepository().FindList<CapitalFlow_CFNodeView>(strSql.ToString());

        }
        /// <summary>
        /// 描述：获取电商收入信息
        /// 作者：姚栋
        /// 日期:20180719
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CapitalFlow_CFNodeView> GetCapitalFlowView(string queryJson)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select cfn.*,cf.ApprovalState,
                cf.LatestApprovetime,
                cf.CapitalFlow_Title,
                dep.fullname as FullName
                from T_CapitalFlow_Node cfn 
                inner join T_CapitalFlow cf  
                on cfn.CapitalFlow_Id=cf.CapitalFlow_Id
                 inner join Base_Department dep 
                on cfn.Company_Id=dep.DepartmentId 
                  where cfn.DeleteMark='0'  ");
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            //查询条件
            if (!queryParam["ApprovalState"].IsEmpty())
            {
                string ApprovalState = queryParam["ApprovalState"].ToString();
                strSql.Append(" AND cf.ApprovalState = @ApprovalState ");
                parameter.Add(DbParameters.CreateDbParameter("@ApprovalState", ApprovalState));


            }
            if (!queryParam["CapitalFlow_Id"].IsEmpty())
            {

                string CapitalFlow_Id = queryParam["CapitalFlow_Id"].ToString();
                strSql.Append(" AND cf.CapitalFlow_Id = @CapitalFlow_Id ");
                parameter.Add(DbParameters.CreateDbParameter("@CapitalFlow_Id", CapitalFlow_Id));


            }
            if (!queryParam["LatestApprovetimeBegin"].IsEmpty())
            {
                string LatestApprovetimeBegin = queryParam["LatestApprovetimeBegin"].ToString();
                strSql.Append(" AND cf.LatestApprovetime>=@LatestApprovetimeBegin  ");
                parameter.Add(DbParameters.CreateDbParameter("@LatestApprovetimeBegin", LatestApprovetimeBegin));


            }
            if (!queryParam["LatestApprovetimeEnd"].IsEmpty())
            {
                string LatestApprovetimeEnd = queryParam["LatestApprovetimeEnd"].ToString();
                strSql.Append(" AND cf.LatestApprovetime<=@LatestApprovetimeEnd  ");
                parameter.Add(DbParameters.CreateDbParameter("@LatestApprovetimeEnd", LatestApprovetimeEnd));


            }
            if (!queryParam["LatestApprovetime"].IsEmpty())
            {
                string LatestApprovetime = queryParam["LatestApprovetime"].ToString();
                strSql.Append(" AND cf.LatestApprovetime=@LatestApprovetime  ");
                parameter.Add(DbParameters.CreateDbParameter("@LatestApprovetime", LatestApprovetime));


            }
            strSql.Append(" order by cfn.ProjectName");
            return this.BaseRepository().FindList<CapitalFlow_CFNodeView>(strSql.ToString());

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
                this.BaseRepository().Delete<T_CapitalFlow_NodeEntity>(keyValue);
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
        public void SaveForm(string keyValue, T_CapitalFlow_NodeEntity entity)
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
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            try {
                T_CapitalFlowEntity entity = t_capservice.GetEntity(keyValue);
                entity.DeleteMark = 1;
                this.BaseRepository().Update(entity);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
    }
}

