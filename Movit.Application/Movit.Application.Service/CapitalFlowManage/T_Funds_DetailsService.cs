using Movit.Application.Entity;
using Movit.Application.IService.CapitalFlowManage;
using Movit.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movit.Util;
using System.Data.Common;
using Movit.Data;
using Movit.Util.Extension;
using Movit.Util.WebControl;
using Movit.Application.Code;
using Movit.Application.Service.AuthorizeManage;
using Movit.Application.Entity.CapitalFlowManage.ViewModel;
using Movit.Application.Entity.EcomPartnerCapitalPoolManage;
using Movit.Application.IService;

namespace Movit.Application.Service.CapitalFlowManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-07-02 13:40
    /// 描 述：T_Funds_Details
    /// </summary>
    public class T_Funds_DetailsService : RepositoryFactory, IT_Funds_DetailsService
    {
        private Base_ProjectInfoIService baseProject = new Base_ProjectInfoService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<T_Funds_DetailsEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable<T_Funds_DetailsEntity>().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public T_Funds_DetailsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<T_Funds_DetailsEntity>(keyValue);
        }
        public IEnumerable<T_Funds_DetailsViewModel> GetPieDataList(string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            var year = Convert.ToInt32(queryParam["loaclyear"]);
            sqlstr.Append(@"SELECT b.EcommerceGroupName,sum(a.ActualControlTotalAmount) as ActualControlTotalAmount
                            FROM [E_Commerce_DB].[dbo].[T_Funds_Details] a 
                            inner join dbo.T_EcommerceGroup b on a.EcommerceGroupID=b.EcommerceGroupID");
            if (!new AuthorizeService<T_Funds_DetailsViewModel>().LookAll())
            {
                sqlstr.AppendFormat(@"   inner join view_post_project pp on a.ProjectID=pp.ItemId and 
                                pp.UserId='{0}'", SystemInfo.CurrentUserId);
            }
            sqlstr.AppendFormat(@"  where 1=1 and a.Year={0} and a.IsLastDayOfYear=1", year);
            if (!queryParam["companyid"].IsEmpty())
            {
                sqlstr.Append(" and a.CompanyID=@companyid");
                parameter.Add(DbParameters.CreateDbParameter("@companyid", queryParam["companyid"].ToString()));
            }
            if (!queryParam["projectid"].IsEmpty())
            {
                sqlstr.Append(" and a.ProjectID=@projectid");
                parameter.Add(DbParameters.CreateDbParameter("@projectid", queryParam["projectid"].ToString()));
            }
            sqlstr.Append(@" group by b.EcommerceGroupName");
            return this.BaseRepository().FindList<T_Funds_DetailsViewModel>(sqlstr.ToString(), parameter.ToArray());
        }

        public IEnumerable<T_PartnerCapitalPoolViewModel> GetLineDataList(string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            var year = Convert.ToInt32(queryParam["loaclyear"]);
            sqlstr.Append(@"select isnull(sum(pcp.OperationMoney),0) as CurrentBalance, pcp.AccountingType,
                            left(convert(varchar,pcp.StatisticalDate,21),7) as sDate
                            from T_PartnerCapitalPool pcp
                            inner join T_EcommerceProjectRelation epr on
                            pcp.EcommerceProjectRelationID=epr.EcommerceProjectRelationID
                              ");
            //if (!new AuthorizeService<T_PartnerCapitalPoolViewModel>().LookAll())
            //{
            //    string a = new AuthorizeService<T_PartnerCapitalPoolViewModel>().GetReadProjectId();
            //    if (a != null && a != "")
            //    {
            //        string b = a.Replace(",", "','");
            //        string c = b.Substring(0, b.Length - 2);
            //        sqlstr.Append("and  epr.ProjectID in('" + c + ") ");
            //    }
            //    else {
            //        sqlstr.Append("and  epr.ProjectID =null ");
            //    }
            //}
            if (!new AuthorizeService<CapitalFlowViewModel>().LookAll())
            {
                string query = null;
                string projectIdList = "'";
                List<Base_ProjectInfoEntity> proList = baseProject.GetListByAuthorize(query).ToList();
                foreach (var item in proList)
                {
                    projectIdList += item.ProjectID + "','";
                }
                if (projectIdList != "'")
                {
                    projectIdList = projectIdList.Substring(0, projectIdList.LastIndexOf(','));
                }
                if (projectIdList != null && projectIdList != "'")
                {
                    sqlstr.Append("and  epr.ProjectID in(" + projectIdList + ") ");
                }
                else
                {
                    sqlstr.Append("and  epr.ProjectID =null ");
                }
            }
            sqlstr.AppendFormat(@"  where 1=1 and pcp.DeleteMark=0 and datename(YY,pcp.StatisticalDate)={0} ", year);
            if (!queryParam["companyid"].IsEmpty())
            {
                sqlstr.Append(" and epr.CompanyID=@companyid");
                parameter.Add(DbParameters.CreateDbParameter("@companyid", queryParam["companyid"].ToString()));
            }

            if (!queryParam["projectid"].IsEmpty())
            {
                sqlstr.Append(" and epr.ProjectID=@projectid");
                parameter.Add(DbParameters.CreateDbParameter("@projectid", queryParam["projectid"].ToString()));
            }
            sqlstr.Append(@" group by left(convert(varchar,pcp.StatisticalDate,21),7) , pcp.AccountingType");
            return this.BaseRepository().FindList<T_PartnerCapitalPoolViewModel>(sqlstr.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取资金池数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public T_Funds_DetailsEntity GetFundStaticJson(string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            int dateyear = DateTime.Now.Year;
            var year = Convert.ToInt32(queryParam["loaclyear"]);
            sqlstr.Append(@"select isnull(SUM(fd.ControlTotalAmount),0) as ControlTotalAmount,
                            isnull(SUM(fd.FlowNopayTotalAmount),0) as FlowNopayTotalAmount
                            ,isnull(SUM(fd.ActualControlTotalAmount),0) as ActualControlTotalAmount
                             from T_Funds_Details fd ");
            if (!new AuthorizeService<T_Funds_DetailsEntity>().LookAll())
            {
                sqlstr.AppendFormat(@"   inner join view_post_project pp on fd.ProjectID=pp.ItemId and 
                                pp.UserId='{0}'", SystemInfo.CurrentUserId);
            }
            if (dateyear == year)
            {
                sqlstr.AppendFormat(@"  where 1=1 and fd.Year={0} and fd.Month={1} and fd.Day={2}", year, DateTime.Now.Month, DateTime.Now.Day);
            }
            else
            {
                sqlstr.AppendFormat(@"  where 1=1 and fd.Year={0} and fd.IsLastDayOfYear=1", year);
            }
            if (!queryParam["companyid"].IsEmpty())
            {
                sqlstr.Append(" and fd.CompanyID=@companyid");
                parameter.Add(DbParameters.CreateDbParameter("@companyid", queryParam["companyid"].ToString()));
            }
            if (!queryParam["projectid"].IsEmpty())
            {
                sqlstr.Append(" and fd.ProjectID=@projectid");
                parameter.Add(DbParameters.CreateDbParameter("@projectid", queryParam["projectid"].ToString()));
            }
            return this.BaseRepository().FindEntity<T_Funds_DetailsEntity>(sqlstr.ToString(), parameter.ToArray());
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
                this.BaseRepository().Delete<T_Funds_DetailsEntity>(keyValue);
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
        public void SaveForm(string keyValue, T_Funds_DetailsEntity entity)
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void BacthInsert(List<T_Funds_DetailsEntity> entityList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (entityList.Count > 0)
                {

                    db.ExecuteBySql(string.Format("  update T_Funds_Details set IsFirstDayOfMonth=0 where [month]={0}", entityList[0].Month));
                    db.ExecuteBySql(string.Format("  update T_Funds_Details set IsLastDayOfYear=0 where [Year]={0}", entityList[0].Year));
                    db.Insert(entityList);
                    db.Commit();
                }

            }
            catch (Exception ex)
            {
                db.Rollback();
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 按照统计日期删除数据
        /// 作者：姚栋
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public int DeleteByData(DateTime time)
        {
            try
            {
                var beginTime = time.ToString("yyyy-MM-dd 00:00:00");
                var endTime = time.ToString("yyyy-MM-dd 23:59:59");
                return this.BaseRepository().ExecuteBySql("delete T_Funds_Details where StatisticalDate>='" + beginTime + "' and StatisticalDate<='" + endTime + "'");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

