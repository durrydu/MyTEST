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
    public class EcomPayEcommerceService:RepositoryFactory, IEcomPayEcommerceService
    {
        public IEnumerable<ProjectView> GetEcommerceGroupList(Pagination pagination,string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select * from
                            ( select NEWID()as id,TEMP.*,isnull(SUM(tempCapitalFlow.IncomeAmount),0) as IncomeTotal,
                            isnull(SUM(tempCapitalFlow.ClearingAmount),0) as ClearingTotal,
                            isnull(SUM(tempCapitalFlow.PlatformExpensesAmount),0) as PlatformExpensesAmount,
                            isnull(SUM(tempCapitalFlow.ClearingAmount)-SUM(tempCapitalFlow.PlatformExpensesAmount),0) 
                            as ControllAmount, (select isnull(sum(pinfo.Pay_Money),0) from T_Pay_Info pinfo ");
            if (!new AuthorizeService<ProjectView>().LookAll())
            {
                sqlstr.AppendFormat(@"  inner join view_post_project pp on pinfo.Project_Id=pp.ItemId and pp.UserId='{0}'", SystemInfo.CurrentUserId);
            }
            sqlstr.Append(@" where 1=1 and pinfo.EcommerceGroupID=temp.EcommerceGroupID and pinfo.DeleteMark=0
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
             sqlstr.Append(@" ) EcommerceExpenseTotal, (select ISNULL(SUM(tsfinfo.Transfer_Money),0) from T_Transfer_Info tsfinfo
                            where 1=1 and tsfinfo.EcommerceGroupID=TEMP.EcommerceGroupID and tsfinfo.DeleteMark=0");
                if (!queryParam["endtime"].IsEmpty())
            {
                sqlstr.Append(@" and tsfinfo.CreateDate<=(select dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,@endtime)+1, 0))) ");
            }
             if (!queryParam["starttime"].IsEmpty())
            {
                sqlstr.Append(@" and tsfinfo.CreateDate>=(select dateadd(dd,-day(@starttime)+1,@starttime)) ");
            }
             sqlstr.Append(@" ) as TransfoTotal  from(
                            select distinct a.EcommerceGroupID,ergroup.EcommerceGroupName from dbo.T_EcommerceProjectRelation a
                            inner join T_EcommerceGroup ergroup on
                            ergroup.EcommerceGroupID=a.EcommerceGroupID");
            if (!new AuthorizeService<ProjectView>().LookAll())
            {
                sqlstr.AppendFormat(@"  inner join view_post_project pp on a.ProjectID=pp.ItemId and pp.UserId='{0}'", SystemInfo.CurrentUserId);
            }
            sqlstr.Append(@" left join dbo.T_CapitalFlow_Node b on b.EcommerceGroupID=a.EcommerceGroupID
                            and a.DeleteMark=b.DeleteMark  where a.ApprovalState=4 ) TEMP
                            left join(
                            select cfn.EcommerceGroupID,cfn.UploadDate,cfn.PlatformExpensesAmount,cfn.IncomeAmount,
                            cfn.ClearingAmount from T_CapitalFlow_Node cfn ");
            if (!new AuthorizeService<ProjectView>().LookAll())
            {
                sqlstr.AppendFormat(@"  inner join view_post_project pp on cfn.ProjectID=pp.ItemId and pp.UserId='{0}'", SystemInfo.CurrentUserId);
            }
                    sqlstr.Append(@"inner join T_CapitalFlow cf
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
            sqlstr.Append(@" ) tempCapitalFlow on TEMP.EcommerceGroupID=tempCapitalFlow.EcommerceGroupID
                            group by TEMP.EcommerceGroupID,TEMP.EcommerceGroupName) A where 1=1 ");
            if (!queryParam["EcommerceGroupID"].IsEmpty())
            {
                sqlstr.Append(" and EcommerceGroupID=@EcommerceGroupID");
                parameter.Add(DbParameters.CreateDbParameter("@EcommerceGroupID", queryParam["EcommerceGroupID"].ToString()));
            }
            return this.BaseRepository().FindList<ProjectView>(sqlstr.ToString(), parameter.ToArray(), pagination);
        }
        public IEnumerable<ProjectView> GetCompanyList(string queryJson)
        {
             var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select NEWID()as id,TEMP.CompanyId,TEMP.CompanyName,
                            TEMP.*,
                            isnull(SUM(tempCapitalFlow.IncomeAmount),0) as IncomeTotal,
                            isnull(SUM(tempCapitalFlow.ClearingAmount),0) as ClearingTotal,
                            isnull(SUM(tempCapitalFlow.PlatformExpensesAmount),0) as PlatformExpensesAmount,
                            isnull(SUM(tempCapitalFlow.ClearingAmount)-SUM(tempCapitalFlow.PlatformExpensesAmount),0) 
                            as ControllAmount,
                            (select isnull(sum(pinfo.Pay_Money),0) from T_Pay_Info pinfo ");
            if (!new AuthorizeService<ProjectView>().LookAll())
            {
                sqlstr.AppendFormat(@" inner join view_post_project pp on pinfo.Project_Id=pp.ItemId and pp.UserId='{0}'", SystemInfo.CurrentUserId);
            }
            sqlstr.Append(@"where 1=1 and pinfo.CompanyID=temp.CompanyId and pinfo.DeleteMark=0
                            and pinfo.Approval_Status=4 and pinfo.EcommerceGroupID=temp.EcommerceGroupID");
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
                            (select top 1 isnull(te.PlatformRate,0) from T_Ecommerce te where te.EcommerceGroupID=TEMP.EcommerceGroupID
                                                        order by te.CreateDate) as Platform,
                         (select ISNULL(SUM(tsfinfo.Transfer_Money),0) from T_Transfer_Info tsfinfo
                                                    where 1=1 and tsfinfo.EcommerceGroupID=TEMP.EcommerceGroupID and 
                                tsfinfo.CompanyId=TEMP.CompanyId  and tsfinfo.DeleteMark=0");
             if (!queryParam["endtime"].IsEmpty())
            {
                sqlstr.Append(@" and tsfinfo.CreateDate<=(select dateadd(ms,-3,DATEADD(mm, DATEDIFF(m,0,@endtime)+1, 0))) ");
            }
             if (!queryParam["starttime"].IsEmpty())
            {
                sqlstr.Append(@" and tsfinfo.CreateDate>=(select dateadd(dd,-day(@starttime)+1,@starttime)) ");
            }
             sqlstr.Append(@"  ) as TransfoTotal   from(
                            select distinct a.CompanyId,a.CompanyName, a.EcommerceGroupID,a.EcommerceGroupName 
                            from dbo.T_EcommerceProjectRelation a ");
             if (!new AuthorizeService<ProjectView>().LookAll())
            {
                sqlstr.AppendFormat(@" inner join view_post_project pp on a.ProjectID=pp.ItemId and pp.UserId='{0}'", SystemInfo.CurrentUserId);
            }
            sqlstr.Append(@"left join dbo.T_CapitalFlow_Node b on b.EcommerceGroupID=a.EcommerceGroupID
                            and a.DeleteMark=b.DeleteMark  where a.ApprovalState=4 and a.DeleteMark=0
                            and a.IsTrunk=1) TEMP
                            left join(
                            select cfn.EcommerceGroupID,cfn.Company_Id,Proportion,cfn.PlatformExpensesAmount,cfn.IncomeAmount,
                            cfn.ClearingAmount
                            from T_CapitalFlow_Node cfn");  
            if (!new AuthorizeService<ProjectView>().LookAll())
            {
                sqlstr.AppendFormat(@" inner join view_post_project pp on cfn.ProjectID=pp.ItemId and pp.UserId='{0}'", SystemInfo.CurrentUserId);
            }
            sqlstr.Append(@" inner join T_CapitalFlow cf
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
            sqlstr.Append(@" ) tempCapitalFlow on TEMP.EcommerceGroupID=tempCapitalFlow.EcommerceGroupID
                            and temp.CompanyId=tempCapitalFlow.Company_Id
                            group by TEMP.EcommerceGroupID,TEMP.EcommerceGroupName
                            ,TEMP.CompanyId,TEMP.CompanyName");
            return this.BaseRepository().FindList<ProjectView>(sqlstr.ToString(), parameter.ToArray());
        }
    }
}
