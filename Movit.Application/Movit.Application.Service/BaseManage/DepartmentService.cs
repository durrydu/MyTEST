using Movit.Application.Code;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Entity.SystemManage;
using Movit.Application.IService.BaseManage;
using Movit.Application.Service.AuthorizeManage;
using Movit.Application.Service.SystemManage;
using Movit.Data;
using Movit.Data.Repository;
using Movit.Util.Attributes;
using Movit.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Movit.Application.Service.BaseManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人： 
    /// 日 期：2015.11.02 14:27
    /// 描 述：部门管理
    /// </summary>
    public class DepartmentService : RepositoryFactory<DepartmentEntity>, IDepartmentService
    {
        private LogService logServer = new LogService();
        #region 获取数据
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取当前用户可以看到的区域公司列表
        /// 作者：姚栋
        /// 日期：20180717
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetListByAuthorize()
        {
            StringBuilder sqlBuilder = new StringBuilder(@"
  select * from Base_Department dp where   dp.Layer = '区域公司' and dp.SourceSys = 'YGHR03'
  and dp.InnerPhone = '地产' and DeleteMark=0");
            var lookAll = new AuthorizeService<DepartmentEntity>().LookAll();
            if (!lookAll)
            {
                sqlBuilder.Clear();
                sqlBuilder.AppendFormat(@"select distinct 
                CompanyData.DepartmentId,
                CompanyData.FullName,
                CompanyData.DataStatus
                from (
                select  dp.DepartmentId,dp.FullName, userProject.ItemId,pinfo.DataStatus,
                pinfo.ProjecName,
                userProject.UserId
                 from view_post_project userProject
                inner join Base_ProjectInfo pinfo
                on userProject.ItemId=pinfo.ProjectID
                inner join  Base_Department dp
                on pinfo.CompanyCode=dp.DepartmentId 
                where userProject.UserId='{0}'
                ) CompanyData
                ", SystemInfo.CurrentUserId);
            }
            return this.BaseRepository().FindList(sqlBuilder.ToString());
        }
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetListHasUser()
        {
            return this.BaseRepository().FindList(@"With QueryName
                                AS(
                                select DepartmentId, ParentId, FullName
                                from(select * from Base_Department org
                                inner join
                                (select distinct DepartmentId as orgid
                                from Base_User where DeleteMark=0
                                ) as pjorg
                                on org.DepartmentId = pjorg.orgid) as Temp
                                where temp.DeleteMark=0
                                Union ALL
                                select A.DepartmentId,A.ParentId ,A.FullName
                                from Base_Department A, QueryName B
                                where  A.DepartmentId = B.ParentId and a.DeleteMark=0        
                                ) select distinct DepartmentId,ParentId,FullName from QueryName").ToList();
        }
        /// <summary>
        /// 部门实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DepartmentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 部门编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<DepartmentEntity>();
            expression = expression.And(t => t.EnCode == enCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.DepartmentId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 部门名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<DepartmentEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.DepartmentId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            int count = this.BaseRepository().IQueryable(t => t.ParentId == keyValue).Count();
            if (count > 0)
            {
                throw new Exception("当前所选数据有子节点数据！");
            }
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存部门表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">机构实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DepartmentEntity departmentEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                departmentEntity.Modify(keyValue);
                this.BaseRepository().Update(departmentEntity);
            }
            else
            {
                departmentEntity.Create();
                this.BaseRepository().Insert(departmentEntity);
            }
        }
        #endregion

        #region 同步数据
        /// <summary>
        /// 描述:部门新增
        /// 作者:姚栋
        /// 日期:2018.06.05
        /// </summary>
        public void SyncNewDep()
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 3;
            logEntity.OperateTypeId = ((int)OperationType.SyncData).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.SyncData);
            logEntity.OperateAccount = "WinDbSyncSerivce";
            logEntity.OperateUserId = "WinDbSyncSerivce";
            logEntity.Module = "WinDbSyncSerivce";
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append(@"insert into Base_Department(
                                DepartmentId--组织GUID
                                ,EnCode --组织编码
                                ,FullName --组织名称
                                 ,ParentId --上级组织GUID
                                 ,ParentEnCode --上级组织编码
                                 ,ParentName--上级组织名称
                                 ,Layer --组织层级
                                 ,OrgClass--组织类型
                                 ,DeleteMark                           
                                 ,CreateDate --创建时间
                                ,SyncTime
                                 ,SourceSys
                                 ,SourceID
                                 ,ManagerId --组织负责人ID
                                 ,OrganizeId                        
                                )select 
                                OrgID, --组织GUID
                                OrgCode, --组织编码
                                OrgName, --组织名称
                                 OrgParentID, --上级组织GUID
                                 OrgParentCode, --上级组织编码
                                 OrgParentName,--上级组织名称
                                 OrgLevel,--组织层级
                                 OrgClass, --组织类型
                              case when DataStatus=0 then 1 when DataStatus=1 then 0 else 0 end, 
                                CreateTime, --创建时间
                                getdate(),
                                 '同步主数据',
                                 OrgID,
                                 PrincipalID,--组织负责人ID
                                 '207fa1a9-160c-4943-a89b-8fa4db0547ce'--写死的公司ID
                                from OPENQUERY(Link_MDM_MiddleBase,'select * from MDM_MiddleBase.dbo.T_OrgUnit') as SyncOrgTable
                                where not exists(select 1 from Base_Department where Base_Department.DepartmentId=SyncOrgTable.OrgID); ");
            try
            {


                var result = this.BaseRepository().ExecuteBySql(sqlInsert.ToString());
                if (result >= 0)
                {
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.ExecuteResultJson = "同步程序SyncNewDep执行成功:" + result.ToString() + "条";
                }
                else
                {
                    //写入日志
                    logEntity.ExecuteResult = -1;
                    logEntity.ExecuteResultJson = "同步程序SyncNewDep执行失败";

                }
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = "SyncNewDep从主数据同步新增部门时出错：" + ex.Message;
                throw new Exception("SyncNewDep从主数据同步新增部门时出错：" + ex.Message);

            }
            finally
            {
                logServer.WriteLog(logEntity);
            }
        }

        /// <summary>
        /// 描述:部门更新
        /// 作者:姚栋
        /// 日期:2018.06.05
        /// </summary>
        public void SyncUpdateDep()
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 3;
            logEntity.OperateTypeId = ((int)OperationType.SyncData).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.SyncData);
            logEntity.OperateAccount = "WinDbSyncSerivce";
            logEntity.OperateUserId = "WinDbSyncSerivce";
            logEntity.Module = "WinDbSyncSerivce";
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append(@"update BaoLi_Dep set 
                                BaoLi_Dep.DepartmentId   =Sync_Dep.OrgID, --组织GUID
                                BaoLi_Dep.EnCode=Sync_Dep. OrgCode, --组织编码
                                BaoLi_Dep.FullName=Sync_Dep.OrgName, --组织名称
                                BaoLi_Dep.ParentId=Sync_Dep.OrgParentID, --上级组织GUID
                                BaoLi_Dep.ParentEnCode=Sync_Dep.OrgParentCode, --上级组织编码
                                BaoLi_Dep.ParentName=Sync_Dep.OrgParentName,--上级组织名称
                                BaoLi_Dep.Layer=Sync_Dep.OrgLevel,--组织层级
                                BaoLi_Dep.OrgClass=Sync_Dep.OrgClass, --组织类型
                                BaoLi_Dep.DeleteMark  =case when DataStatus=0 then 1 when DataStatus=1 then 0 else 0 end,  
                                BaoLi_Dep.SyncTime=getdate(),
                                BaoLi_Dep.SourceID  =Sync_Dep.OrgID,
                                BaoLi_Dep.ManagerId =Sync_Dep. PrincipalID--组织负责人ID            
                                from OPENQUERY(Link_MDM_MiddleBase,'select * from MDM_MiddleBase.dbo.T_OrgUnit') as Sync_Dep
                                inner join Base_Department  BaoLi_Dep
                                on  BaoLi_Dep.DepartmentId=Sync_Dep.OrgID
                                 and(
                                BaoLi_Dep.EnCode!=Sync_Dep. OrgCode or --组织编码
                                BaoLi_Dep.FullName!=Sync_Dep.OrgName or --组织名称
                                BaoLi_Dep.ParentId!=Sync_Dep.OrgParentID or --上级组织GUID
                                BaoLi_Dep.ParentEnCode!=Sync_Dep.OrgParentCode or--上级组织编码
                                BaoLi_Dep.ParentName!=Sync_Dep.OrgParentName or--上级组织名称
                                BaoLi_Dep.Layer!=Sync_Dep.OrgLevel or--组织层级
                                BaoLi_Dep.OrgClass!=Sync_Dep.OrgClass  or --组织类型
                                BaoLi_Dep.DeleteMark!=(case when DataStatus=0 then 1 when DataStatus=1 then 0 else 0 end) or 
                                BaoLi_Dep.SourceID !=Sync_Dep.OrgID or
                                BaoLi_Dep.ManagerId!=Sync_Dep.PrincipalID);--组织负责人ID ");
            try
            {


                var result = this.BaseRepository().ExecuteBySql(sqlInsert.ToString());
                if (result >= 0)
                {
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.ExecuteResultJson = "同步程序SyncUpdateDep执行成功:" + result.ToString() + "条";
                }
                else
                {
                    //写入日志
                    logEntity.ExecuteResult = -1;
                    logEntity.ExecuteResultJson = "同步程序SyncUpdateDep执行失败";

                }
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = "SyncUpdateDep从主数据同步更新部门时出错：" + ex.Message;
                throw new Exception("SyncUpdateDep从主数据同步更新部门时出错：" + ex.Message);

            }
            finally
            {
                logServer.WriteLog(logEntity);
            }
        }
        #endregion
    }
}
