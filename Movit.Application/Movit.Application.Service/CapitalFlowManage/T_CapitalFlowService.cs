using Movit.Application.Entity.CapitalFlow;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;
using Movit.Application.IService.CapitalFlow;
using Movit.Data.Repository;
using Movit.Util.WebControl;
using Movit.Data;
using Movit.Data.Repository;
using Movit.Util.WebControl;
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
using System.Data.Common;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.Service.CapitalFlowManage;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Code;
using Movit.Application.Service.BaseManage;
using Movit.Application.IService.BaseManage;
using Movit.Application.Entity.CapitalFlowManage;
using Movit.Application.Service.AuthorizeManage;
using Movit.Application.IService.AuthorizeManage;

namespace Movit.Application.Service.CapitalFlow
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 10:55
    /// 描 述：T_CapitalFlow
    /// </summary>
    public class T_CapitalFlowService : RepositoryFactory, T_CapitalFlowIService
    {
        //private IAuthorizeService<CapitalFlowViewModel> auth = new AuthorizeService<CapitalFlowViewModel>();
        private T_AttachmentIService attservice = new T_AttachmentService();
        private Base_ProjectInfoIService baseProject = new Base_ProjectInfoService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<T_CapitalFlowEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable<T_CapitalFlowEntity>().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_CapitalFlowEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<T_CapitalFlowEntity>(keyValue);
        }

        public IEnumerable<IncomeView> GetCFEntity(string keyValue)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select c.FullName as OrgName,a.Company_Id as OrgCode,a.*
                                   from T_CapitalFlow a 
                                   left join Base_Department c  on a.Company_Id=c.DepartmentId 
                                   where a.DeleteMark='0' and a.CapitalFlow_Id='" + keyValue + "'");
            return this.BaseRepository().FindList<IncomeView>(sqlstr.ToString());
        }

        public CapitalFlowViewscs GetEn(string keyValue)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@" select b.FullName as Department,* from T_CapitalFlow c  
                                   left join Base_Department b on b.DepartmentId=c.Department_Id
                                   where c.CapitalFlow_Id='" + keyValue + "'");
            IEnumerable<CapitalFlowViewscs> cc = this.BaseRepository().FindList<CapitalFlowViewscs>(sqlstr.ToString());
            return cc.First();
        }
        public IEnumerable<CapitalFlowViewModel> GetPageList(Pagination pagination, string queryJson, string urlname)
        {
            StringBuilder sqlstr = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            sqlstr.Append(@"select a.CapitalFlow_Id as CapitalFlow_Id,c.FullName as FullName,
	                               a.ApprovalState,
	                               a.Year,a.Month,
	                               a.Company_Id,
                                   a.CreateDate as CreateDate,
                                   a.Account,a.Procinstid,
	                               isnull(SUM(b.IncomeAmount),0) as IncomeAmount,
	                               isnull(SUM(b.ClearingAmount),0) as ClearingAmount,
	                               isnull(SUM(b.PlatformExpensesAmount),0) as PlatformExpensesAmount,
	                               isnull(SUM(b.CapitalPoolAdd),0) as CapitalPoolAdd  
                                   from T_CapitalFlow a 
                                   left join T_CapitalFlow_Node b on a.CapitalFlow_Id=b.CapitalFlow_Id 
                                    and b.DeleteMark=0
                                   left join Base_Department c  on a.Company_Id=c.DepartmentId 
                                   where 1=1 and a.DeleteMark='0'  ");
            if (!new AuthorizeService<CapitalFlowViewModel>().LookAll())
            {
                string query= null;
                string projectIdList="'";
                List<Base_ProjectInfoEntity> proList=baseProject.GetListByAuthorize(query).ToList();
                foreach (var item in proList) {
                    projectIdList += item.ProjectID+"','";
                }
                if (projectIdList == "'")
                {
                    projectIdList = "''";
                }
                else
                {
                    projectIdList = projectIdList.Substring(0, projectIdList.LastIndexOf(','));
                }
                sqlstr.Append("and b.ProjectID in(" + projectIdList + ") ");
               
                //string a = new AuthorizeService<CapitalFlowViewModel>().GetReadProjectId();
                //if (a != null && a!="") {
                //    string b = a.Replace(",", "','");
                //    string c = b.Substring(0, b.Length - 2);
                //    
                //}
            }
            var parameter = new List<DbParameter>();
            if (!queryParam["FullName"].IsEmpty())
            {
                sqlstr.Append("and c.FullName like '%" + queryParam["FullName"].ToString() + "%'");
                //parameter.Add(DbParameters.CreateDbParameter("@FullName", queryParam["FullName"].ToString()));
            }
            if (!queryParam["ApprovalState"].IsEmpty())
            {
                sqlstr.Append(" and a.ApprovalState = " + queryParam["ApprovalState"].ToString());

            }
            sqlstr.Append(" group by a.Year,a.Month,a.Company_Id,c.FullName,a.ApprovalState,a.CreateDate,a.CapitalFlow_Id,a.Account,a.Procinstid");
            var olddata = this.BaseRepository().FindList<CapitalFlowViewModel>(sqlstr.ToString(), pagination);
            string domainurl = urlname;
            var result = olddata.Select(p => new CapitalFlowViewModel()
            {
                Account=p.Account,
                CreateDate = p.CreateDate,
                FullName = p.FullName,
                Year = p.Year,
                CapitalFlow_Id = p.CapitalFlow_Id,
                IncomeAmount = p.IncomeAmount,
                ClearingAmount = p.ClearingAmount,
                PlatformExpensesAmount = p.PlatformExpensesAmount,
                CapitalPoolAdd = p.CapitalPoolAdd,
                ApprovalState = p.ApprovalState,
                Month = p.Month,
                Procinstid=p.Procinstid,
                url = string.Format("{0}?procInstId={1}&userid={2}&key={3}", domainurl, p.Procinstid, p.Account, BpmMD5Helper.GetEnCodeStr(p.Procinstid))
            });
            return result;
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
                this.BaseRepository().Delete<T_CapitalFlowEntity>(keyValue);
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
        public void SaveForm(string keyValue, T_CapitalFlowEntity entity)
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
        #endregion

        #region 获取权限
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public string FindPostByUser(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select AuthorizationMethod FROM [E_Commerce_DB].[dbo].[view_post_user] where UserId= '" + userId + "'");
            int a = this.BaseRepository().ExecuteBySql(strSql.ToString());
            userId = a.ToString();
            return userId;
        }
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<EcommerceProjectRelationEntity> FindProjectByUser(string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" SELECT c.EcommerceName as EcommerceName,b.ProjecName as ProjecName
            FROM [E_Commerce_DB].[dbo].[view_post_project] a 
                left join [E_Commerce_DB].[dbo].[T_EcommerceProjectRelation] b 
                on a.ItemId=b.ProjectID 
                left join T_Ecommerce c on c.EcommerceID=b.EcommerceID
                where b.ApprovalState='4' and  b.IsTrunk='1' and b.DeleteMark='0'   and UserId ='" + userId + "'");

            return this.BaseRepository().FindList<EcommerceProjectRelationEntity>(strSql.ToString());
        }
        public IEnumerable<EcommerceProjectRelationEntity> GetAllListByST()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  er.EcommerceName,epr.EcommerceProjectRelationID,epr.ProjectID,epr.ProjecName,epr.EcommerceTypeName,       epr.CooperateStartTime,epr.CooperateEndTime,epr.PlatformRate,epr.Agent,epr.ApprovalState,epr.CreateDate,epr.Procinstid,epr.Account,
                ergroup.EcommerceGroupName
                 from T_EcommerceProjectRelation epr
                 inner join T_Ecommerce er
                 on epr.EcommerceID=er.EcommerceID
                 inner join T_EcommerceGroup ergroup
                 on ergroup.EcommerceGroupID=epr.EcommerceGroupID where 1=1 and epr.DeleteMark=0 and epr.IsTrunk='1' and epr.ApprovalState='4'");

            return this.BaseRepository().FindList<EcommerceProjectRelationEntity>(strSql.ToString());
        }

        public string submitFormApp(List<FileModel> uploadFiles, List<T_CapitalFlow_NodeEntity> entity, string year, string month, string keyValue, string CapitalFlow_Title,string Job_Number)
        {
            try
            {
                //if (!string.IsNullOrEmpty(keyValue))
                //{
                //    entity.ApprovalState = 3;
                //    entity.Modify(keyValue);
                //    this.BaseRepository().Update(entity);
                //}
                //else
                //{
                //    entity.Create();
                //    entity.ApprovalState = 3;
                //    db.Insert(entity);
                ////}
                //var parameterFirst = new List<DbParameter>();
                //parameterFirst.Add(DbParameters.CreateDbParameter("@Company_Id", entity[0].Company_Id));
                //parameterFirst.Add(DbParameters.CreateDbParameter("@Year", year));
                //parameterFirst.Add(DbParameters.CreateDbParameter("@Month", month));

                //StringBuilder strSql = new StringBuilder();
                //strSql.Append("select CapitalFlow_Id from T_CapitalFlow  where Company_Id=@Company_Id and year=@Year and month=@Month and ApprovalState<>4");
                //IEnumerable<T_CapitalFlowEntity> newEntity = this.BaseRepository().FindList<T_CapitalFlowEntity>(strSql.ToString(), parameterFirst.ToArray());
                //if (newEntity.Count() != 0)
                //{
                //    foreach (T_CapitalFlowEntity t in newEntity) {
                //        this.BaseRepository().ExecuteBySql("update T_CapitalFlow_Node set DeleteMark=1 where CapitalFlow_Id='" + t.CapitalFlow_Id + "'");

                //    }
                //}

                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();



                T_CapitalFlowEntity cfEntity = new T_CapitalFlowEntity();
                if (!string.IsNullOrEmpty(keyValue))
                {
                    cfEntity.Company_Id = entity[0].Company_Id;
                    cfEntity.DeleteMark = 0;
                    cfEntity.Year = int.Parse(year);
                    cfEntity.Month = int.Parse(month);
                    cfEntity.CapitalFlow_Title = CapitalFlow_Title;
                    cfEntity.ApprovalState = 1;
                    cfEntity.Account = OperatorProvider.Provider.Current().Account;
                    cfEntity.Job_Number = Job_Number;
                    cfEntity.Modify(keyValue);
                    this.BaseRepository().Update(cfEntity);
                    db.ExecuteBySql("update T_CapitalFlow_Node set DeleteMark=1 where CapitalFlow_Id= '" + keyValue + "'");
                }
                else
                {

                    cfEntity.Create();
                    cfEntity.CreateDate = DateTime.Now;
                    cfEntity.DeleteMark = 0;
                    cfEntity.Year = int.Parse(year);
                    cfEntity.Job_Number = Job_Number;
                    cfEntity.Month = int.Parse(month);
                    cfEntity.CapitalFlow_Title = CapitalFlow_Title;
                    cfEntity.ApprovalState = 1;
                    cfEntity.Company_Id = entity[0].Company_Id;
                    db.Insert(cfEntity);
                }

                try
                {
                        foreach (T_CapitalFlow_NodeEntity item in entity)
                        {
                            item.Create();
                            item.CapitalFlow_Id = cfEntity.CapitalFlow_Id;
                            item.DeleteMark = 0;
                            item.ClearingAmount = item.ClearingAmount;
                            item.IncomeAmount = item.IncomeAmount;
                            item.PlatformExpensesAmount = item.PlatformExpensesAmount;
                            item.CapitalPoolAdd = item.CapitalPoolAdd;
                            item.Month = int.Parse(month);
                            item.Year = int.Parse(year);
                            string upDate = string.Format("{0}-{1}", year, month);
                            item.UploadDate = Convert.ToDateTime(upDate);
                            db.Insert(item);
                        }
                    attservice.MapingFiles(cfEntity.CapitalFlow_Id, uploadFiles, db);
                    db.Commit();
                    return cfEntity.CapitalFlow_Id;
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SaveFormApp(List<FileModel> uploadFiles, List<T_CapitalFlow_NodeEntity> entity, string year, string month, string keyValue, string CapitalFlow_Title, string Job_Number)
        {
            try
            {

                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                T_CapitalFlowEntity cfEntity = new T_CapitalFlowEntity();
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.ExecuteBySql("update T_CapitalFlow_Node set DeleteMark=1 where CapitalFlow_Id='" + keyValue + "'");
                    cfEntity.Modify(keyValue);
                    cfEntity.Year = int.Parse(year);
                    cfEntity.Month = int.Parse(month);
                    cfEntity.Job_Number = Job_Number;
                    cfEntity.CapitalFlow_Title = CapitalFlow_Title;
                    cfEntity.Account=OperatorProvider.Provider.Current().Account;
                    cfEntity.ApprovalState = 1;
                    if (entity != null)
                    {
                        cfEntity.Company_Id = entity[0].Company_Id;
                    }
                    this.BaseRepository().Update(cfEntity);

                }
                else
                {
                    cfEntity.Create();
                    cfEntity.CreateDate = DateTime.Now;
                    cfEntity.DeleteMark = 0;
                    cfEntity.Year = int.Parse(year);
                    cfEntity.Month = int.Parse(month);
                    cfEntity.Job_Number = Job_Number;
                    cfEntity.CapitalFlow_Title = CapitalFlow_Title;
                    cfEntity.ApprovalState = 1;
                    if (entity != null) {
                        cfEntity.Company_Id = entity[0].Company_Id;
                    }
                    db.Insert(cfEntity);
                }

                try
                {
                    if (entity != null) {
                        foreach (T_CapitalFlow_NodeEntity item in entity)
                        {
                            item.Create();
                            item.CapitalFlow_Id = cfEntity.CapitalFlow_Id;
                            item.DeleteMark = 0;
                            item.ClearingAmount = item.ClearingAmount ;
                            item.IncomeAmount = item.IncomeAmount ;
                            item.PlatformExpensesAmount = item.PlatformExpensesAmount ;
                            item.CapitalPoolAdd = item.CapitalPoolAdd ;
                            item.Month = int.Parse(month);
                            item.Year = int.Parse(year);
                            string upDate = string.Format("{0}-{1}", year, month);
                            item.UploadDate = Convert.ToDateTime(upDate);
                            db.Insert(item);
                        }
                    }
                    attservice.MapingFiles(cfEntity.CapitalFlow_Id, uploadFiles, db);
                    db.Commit();
                    
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
               
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<CapitalFlowViewModel> GetStartUrl(string keyValue, string starturlname)
        {
            var expression = LinqExtensions.True<T_CapitalFlowEntity>();
            expression = expression.And(t => t.DeleteMark == 0).And(t => t.CapitalFlow_Id == keyValue);
            var predata = this.BaseRepository().FindList<T_CapitalFlowEntity>(expression);
            List<CapitalFlowViewModel> eprviews = new List<CapitalFlowViewModel>();
            foreach (var item in predata)
            {
                CapitalFlowViewModel eprview = new CapitalFlowViewModel();
                eprview.CapitalFlow_Id = keyValue;
                eprview.url = string.Format("{0}?BSID=EC_Income&BOID={1}&UserId={2}&Key={3}", starturlname, keyValue, item.Account, BpmMD5Helper.GetEnCodeStr(keyValue));
                if (!eprviews.Contains(eprview))
                {
                    eprviews.Add(eprview);
                }
            }
            return eprviews;
        }
        public void ApprovalUpdateState(string keyValue, T_CapitalFlowEntity entity)
        {
            try
            {

                var model = this.BaseRepository().FindEntity<T_CapitalFlowEntity>(entity.CapitalFlow_Id);
                model.ApprovalState = entity.ApprovalState;
                model.LatestApprover = entity.LatestApprover;
                model.LatestApprovetime = entity.LatestApprovetime;
                model.LatestComment = entity.LatestComment;
                model.Procinstid = entity.Procinstid.ToString();
                model.ModifyDate = DateTime.Now;
                this.BaseRepository().Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<T_CapitalFlowEntity> check(string keyValue, T_CapitalFlowEntity entity)
        {
            try
            {

                var model = this.BaseRepository().FindEntity<T_CapitalFlowEntity>(entity.CapitalFlow_Id);
                var parameterFirst = new List<DbParameter>();
                parameterFirst.Add(DbParameters.CreateDbParameter("@Company_Id", model.Company_Id));
                parameterFirst.Add(DbParameters.CreateDbParameter("@Year", model.Year));
                parameterFirst.Add(DbParameters.CreateDbParameter("@Month", model.Month));
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from T_CapitalFlow  where Company_Id=@Company_Id and year=@Year and month=@Month and ApprovalState=4 order by orderNo desc");
                return this.BaseRepository().FindList<T_CapitalFlowEntity>(strSql.ToString(), parameterFirst.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public IEnumerable<T_CapitalFlowEntity> checkCaFLow(List<T_CapitalFlow_NodeEntity> entity, string year, string month)
        {
            var parameterFirst = new List<DbParameter>();
            parameterFirst.Add(DbParameters.CreateDbParameter("@Company_Id", entity[0].Company_Id));
            parameterFirst.Add(DbParameters.CreateDbParameter("@Year", year));
            parameterFirst.Add(DbParameters.CreateDbParameter("@Month", month));

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CapitalFlow_Id from T_CapitalFlow  where Company_Id=@Company_Id and year=@Year and month=@Month and ApprovalState=3");
            return this.BaseRepository().FindList<T_CapitalFlowEntity>(strSql.ToString(), parameterFirst.ToArray());
        }
        public IEnumerable<CapitalFlow_ProRelaView> updateMoney(string keyValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.UploadDate,a.CapitalFlow_Details_Id,c.EcommerceProjectRelationID,b.CreateUserId as CreateUserId,b.CreateUserName as CreateUserName, c.ProjectID,c.EcommerceID,c.ActualControlTotalAmount,sum(a.CapitalPoolAdd) as CapitalPoolAdd ,c.ControlTotalAmount from  T_CapitalFlow_Node a inner join T_CapitalFlow b on a.CapitalFlow_Id=b.CapitalFlow_Id inner join T_EcommerceProjectRelation c on c.EcommerceID=a.EcommerceID  and c.ProjectID=a.ProjectID  where a.DeleteMark='0'and c.IsTrunk=1 and b.CapitalFlow_Id= '" + keyValue + "' group by c.ProjectID,c.EcommerceID,c.ActualControlTotalAmount,c.ControlTotalAmount,c.EcommerceProjectRelationID,b.CreateUserId,b.CreateUserName,a.CapitalFlow_Details_Id,a.UploadDate");
            return this.BaseRepository().FindList<CapitalFlow_ProRelaView>(strSql.ToString());
        }

        public void updateOrderNo(int orderNo, string keyValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_CapitalFlow set orderNo=" + orderNo + " where CapitalFlow_Id='" + keyValue + "'");
            this.BaseRepository().ExecuteBySql(strSql.ToString());
        }
        public void updateDeleteMark(string keyValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_CapitalFlow_Node set DeleteMark=1 where CapitalFlow_Id='" + keyValue + "'");
            this.BaseRepository().ExecuteBySql(strSql.ToString());
        }
        public void updateCapDeleteMark(string keyValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_CapitalFlow set DeleteMark=1 where CapitalFlow_Id='" + keyValue + "'");
            this.BaseRepository().ExecuteBySql(strSql.ToString());
        }
        #endregion
    }
}
