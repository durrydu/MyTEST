using Movit.Application.Entity.EcommerceTransferManage;
using Movit.Application.Entity.EcommerceTransferManage.ViewModel;
using Movit.Application.IService.EcommerceTransferManage;
using Movit.Data;
using Movit.Data.Repository;
using Movit.Util.WebControl;
using Movit.Application.Entity;
using Movit.Application.IService;
using Movit.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using Movit.Util;
using System.Text;
using System.Data.Common;
using Movit.Application.Service.AuthorizeManage;
using Movit.Application.IService.SystemManage;
using Movit.Application.Service.SystemManage;
using Movit.Application.Code;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.IService.EcommerceContractManage;
using Movit.Application.Service.EcommerceContractManage;
using Movit.Application.IService.EcommercePayQueryManage;
using Movit.Application.Entity.EcommercePayQueryManage.ViewModel;

namespace Movit.Application.Service.EcommercePayQueryManage
{

    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：durry
    /// 日 期：2018-06-25 14:48
    /// </summary>
    public class EcomPayTotalService : RepositoryFactory, IEcomPayTotalService
    {
        #region 获取数据
        /// <summary>
        /// 获取项目的所有信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<CompanyView> GetAllList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            DateTime datetime = DateTime.Now;
            int dateyear = datetime.Year;
            int datamomtn = datetime.Month;
            int dataday = datetime.Day;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select * from( select  
                            NewID() as id, 
                            er.ProjecName as
                            ProjectName,er.ProjectID,er.CompanyId,ergroup.EcommerceGroupName,er.EcommerceGroupID,
                            er.CompanyName,ISNULL( SUM(fd.ControlTotalAmount),0) as  ControlTotalAmount,
                            isnull(SUM(fd.FlowNopayTotalAmount),0) as FlowNopayTotalAmount,
                            isnull( SUM(fd.ControlTotalAmount)-SUM(fd.FlowNopayTotalAmount),0) as ActualControlTotalAmount
                            from T_EcommerceProjectRelation er
                            inner join T_EcommerceGroup ergroup on
                            ergroup.EcommerceGroupID=er.EcommerceGroupID
                            left join T_Funds_Details fd on 1=1 and
                            fd.EcommerceGroupID=er.EcommerceGroupID and fd.CompanyID=er.CompanyId
                            and fd.ProjectID=er.ProjectID");
            if (queryParam["endtime"].IsEmpty())
            {
                sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.Day='{2}'", dateyear, datamomtn, dataday);
                //if (Time.IsFirstDayOfYear(datetime))
                //{
                //    dateyear = dateyear - 1;
                //    datamomtn = 12;
                //    dataday = 31;
                //    sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.Day='{2}'", dateyear, datamomtn, dataday);
                //}
                //else
                //{
                //    if (Time.IsFirstDayOfMonth(datetime) && datamomtn != 1)
                //    {
                //        datamomtn = datamomtn - 1;
                //        dataday = Time.GetDaysOfMonth(dateyear, datamomtn);
                //        sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.Day='{2}'", dateyear, datamomtn, dataday);
                //    }
                //    else
                //    {
                //        dataday = dataday - 1;
                //        sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.Day='{2}'", dateyear, datamomtn, dataday);
                //    }
                //}
            }
            else
            {
                DateTime ddtime = Convert.ToDateTime(queryParam["endtime"].ToString());
                int ddyear = ddtime.Year;
                int ddmomtn = ddtime.Month;
                if (ddmomtn == datamomtn && ddyear == dateyear)
                {
                    sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.Day='{2}'", dateyear, datamomtn, dataday);
                }
                else
                {
                    sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.IsLastDayOfMonth='{2}'", ddyear, ddmomtn, 1);
                }
            }
            sqlstr.Append(@" where 1=1 and er.DeleteMark=0  and er.IsTrunk=1 and er.ApprovalState=4
                            group by er.ProjecName,er.ProjectID,er.CompanyId,er.CompanyName,ergroup.EcommerceGroupName,er.EcommerceGroupID
                            ) B where 1=1");
            if (!queryParam["CompanyID"].IsEmpty())
            {
                var companyids = queryParam["CompanyID"].ToArray();
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
                    compaynid = compaynid.Substring(0, compaynid.Length -2 );
                }
                sqlstr.Append(" and B.CompanyId in ('" + compaynid + "')");
            }
            if (!queryParam["ProjectID"].IsEmpty())
            {
                sqlstr.Append(" and ProjectID=@ProjectID");
                parameter.Add(DbParameters.CreateDbParameter("@ProjectID", queryParam["ProjectID"].ToString()));
            }
            if (!queryParam["EcommerceGroupID"].IsEmpty())
            {
                sqlstr.Append(" and EcommerceGroupID=@EcommerceGroupID");
                parameter.Add(DbParameters.CreateDbParameter("@EcommerceGroupID", queryParam["EcommerceGroupID"].ToString()));
            }
            return new AuthorizeService<CompanyView>().FindList(sqlstr.ToString(), parameter.ToArray(), "ProjectID");
        }
        /// <summary>
        /// 获取关于区域公司的所有信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<CompanyView> GetListJson(Pagination pagination, string queryJson)
        {
            int start = (pagination.page - 1) * pagination.rows + 1;
            int end = start + pagination.rows - 1;
            StringBuilder sqlstr = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            DateTime datetime = DateTime.Now;
            int dateyear = datetime.Year;
            int datamomtn = datetime.Month;
            int dataday = datetime.Day;
            sqlstr.Append(@"select * from (select temp.CompanyId, cp.FullName as CompanyName,fd.StatisticalDate,
                            ISNULL( SUM(fd.ControlTotalAmount) ,0.0000)as ControlTotalAmount,
                            ISNULL( SUM(fd.FlowNopayTotalAmount) ,0.0000)as FlowNopayTotalAmount,
                            ISNULL( SUM(fd.ControlTotalAmount)-SUM(fd.FlowNopayTotalAmount),0.0000)as ActualControlTotalAmount
                            from (
                            SELECT distinct CompanyId from
                            T_EcommerceProjectRelation er 
                            ");
            if (!new AuthorizeService<CompanyView>().LookAll())
            {
                sqlstr.AppendFormat(@" inner join view_post_project pp on er.ProjectID=pp.ItemId and pp.UserId='{0}'", SystemInfo.CurrentUserId);
            }
            sqlstr.Append(@"where er.ApprovalState=4 and er.IsTrunk=1 and er.DeleteMark=0) temp
                            inner join Base_Department cp 
                            on temp.CompanyId=cp.DepartmentId
                            left join T_Funds_Details fd on
                            1=1 and temp.CompanyId=fd.CompanyID");
            if (queryParam["endtime"].IsEmpty())
            {

                sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.Day='{2}'", dateyear, datamomtn, dataday);
                //if (Time.IsFirstDayOfYear(datetime))
                //{
                //    dateyear=dateyear-1;
                //    datamomtn = 12;
                //    dataday = 31;
                //    sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.Day='{2}'", dateyear, datamomtn, dataday);
                //}
                //else
                //{
                //    if (Time.IsFirstDayOfMonth(datetime) && datamomtn != 1)
                //    {
                //        datamomtn=datamomtn-1;
                //        dataday = Time.GetDaysOfMonth(dateyear, datamomtn);
                //        sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.Day='{2}'", dateyear, datamomtn, dataday);
                //    }
                //    else
                //    {
                //        dataday=dataday-1;
                //        sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.Day='{2}'", dateyear, datamomtn, dataday);
                //    }
                //}
            }
            else
            {
                DateTime ddtime = Convert.ToDateTime(queryParam["endtime"].ToString());
                int ddyear = ddtime.Year;
                int ddmomtn = ddtime.Month;
                if (ddmomtn == datamomtn && ddyear == dateyear)
                {
                    sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.Day='{2}'", dateyear, datamomtn, dataday);
                }
                else
                {
                    sqlstr.AppendFormat(@" and fd.Year='{0}' and fd.Month='{1}' and fd.IsLastDayOfMonth='{2}'", ddyear, ddmomtn, 1);
                }
            }
            if (!queryParam["ProjectID"].IsEmpty())
            {
                sqlstr.Append(" and fd.ProjectID=@ProjectID");
                parameter.Add(DbParameters.CreateDbParameter("@ProjectID", queryParam["ProjectID"].ToString()));
            }
            if (!queryParam["EcommerceGroupID"].IsEmpty())
            {
                sqlstr.Append(" and fd.EcommerceGroupID=@EcommerceGroupID");
                parameter.Add(DbParameters.CreateDbParameter("@EcommerceGroupID", queryParam["EcommerceGroupID"].ToString()));
            }
            if (!new AuthorizeService<CompanyView>().LookAll())
            {
                sqlstr.AppendFormat(@"  left join   view_post_project pp
                            on pp.ItemId=fd.ProjectID and pp.UserId='{0}'", SystemInfo.CurrentUserId);
            }
            sqlstr.Append(@"    group by temp.CompanyId,cp.FullName,fd.StatisticalDate) A where 1=1");
            if (!queryParam["CompanyID"].IsEmpty())
            {
                var companyids = queryParam["CompanyID"].ToArray();
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
                sqlstr.Append(" and   A.CompanyId in ('" + compaynid + "')");
            }
            return this.BaseRepository().FindList<CompanyView>(sqlstr.ToString(), parameter.ToArray(), pagination);
        }
        #endregion
    }
}
