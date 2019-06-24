using Movit.Application.Code;
using Movit.Application.Entity;
using Movit.Application.Entity.AuthorizeManage;
using Movit.Application.Entity.SystemManage;
using Movit.Application.IService;
using Movit.Application.Service.SystemManage;
using Movit.Data.Repository;
using Movit.Util.Attributes;
using Movit.Util.Extension;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Movit.Util;
using Movit.Application.Entity.BaseManage.ViewModel;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.Service.AuthorizeManage;

namespace Movit.Application.Service
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：姚栋
    /// 日 期：2018-05-30 13:49
    /// 描 述：Base_ProjectInfo
    /// </summary>
    public class Base_ProjectInfoService : RepositoryFactory, Base_ProjectInfoIService
    {
        private LogService logServer = new LogService();
        #region 获取数据
        ///<summary>
        ///作者：durry
        ///time：2018-06-22 11:20
        ///获取区域公司下拉
        /// </summary>
        public IEnumerable<Base_ProjectInfoEntity> GetCompanyName(string queryJson)
        {
            var expression = LinqExtensions.True<Base_ProjectInfoEntity>();
            var quertParam = queryJson.ToJObject();
            expression = expression.And(t => t.DataStatus == 0);
            if (!quertParam["ProjectID"].IsEmpty())
            {
                string projectid = quertParam["ProjectID"].ToString();
                expression = expression.And(t => t.ProjectID == projectid);
            }
            return this.BaseRepository().FindList(expression);
        }
        /// <summary>
        /// 获取当前用户可以看到的项目列表
        /// 作者：姚栋
        /// 日期：20180717
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Base_ProjectInfoEntity> GetListByCompanyid(string queryJson)
        {
            //string compaynid = string.Empty;
            //if (companyids.Count() == 1)
            //{
            //    compaynid += companyids[0].ToString();
            //}
            //else
            //{
            //    for (int i = 0; i < companyids.Count(); i++)
            //    {
            //        compaynid += "'" + companyids[i].ToString() + "',";
            //    }
            //    compaynid = compaynid.Substring(1);
            //    compaynid = compaynid.Substring(0, compaynid.Length - 2);
            //}
            StringBuilder sqlBuilder = new StringBuilder("select * from Base_ProjectInfo");
            var lookAll = new AuthorizeService<Base_ProjectInfoEntity>().LookAll();
            if (!lookAll)
            {
                sqlBuilder.Clear();
                sqlBuilder.AppendFormat(@"select distinct * from (
                    select  pinfo.ProjectID,pinfo.ProjecName,pinfo.DataStatus, pinfo.CompanyCode,pinfo.CompanyId,pinfo.CompanyName 
                     from view_post_project userProject
                    inner join Base_ProjectInfo pinfo
                    on userProject.ItemId=pinfo.ProjectID
                    where userProject.UserId='{0}'", SystemInfo.CurrentUserId);
                if (queryJson != "" && queryJson != null)
                {
                    sqlBuilder.AppendFormat(@" and pinfo.CompanyCode='{0}'", queryJson);
                }
                sqlBuilder.Append(@") Project");
            }
            else
            {
                if (queryJson != "" && queryJson != null)
                {
                    sqlBuilder.AppendFormat(@" where CompanyCode='{0}'", queryJson);
                }
            }
            return this.BaseRepository().FindList<Base_ProjectInfoEntity>(sqlBuilder.ToString());
        }
        /// <summary>
        /// 获取当前用户可以看到的项目列表
        /// 作者：姚栋
        /// 日期：20180717
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Base_ProjectInfoEntity> GetListByAuthorize(string queryJson)
        {
            StringBuilder sqlBuilder = new StringBuilder("select * from Base_ProjectInfo");
            var lookAll = new AuthorizeService<Base_ProjectInfoEntity>().LookAll();
            if (!lookAll)
            {
                sqlBuilder.Clear();
                sqlBuilder.AppendFormat(@"select distinct * from (
                    select  pinfo.ProjectID,pinfo.ProjecName,pinfo.DataStatus, pinfo.CompanyCode,pinfo.CompanyId,pinfo.CompanyName 
                     from view_post_project userProject
                    inner join Base_ProjectInfo pinfo
                    on userProject.ItemId=pinfo.ProjectID
                    where userProject.UserId='{0}'", SystemInfo.CurrentUserId);
                if (queryJson != "" && queryJson != null)
                {
                    sqlBuilder.AppendFormat(@" and pinfo.CompanyCode='{0}'", queryJson);
                }
                sqlBuilder.Append(@") Project");
            }
            else
            {
                if (queryJson != "" && queryJson != null)
                {
                    sqlBuilder.AppendFormat(@" where CompanyCode='{0}'", queryJson);
                }
            }
            return this.BaseRepository().FindList<Base_ProjectInfoEntity>(sqlBuilder.ToString());
        }
        ///<summary>
        ///作者：clare
        ///time：2018-06-22 11:20
        ///获取电商名称
        /// </summary>
        public IEnumerable<EcommerceProjectRelationEntity> GetEcomGroupNameJson(string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append(@"select b.EcommerceName as EcommerceName,b.EcommerceID  as EcommerceID from Base_ProjectInfo  a left join T_EcommerceProjectRelation b on a.ProjectID= b.ProjectID where 1=1 and b.ApprovalState='4' ");
            if (!queryJson.IsEmpty())
            {
                sqlstr.Append("and b.ProjectID ='" + queryJson.ToString() + "'");
            }
            return this.BaseRepository().FindList<EcommerceProjectRelationEntity>(sqlstr.ToString());
        }
        ///<summary>
        ///作者：clare
        ///time：2018-06-22 11:20
        ///获取集团名称
        /// </summary>
        public IEnumerable<EcommerceProjectRelationEntity> GetEcomGroupNameByEconmJson(string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append(@"select EcommerceGroupName from T_EcommerceProjectRelation where 1=1  ");
            if (!queryJson.IsEmpty())
            {
                sqlstr.Append("and  EcommerceID='" + queryJson.ToString() + "'");
            }
            sqlstr.Append("group by EcommerceGroupName");
            return this.BaseRepository().FindList<EcommerceProjectRelationEntity>(sqlstr.ToString());
        }
        ///<summary>
        ///作者：clare
        ///time：2018-06-22 11:20
        ///获取资金
        /// </summary>
        public IEnumerable<EcommerceProjectRelationEntity> GetMoneyByEconmProjectJson(string queryJson, string queryValue)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append(@"select b.ActualControlTotalAmount as ActualControlTotalAmount,b.EcommerceGroupID,b.CompanyId as CompanyId,b.ControlTotalAmount as ControlTotalAmount ,b.EcommerceProjectRelationID as EcommerceProjectRelationID from Base_ProjectInfo  a left join T_EcommerceProjectRelation b on a.ProjectID= b.ProjectID where 1=1 and b.ApprovalState='4'  ");
            if (!queryJson.IsEmpty())
            {
                sqlstr.Append("and b.ProjectID ='" + queryJson.ToString() + "'");
            }
            if (!queryValue.IsEmpty())
            {
                sqlstr.Append("and b.EcommerceID ='" + queryValue.ToString() + "'");
            }
            return this.BaseRepository().FindList<EcommerceProjectRelationEntity>(sqlstr.ToString());
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="pagintion">分页</param>>
        /// <returns>返回列表Json</returns>
        public IEnumerable<Base_ProjectInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Base_ProjectInfoEntity>();
            var quertParam = queryJson.ToJObject();
            expression = expression.And(t => t.DataStatus == 0);
            if (!quertParam["CompanyName"].IsEmpty())
            {
                string companyName = quertParam["CompanyName"].ToString();
                expression = expression.And(t => t.CompanyName.Contains(companyName));
            }
            if (!quertParam["ProjecName"].IsEmpty())
            {
                string projecName = quertParam["ProjecName"].ToString();
                expression = expression.And(t => t.ProjecName.Contains(projecName));
            }
            //List<Base_ProjectInfoEntity> projectlist = GetListByAuthorize(null).ToList();
            //List<string> ProjectArray = new List<string>();
            //foreach (Base_ProjectInfoEntity item in projectlist)
            //{
            //    ProjectArray.Add(item.ProjectID);
            //}
            //if (ProjectArray.Count > 0)
            //{
            //    //expression = expression.And(t => t.ProjectID.Contains(projectids));
            //    expression = expression.And(t => ProjectArray.Contains(t.ProjectID));
            //}
            //return this.BaseRepository().FindList<Base_ProjectInfoEntity>(expression, pagination);
            return new AuthorizeService<Base_ProjectInfoEntity>().FindList(expression, pagination, "ProjectID");
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="CompanyName">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Base_ProjectInfoEntity> GetList(string keyValue)
        {
            var expression = LinqExtensions.True<Base_ProjectInfoEntity>();
            expression = expression.And(t => t.DataStatus == 0);
            if (!keyValue.IsEmpty())
            {
                expression = expression.And(t => t.CompanyCode == keyValue);
            }
            return this.BaseRepository().IQueryable(expression);
        }
        /// <summary>
        /// 描述:根据岗位ID获取已经授权过的项目信息
        /// 作者:姚栋
        /// 日期:2018-05-30
        /// </summary>
        /// <param name="keyValue">角色ID</param>
        public IEnumerable<Base_ProjectInfoEntity> GetProjectListJsonByPostId(string keyValue)
        {
            return this.BaseRepository().IQueryable<Base_ProjectInfoEntity>().ToList();

            var expression = LinqExtensions.True<Base_ProjectInfoEntity>();
            expression = expression.And(t => t.DataStatus == 0).And(p => p.DataStatus == 1);
            return this.BaseRepository().FindList<Base_ProjectInfoEntity>(expression);

        }

        /// <summary>
        /// 获取Sum实际可支配总金额
        /// 作者：durry
        /// time：2018-06-21 19：30
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Project_RelationView GetEntity(string keyValue)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"  select isnull((select  SUM(ActualControlTotalAmount)
                            from T_EcommerceProjectRelation where ApprovalState=4
                            and IsTrunk=1
                            and ProjectID='" + keyValue + "') ,0) as ControlAmount,*   from Base_ProjectInfo  where ProjectID='" + keyValue + "'");


            return this.BaseRepository().FindList<Project_RelationView>(sqlstr.ToString()).FirstOrDefault();
        }

        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Base_ProjectInfoEntity GetEntityBase(string keyValue)
        {
            return this.BaseRepository().FindEntity<Base_ProjectInfoEntity>(keyValue);
        }
        public Base_ProjectInfoEntity GetAreaName(string keyValue)
        {
            return this.BaseRepository().FindEntity<Base_ProjectInfoEntity>(keyValue);
        }
        public IEnumerable<Base_ProjectInfoEntity> GetALL(string projectname)
        {
            var expression = LinqExtensions.True<Base_ProjectInfoEntity>();
            expression = expression.And(t => t.DataStatus == 0).And(p => p.ProjecName == projectname);
            return this.BaseRepository().FindList<Base_ProjectInfoEntity>(expression);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete<Base_ProjectInfoEntity>(keyValue);
        }


        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Base_ProjectInfoEntity entity)
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
        #endregion

        #region 同步数据
        /// <summary>
        /// 描述:项目新增
        /// 作者:姚栋
        /// 日期:2018.06.04
        /// </summary>
        public void SyncNewProject()
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 3;
            logEntity.OperateTypeId = ((int)OperationType.SyncData).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.SyncData);
            logEntity.OperateAccount = "WinDbSyncSerivce";
            logEntity.OperateUserId = "WinDbSyncSerivce";
            logEntity.Module = "WinDbSyncSerivce";
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append(@"insert into Base_ProjectInfo(
                            ProjectID, 
                            ProjectCode,
                            ProjecName, 
                            ProjectGeneralizeName, 
                            ProjectOfficialName, 
                            CompanyId, 
                            CompanyCode, 
                            CompanyName, 
                            CityID, 
                            CityCode, 
                            CityName, 
                            [Address], 
                            PrincipleMan, 
                            DataStatus,
                            SourceSys,
                            SourceID,
                            SyncTime,
                            F1,
                            F2,
                            F3
                            )select 
                            ProjectID, 
                            ProjectCode,
                            ProjectShortName,
                            ProjectGeneralizeName,
                            ProjectOfficialName, 
                            CompanyId,
                            CompanyCode,
                            CompanyName,
                            CityID, 
                            CityCode,
                            CityName, 
                            [Address], 
                            PrincipleMan,
                            case when [Status]=-1 then 1 when [Status]=1 then 0 else 0 end, 
                            '主数据同步',
                            ProjectID,
                            getdate(),
                            F1, 
                            F2,
                            F3
                            from OPENQUERY(Link_yg_mds_middle,'select * from yg_mds_middle.dbo.MDS_BPM_Project ') as SyncProjectTable
                            where not exists(select 1 from Base_ProjectInfo where Base_ProjectInfo.ProjectID=SyncProjectTable.ProjectID); ");
            try
            {


                var result = this.BaseRepository().ExecuteBySql(sqlInsert.ToString());
                if (result >= 0)
                {
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.ExecuteResultJson = "同步程序SyncNewProject执行成功:" + result.ToString() + "条";
                }
                else
                {
                    //写入日志
                    logEntity.ExecuteResult = -1;
                    logEntity.ExecuteResultJson = "同步程序SyncNewProject执行失败";

                }
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = "SyncNewProject从主数据同步新增项目时出错：" + ex.Message;
                throw new Exception("SyncNewProject从主数据同步新增项目时出错：" + ex.Message);

            }
            finally
            {
                logServer.WriteLog(logEntity);
            }
        }

        /// <summary>
        /// 描述:项目更新
        /// 作者:姚栋
        /// 日期:2018.06.04
        /// </summary>
        public void SyncUpdateProject()
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 3;
            logEntity.OperateTypeId = ((int)OperationType.SyncData).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.SyncData);
            logEntity.OperateAccount = "WinDbSyncSerivce";
            logEntity.OperateUserId = "WinDbSyncSerivce";
            logEntity.Module = "WinDbSyncSerivce";

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append(@"update BaoLi_ProjectInfo set 
                                BaoLi_ProjectInfo.ProjectID=SyncProjectTable.ProjectID, 
                                BaoLi_ProjectInfo.ProjectCode=SyncProjectTable.ProjectCode,
                                BaoLi_ProjectInfo.ProjecName=SyncProjectTable.ProjectShortName,
                                BaoLi_ProjectInfo.ProjectGeneralizeName=SyncProjectTable.ProjectGeneralizeName,
                                BaoLi_ProjectInfo.ProjectOfficialName=SyncProjectTable.ProjectOfficialName,  
                                BaoLi_ProjectInfo.CompanyId=SyncProjectTable.CompanyId,
                                BaoLi_ProjectInfo.CompanyCode=SyncProjectTable.CompanyCode,
                                BaoLi_ProjectInfo.CompanyName=SyncProjectTable.CompanyName,
                                BaoLi_ProjectInfo.CityID=SyncProjectTable.CityID,
                                BaoLi_ProjectInfo.CityCode=SyncProjectTable.CityCode,
                                BaoLi_ProjectInfo.CityName=SyncProjectTable.CityName, 
                                BaoLi_ProjectInfo.[Address]=SyncProjectTable.[Address],  
                                BaoLi_ProjectInfo.PrincipleMan=SyncProjectTable.PrincipleMan,
                                BaoLi_ProjectInfo.DataStatus=(case when SyncProjectTable.[Status]=-1 then 1 when SyncProjectTable.[Status]=1 then 0 else 0 end), 
                                BaoLi_ProjectInfo.SourceSys='主数据同步',
                                BaoLi_ProjectInfo.SourceID=SyncProjectTable.ProjectID,
                                BaoLi_ProjectInfo.SyncTime=getdate()
                                from OPENQUERY(Link_yg_mds_middle,'select * from yg_mds_middle.dbo.MDS_BPM_Project')  SyncProjectTable 
                                inner join Base_ProjectInfo  BaoLi_ProjectInfo
                                on   BaoLi_ProjectInfo.ProjectID=SyncProjectTable.ProjectID
                                and( BaoLi_ProjectInfo.ProjectCode!=SyncProjectTable.ProjectCode or
                                BaoLi_ProjectInfo.ProjecName!=SyncProjectTable.ProjectShortName or
                                BaoLi_ProjectInfo.ProjectGeneralizeName!=SyncProjectTable.ProjectGeneralizeName or
                                BaoLi_ProjectInfo.ProjectOfficialName!=SyncProjectTable.ProjectOfficialName or  
                                BaoLi_ProjectInfo.CompanyId!=SyncProjectTable.CompanyId or
                                BaoLi_ProjectInfo.CompanyCode!=SyncProjectTable.CompanyCode or
                                BaoLi_ProjectInfo.CompanyName!=SyncProjectTable.CompanyName or
                                BaoLi_ProjectInfo.CityID!=SyncProjectTable.CityID or
                                BaoLi_ProjectInfo.CityCode!=SyncProjectTable.CityCode or
                                BaoLi_ProjectInfo.CityName!=SyncProjectTable.CityName or 
                                BaoLi_ProjectInfo.[Address]!=SyncProjectTable.[Address] or  
                                BaoLi_ProjectInfo.PrincipleMan!=SyncProjectTable.PrincipleMan or
                                BaoLi_ProjectInfo.DataStatus!=(case when SyncProjectTable.[Status]=-1 then 1 when SyncProjectTable.[Status]=1 then 0 else 0 end))");
            try
            {


                var result = this.BaseRepository().ExecuteBySql(sqlInsert.ToString());
                if (result >= 0)
                {
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.ExecuteResultJson = "同步程序SyncUpdateProject执行成功:" + result.ToString() + "条";
                }
                else
                {
                    //写入日志
                    logEntity.ExecuteResult = -1;
                    logEntity.ExecuteResultJson = "同步程序SyncUpdateProject执行失败";

                }
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = "SyncUpdateProject从主数据同步更新项目时出错：" + ex.Message;
                throw new Exception("SyncUpdateProject从主数据同步更新项目时出错：" + ex.Message);

            }
            finally
            {
                logServer.WriteLog(logEntity);
            }
        }

        #endregion
    }
}