using Movit.Application.Entity.EcommercePayQueryManage.ViewModel;
using Movit.Application.IService.EcommercePayQueryManage;
using Movit.Application.Service.AuthorizeManage;
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

namespace Movit.Application.Service.EcommercePayQueryManage
{
    /// <summary>
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：durry
    /// 日 期：2018-07-02 09:54
    /// 描 述：EcomPayProject
    /// </summary>
    public class EcomPayProjectService : RepositoryFactory, IEcomPayProjectService
    {
        #region 获取数据
        public IEnumerable<ProjectView> GetAllCompany(Pagination pagination, string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select * from(select a.CompanyId as CompanyID,a.CompanyName  from        
                            T_EcommerceProjectRelation a          
                            left join T_CapitalFlow_Node c
                            on c.Company_Id=a.CompanyId"); 

            if(!new AuthorizeService<ProjectView>().LookAll())
            {
                sqlstr.AppendFormat(@"  inner join dbo.view_post_project pp on pp.ItemId=a.ProjectID and
                                    pp.UserId='{0}'", SystemInfo.CurrentUserId);
            }
                     sqlstr.Append(@"  where a.ApprovalState=4
                            group by a.CompanyId,a.CompanyName )A where 1=1");
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            if (!queryParam["CompanyId"].IsEmpty())
            {
                var companyids = queryParam["CompanyId"].ToArray();
                string compaynid = string.Empty;
                if (companyids.Count() == 1)
                {
                    compaynid += companyids[0].ToString();
                }
                else
                {
                    for (int i = 0; i < companyids.Count(); i++)
                    {
                        compaynid += "'" + companyids[i].ToString() + "',";
                    }
                    compaynid = compaynid.Substring(1);
                    compaynid = compaynid.Substring(0, compaynid.Length - 2);
                }
                sqlstr.Append(" and A.CompanyId in ('" + compaynid + "')");
            }
            return this.BaseRepository().FindList<ProjectView>(sqlstr.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 获取所有项目的信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<ProjectView> GetAllProject(string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            sqlstr.Append(@"select * from
                            (select NEWID()as id,epr.ProjectID,epr.ProjecName as ProjectName,
                            epr.CompanyId as CompanyID,epr.CompanyName,
                            isnull(sum(tempCapitalFlow.IncomeAmount),0) as IncomeTotal,
                            isnull(SUM(tempCapitalFlow.ClearingAmount),0) as ClearingTotal,
                            ISNULL(SUM(tempCapitalFlow.PlatformExpensesAmount),0) as PlatformExpensesAmount,
                            isnull(sum(tempCapitalFlow.ClearingAmount)-SUM(tempCapitalFlow.PlatformExpensesAmount),0)
                            as ControllAmount,
                            (select isnull(sum(pinfo.Pay_Money),0) from T_Pay_Info pinfo 
                            where 1=1 and pinfo.Project_Id=epr.ProjectID and pinfo.DeleteMark=0
                            and pinfo.Approval_Status=4");
            
            if (!queryParam["endtime"].IsEmpty())
            {
                sqlstr.Append(@" and pinfo.CreateDate<=(select dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,@endtime)+1, 0))) ");
                parameter.Add(DbParameters.CreateDbParameter("@endtime", queryParam["endtime"].ToString()));
            }
             if (!queryParam["starttime"].IsEmpty())
            {
                sqlstr.Append(@" and pinfo.CreateDate>=(select dateadd(dd,-day(@starttime)+1,@starttime)) ");
                parameter.Add(DbParameters.CreateDbParameter("@starttime", queryParam["starttime"].ToString()));
            }
             sqlstr.Append(@" ) EcommerceExpenseTotal,
                            (select ISNULL(SUM(tsfinfo.Transfer_Money),0) from T_Transfer_Info tsfinfo
                            where 1=1 and tsfinfo.ProjectID=epr.ProjectID and tsfinfo.DeleteMark=0");
               if (!queryParam["endtime"].IsEmpty())
            {
                sqlstr.Append(@" and tsfinfo.CreateDate<=(select dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,@endtime)+1, 0))) ");
            }
             if (!queryParam["starttime"].IsEmpty())
            {
                sqlstr.Append(@" and tsfinfo.CreateDate>=(select dateadd(dd,-day(@starttime)+1,@starttime)) ");
            }
            sqlstr.Append(@"  ) as TransfoTotal
                from (select distinct tepr.ProjectID,tepr.ProjecName,tepr.CompanyId,
                tepr.CompanyName from T_EcommerceProjectRelation tepr where tepr.ApprovalState=4 and tepr.IsTrunk=1 
                and tepr.DeleteMark=0) epr
                left join (
                select cfn.ProjectID,cfn.CreateDate,cfn.PlatformExpensesAmount,cfn.IncomeAmount,cfn.ClearingAmount
                from T_CapitalFlow_Node cfn
                inner join T_CapitalFlow cf
                on cfn.CapitalFlow_Id=cf.CapitalFlow_Id
                where 1=1 and cf.ApprovalState=4 and cfn.DeleteMark=0 and cf.DeleteMark=0");
             if (!queryParam["endtime"].IsEmpty())
            {
                sqlstr.Append(@" and cfn.UploadDate<=(select dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,@endtime)+1, 0))) ");
            }
             if (!queryParam["starttime"].IsEmpty())
            {
                sqlstr.Append(@" and cfn.UploadDate>=(select dateadd(dd,-day(@starttime)+1,@starttime)) ");
            }
             sqlstr.Append(@"  ) tempCapitalFlow
                            on epr.ProjectID=tempCapitalFlow.ProjectID
                            group by epr.ProjectID,epr.ProjecName,epr.CompanyId,epr.CompanyName
                            ) A WHERE 1=1");
            if (!queryParam["ProjectID"].IsEmpty())
            {
                sqlstr.Append(" and ProjectID=@ProjectID");
                parameter.Add(DbParameters.CreateDbParameter("@ProjectID", queryParam["ProjectID"].ToString()));
            }
            return new AuthorizeService<ProjectView>().FindList(sqlstr.ToString(), parameter.ToArray(), "ProjectID");
        }
        /// <summary>
        /// 获取所有的信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<ProjectView> GetAllList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"SELECT * FROM(select NEWID()as id,epr.ProjectID,epr.ProjecName as ProjectName,
                            epr.CompanyId as CompanyID,epr.CompanyName,epr.EcommerceGroupID
                            ,epr.EcommerceGroupName,
                            isnull(sum(tempCapitalFlow.IncomeAmount),0) as IncomeTotal,
                            isnull(SUM(tempCapitalFlow.ClearingAmount),0) as ClearingTotal,
                            ISNULL(SUM(tempCapitalFlow.PlatformExpensesAmount),0) as PlatformExpensesAmount,
                            isnull(sum(tempCapitalFlow.ClearingAmount)-SUM(tempCapitalFlow.PlatformExpensesAmount),0)
                            as ControllAmount,
                            (select isnull(sum(pinfo.Pay_Money),0) from T_Pay_Info pinfo 
                            where 1=1 and pinfo.Project_Id=epr.ProjectID and pinfo.DeleteMark=0
                            and pinfo.Approval_Status=4 and pinfo.EcommerceGroupID=epr.EcommerceGroupID");
             if (!queryParam["endtime"].IsEmpty())
            {
                sqlstr.Append(@" and pinfo.CreateDate<=(select dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,@endtime)+1, 0))) ");
                parameter.Add(DbParameters.CreateDbParameter("@endtime", queryParam["endtime"].ToString()));
            }
             if (!queryParam["starttime"].IsEmpty())
            {
                sqlstr.Append(@" and pinfo.CreateDate>=(select dateadd(dd,-day(@starttime)+1,@starttime)) ");
                parameter.Add(DbParameters.CreateDbParameter("@starttime", queryParam["starttime"].ToString()));
            }
             sqlstr.Append(@"  ) EcommerceExpenseTotal,
                            (select top 1 isnull(te.PlatformRate,0) from T_Ecommerce te where te.EcommerceGroupID=epr.EcommerceGroupID
                            order by te.CreateDate) as Platform,  (select ISNULL(SUM(tsfinfo.Transfer_Money),0) from T_Transfer_Info tsfinfo
                            where 1=1 and tsfinfo.ProjectID=epr.ProjectID and tsfinfo.EcommerceGroupID=epr.EcommerceGroupID 
                            and tsfinfo.DeleteMark=0");
               if (!queryParam["endtime"].IsEmpty())
            {
                sqlstr.Append(@" and tsfinfo.CreateDate<=(select dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,@endtime)+1, 0))) ");
            }
             if (!queryParam["starttime"].IsEmpty())
            {
                sqlstr.Append(@" and tsfinfo.CreateDate>=(select dateadd(dd,-day(@starttime)+1,@starttime)) ");
            }
             sqlstr.Append(@"  ) as TransfoTotal  from (select distinct tepr.ProjectID,tepr.ProjecName,tepr.CompanyId,tepr.EcommerceGroupID,
                    ergroup.EcommerceGroupName,
                    tepr.CompanyName from T_EcommerceProjectRelation tepr inner join T_EcommerceGroup ergroup on
                            ergroup.EcommerceGroupID=tepr.EcommerceGroupID
                    where tepr.ApprovalState=4 and tepr.IsTrunk=1 
                    and tepr.DeleteMark=0) epr
                    left join (
                    select cfn.ProjectID,cfn.UploadDate,cfn.PlatformExpensesAmount,cfn.IncomeAmount,cfn.ClearingAmount,
                    cfn.EcommerceGroupID,cfn.Proportion
                    from T_CapitalFlow_Node cfn
                    inner join T_CapitalFlow cf
                    on cfn.CapitalFlow_Id=cf.CapitalFlow_Id
                    where 1=1 and cf.ApprovalState=4 and cfn.DeleteMark=0 and cf.DeleteMark=0");
              if (!queryParam["endtime"].IsEmpty())
            {
                sqlstr.Append(@" and cfn.UploadDate<=(select dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,@endtime)+1, 0))) ");
            }
             if (!queryParam["starttime"].IsEmpty())
            {
                sqlstr.Append(@" and cfn.UploadDate>=(select dateadd(dd,-day(@starttime)+1,@starttime)) ");
            }
            sqlstr.Append(@" ) tempCapitalFlow
                            on epr.ProjectID=tempCapitalFlow.ProjectID and epr.EcommerceGroupID=tempCapitalFlow.EcommerceGroupID
                            group by epr.ProjectID,epr.ProjecName,epr.CompanyId,epr.CompanyName,epr.EcommerceGroupID
                            ,epr.EcommerceGroupName) B WHERE 1=1");
            if (!queryParam["CompanyId"].IsEmpty())
            {
                var companyids = queryParam["CompanyId"].ToArray();
                string compaynid = string.Empty;
                if (companyids.Count() == 1)
                {
                    compaynid += companyids[0].ToString();
                }
                else
                {
                    for (int i = 0; i < companyids.Count(); i++)
                    {
                        compaynid += "'" + companyids[i].ToString() + "',";
                    }
                    compaynid = compaynid.Substring(1);
                    compaynid = compaynid.Substring(0, compaynid.Length - 2);
                }
                sqlstr.Append(" and B.CompanyID in ('" + compaynid + "')");
            }
            if (!queryParam["ProjectID"].IsEmpty())
            {
                sqlstr.Append(" and ProjectID=@ProjectID");
                parameter.Add(DbParameters.CreateDbParameter("@ProjectID", queryParam["ProjectID"].ToString()));
            }
            return new AuthorizeService<ProjectView>().FindList(sqlstr.ToString(), parameter.ToArray(), "ProjectID");
        }
        #endregion
    }
}
