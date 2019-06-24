using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.IService.EcommerceContractManage;
using Movit.Data.Repository;
using Movit.Util.Extension;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Movit.Util;
using Movit.Application.Entity.BaseManage;
using Movit.Data;
using Movit.Application.Code;
using Movit.Application.Service.BaseManage;
using Movit.Application.IService.BaseManage;
using Movit.Sys.Api.Code.Entity;
using Movit.Application.Service.AuthorizeManage;
using Movit.Application.Entity.EcommerceContractManage.ViewModel;
using Movit.Application.IService.SystemManage;
using Movit.Application.Service.SystemManage;
using Movit.Sys.Api.Code;
using Movit.Data.SQLSugar;


namespace Movit.Application.Service.EcommerceContractManage
{
    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：emily
    /// 日 期：2018-06-19 11:08
    /// 描 述：T_EcommerceProjectRelation
    /// </summary>
    public class EcommerceProjectRelationService : RepositoryFactory, IEcommerceProjectRelationService
    {
        private T_AttachmentIService attservice = new T_AttachmentService();
        private ICodeRuleService coderuleService = new CodeRuleService();
        private UserService userBll = null;
        public EcommerceProjectRelationService()
        {

            userBll = new UserService();
        }
        #region 获取数据
        /// <summary>
        /// 查询是否存在电商和项目是否存在istrunk=1
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="ecommerceid"></param>
        /// <param name="istrunk"></param>
        /// <returns></returns>
        public int GetIsTrunkCount(string projectid, string ecommerceid, int istrunk)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select ER.EcommerceID,ER.ProjectID,ER.IsTrunk FROM T_EcommerceProjectRelation ER
    where er.EcommerceID='" + ecommerceid + "' and er.ProjectID='" + projectid + "' and er.IsTrunk='" + istrunk + "'");
            var data = this.BaseRepository().FindList<EcommerceProjectRelationEntity>(sqlstr.ToString());
            return data.Count();
        }

        /// <summary>
        /// 获取当前用户可以看到的合同信息列表
        /// 作者：姚栋
        /// 日期：20180717
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EcommerceDiscountProgramEntity> GetListByAuthorize()
        {
            StringBuilder sqlBuilder = new StringBuilder("select * from T_EcommerceProjectRelation");
            var lookAll = new AuthorizeService<EcommerceDiscountProgramEntity>().LookAll();
            if (!lookAll)
            {
                sqlBuilder.Clear();
                sqlBuilder.AppendFormat(@"
                select  epr.*
                 from view_post_project userProject
                inner join T_EcommerceProjectRelation epr
                on userProject.ItemId=epr.ProjectID               
                 where userProject.UserId='{0}'
                ", SystemInfo.CurrentUserId);
            }
            return this.BaseRepository().FindList<EcommerceDiscountProgramEntity>(sqlBuilder.ToString());
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EcommerceProjectRelationView> GetPageList(Pagination pagination, string queryJson, string urlname)
        {
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select  er.EcommerceName,epr.EcommerceProjectRelationID,epr.ProjectID,epr.ProjecName,epr.EcommerceTypeName,       epr.CooperateStartTime,epr.CooperateEndTime,epr.PlatformRate,epr.Agent,epr.ApprovalState,epr.CreateDate,epr.Procinstid,epr.Account,
                ergroup.EcommerceGroupName
                 from T_EcommerceProjectRelation epr
                 inner join T_Ecommerce er
                 on epr.EcommerceID=er.EcommerceID
                 inner join T_EcommerceGroup ergroup
                 on ergroup.EcommerceGroupID=epr.EcommerceGroupID where 1=1 and epr.DeleteMark=0");
            if (!queryParam["ProjecName"].IsEmpty())
            {
                sqlstr.Append(" and epr.ProjecName like @projecName");
                parameter.Add(DbParameters.CreateDbParameter("@projecName", "%" + queryParam["ProjecName"].ToString() + "%"));
            }
            if (!queryParam["EcommerceGroupName"].IsEmpty())
            {
                sqlstr.Append(" and ergroup.EcommerceGroupName like @ecommerceGroupName");
                parameter.Add(DbParameters.CreateDbParameter("@ecommerceGroupName", "%" + queryParam["EcommerceGroupName"].ToString() + "%"));
            }
            if (!queryParam["EcommerceName"].IsEmpty())
            {
                sqlstr.Append(" and er.EcommerceName like @ecommerceName");
                parameter.Add(DbParameters.CreateDbParameter("@ecommerceName", "%" + queryParam["EcommerceName"].ToString() + "%"));
            }
            var olddata = new AuthorizeService<EcommerceProjectRelationEntity>().FindList(sqlstr.ToString(), parameter.ToArray(), pagination, "projectid");


            string domainurl = urlname;

            var result = olddata.Select(p => new EcommerceProjectRelationView()
            {
                Agent=p.Agent,
                EcommerceProjectRelationID = p.EcommerceProjectRelationID,
                ProjecName = p.ProjecName,
                EcommerceName = p.EcommerceName,
                EcommerceID = p.EcommerceID,
                EcommerceGroupID = p.EcommerceGroupID,
                EcommerceGroupName = p.EcommerceGroupName,
                EcommerceTypeName = p.EcommerceTypeName,
                CooperateStartTime = p.CooperateStartTime,
                CooperateEndTime = p.CooperateEndTime,
                PlatformRate = p.PlatformRate,
                CreateUserId = p.CreateUserId,
                CreateUserName = p.CreateUserName,
                Account = p.Account,
                ApprovalState = p.ApprovalState,
                CreateDate = p.CreateDate,
                Procinstid = p.Procinstid,
                url = string.Format("{0}?procInstId={1}&userid={2}&key={3}", domainurl, p.Procinstid, p.Account, BpmMD5Helper.GetEnCodeStr(p.Procinstid))
            });

            return result;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyValue">查询参数</param>
        /// <param name="state">审批状态</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceProjectRelationEntity> GetList(string keyValue, int state, int istrunk)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendFormat(@"select  er.EcommerceName,epr.EcommerceProjectRelationID,epr.ProjectID,epr.ProjecName,epr.EcommerceTypeName,       epr.CooperateStartTime,epr.CooperateEndTime,epr.PlatformRate,epr.Agent,epr.ApprovalState,epr.CreateDate,epr.Procinstid,epr.Account,
              epr.ActualControlTotalAmount,epr.FlowNopayTotalAmount,epr.ControlTotalAmount,
                ergroup.EcommerceGroupName
                 from T_EcommerceProjectRelation epr
                 inner join T_Ecommerce er
                 on epr.EcommerceID=er.EcommerceID
                 inner join T_EcommerceGroup ergroup
                 on ergroup.EcommerceGroupID=epr.EcommerceGroupID where 1=1 and epr.IsTrunk={0}
                 and epr.ApprovalState={1} and epr.ProjectID='{2}'", istrunk,state,keyValue);
            //var expression = LinqExtensions.True<EcommerceProjectRelationEntity>();
            //expression = expression.And(t => t.IsTrunk == istrunk);
            //expression = expression.And(t => t.ApprovalState == state);
            //expression = expression.And(t => t.ProjectID == keyValue);
            return this.BaseRepository().FindList<EcommerceProjectRelationEntity>(sqlstr.ToString());
        }
        public IEnumerable<EcommerceProjectRelationEntity> GetProjectAndEcom()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select epr.ProjecName,er.EcommerceName from  T_EcommerceProjectRelation epr
                            inner join T_Ecommerce er on epr.EcommerceID=er.EcommerceID
                            where epr.DeleteMark=0 and epr.IsTrunk=1");
            return this.BaseRepository().FindList<EcommerceProjectRelationEntity>(sqlstr.ToString());
        }
        /// <summary>
        /// 获取列表
        /// </summary> 
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceProjectRelationEntity> GetList()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"select * from  T_EcommerceProjectRelation where DeleteMark=0 ");
            return this.BaseRepository().FindList<EcommerceProjectRelationEntity>(sqlstr.ToString());
        }
        public IEnumerable<EcommerceProjectRelationEntity> GetAllListByST(int state, int IsTrunk)
        {
            var expression = LinqExtensions.True<EcommerceProjectRelationEntity>();
            expression = expression.And(t => t.ApprovalState == state);
            expression = expression.And(t => t.IsTrunk == IsTrunk);
            return this.BaseRepository().IQueryable(expression);
        }
        /// <summary>

        /// 根据电商名集合查询出所有项目
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<EcommerceProjectRelationEntity> GetAllListByEcomNameList(string EcomNameStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  epr.[EcommerceProjectRelationID],epr.[EcommerceGroupID],epr.[EcommerceID],
            ecom.[EcommerceName] as EcommerceName,ecomGro.EcommerceGroupName 
                as EcommerceGroupName,epr.[PlatformRate],epr.[EcommerceType],
            epr.[EcommerceTypeName] ,[Agent] ,epr.[CooperateStartTime],epr.[CooperateEndTime],[ForceContractAmount],
            pr.[ProjectID],pr.[ProjecName],epr.[DeleteMark],pr.[ProjectCode] ,pr.[ProjectName],pr.[ProjectGeneralizeName]
            ,pr.[ProjectOfficialName],epr.[CompanyId] ,pr.[CompanyCode] ,pr.[CompanyName],pr.[CityID],pr.[CityCode],
            pr.[CityName] ,pr.[Address],pr.[PrincipleMan],epr.[ActualControlTotalAmount] ,epr.[ProjectType],
            epr.[FlowNopayTotalAmount],epr.[ControlTotalAmount],epr.[ApprovalState],epr.[Remark],epr.[Procinstid] ,
            epr.[LatestApprover],epr.[LatestComment],epr.[LatestApprovetime],epr.[EcommerceCode],epr.[IsTrunk] 
            ,pr.ProjectName FROM [E_Commerce_DB].[dbo].[T_EcommerceProjectRelation]
            epr inner join dbo.Base_ProjectInfo pr on epr.ProjectID=pr.ProjectID inner join [T_EcommerceGroup] 
            ecomGro on ecomGro.[EcommerceGroupID]=epr.[EcommerceGroupID] inner join T_Ecommerce
                ecom on ecom.EcommerceID=epr.EcommerceID where ecom.EcommerceName in('" + EcomNameStr + "')and  IsTrunk=1 and epr.DeleteMark=0 and epr.ApprovalState='4'");
            return this.BaseRepository().FindList<EcommerceProjectRelationEntity>(strSql.ToString());
        }
        /// 获取列表
        /// </summary>
        /// <param name="keyValue">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EcommerceProjectRelationEntity> GetList(string keyValue)
        {
            var expression = LinqExtensions.True<EcommerceProjectRelationEntity>();
            expression = expression.And(t => t.ProjectID == keyValue);
            return this.BaseRepository().IQueryable(expression);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EcommerceProjectRelationEntity GetEntity(string keyValue)
        {
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@EcommerceProjectRelationID", keyValue));
            return this.BaseRepository().FindEntity<EcommerceProjectRelationEntity>("select * from T_EcommerceProjectRelation where EcommerceProjectRelationID=@EcommerceProjectRelationID", parameter.ToArray());
        }
        ///<summary>
        ///作者：durry
        ///time:2018-06-29 10:06
        ///获取发起流程的url
        public IEnumerable<EcommerceProjectRelationView> GetStartUrl(string keyValue, string starturlname)
        {
            var expression = LinqExtensions.True<EcommerceProjectRelationEntity>();
            expression = expression.And(t => t.DeleteMark == 0).And(t => t.EcommerceProjectRelationID == keyValue);
            var predata = this.BaseRepository().FindList<EcommerceProjectRelationEntity>(expression);
            List<EcommerceProjectRelationView> eprviews = new List<EcommerceProjectRelationView>();
            foreach (var item in predata)
            {
                EcommerceProjectRelationView eprview = new EcommerceProjectRelationView();
                eprview.EcommerceProjectRelationID = keyValue;
                eprview.url = string.Format("{0}?BSID=EC_Contract_Add&BOID={1}&UserId={2}&Key={3}", starturlname, keyValue, item.Account, BpmMD5Helper.GetEnCodeStr(keyValue));
                if (!eprviews.Contains(eprview))
                {
                    eprviews.Add(eprview);
                }
            }
            return eprviews;
        }
        /// <summary>
        /// 获取主干实体
        /// </summary>
        /// <param name="ProjectId">项目ID</param>
        /// <param name="EcommerceId">电商ID</param>
        /// <returns></returns>
        public EcommerceProjectRelationEntity GetTrunkEntity(string ProjectId, string EcommerceId)
        {
            var expression = LinqExtensions.True<EcommerceProjectRelationEntity>();
            expression = expression.And(t => t.ProjectID == ProjectId);
            expression = expression.And(t => t.EcommerceID == EcommerceId);
            expression = expression.And(t => t.ApprovalState == 4);
            expression = expression.And(t => t.IsTrunk == 1);
            return this.BaseRepository().FindEntity<EcommerceProjectRelationEntity>(expression);
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
            //验证登录用户是否存在
            var userModel = userBll.CheckLogin(login_name);
            if (userModel == null)
            {
                throw new Exception("当前用户信息不存在!");

            }
            OutOnlieModel result = new OutOnlieModel();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                          select epr.EcommerceID	 as electricity_supplier_id,
                        er.EcommerceName as electricity_supplier_name,		
                        er.EcommerceCode as electricity_supplier_code,
                        epr.EcommerceGroupID as electricity_supplier_ab_id,
                        ergroup.EcommerceGroupName	 as electricity_supplier_ad,			
                        CONVERT(DECIMAL(18,2),ActualControlTotalAmount)	 as available_balance,
                        ProjectID as project_id,
                        ProjectID as projectid,
                        ProjectCode as project_code,
                        ProjecName		 as project_name,	
                        'CNY'		as currency_code	
                        from dbo.T_EcommerceProjectRelation epr
                        inner join T_Ecommerce er on 
                        epr.EcommerceID=er.EcommerceID
                        inner join t_ecommercegroup ergroup on
                        ergroup.EcommerceGroupID=epr.EcommerceGroupID
                        where epr.ApprovalState=@ApprovalState and epr.istrunk=1");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(electricity_supplier_name))
            {
                strSql.AppendLine(" and er.EcommerceName like @EcommerceName ");
                parameter.Add(DbParameters.CreateDbParameter("@EcommerceName", "%" + electricity_supplier_name + "%"));
            }
            if (!string.IsNullOrEmpty(electricity_supplier_ad))
            {
                strSql.AppendLine(" and ergroup.EcommerceGroupName like @EcommerceGroupName");
                parameter.Add(DbParameters.CreateDbParameter("@EcommerceGroupName", "%" + electricity_supplier_ad + "%"));
            }
            if (!string.IsNullOrEmpty(electricity_supplier_code))
            {
                strSql.AppendLine(" and er.EcommerceCode like @EcommerceCode");
                parameter.Add(DbParameters.CreateDbParameter("@EcommerceCode", "%" + electricity_supplier_code + "%"));
            }
            if (!string.IsNullOrEmpty(project_code))
            {
                strSql.AppendLine(" and epr.ProjectCode like @ProjectCode");
                parameter.Add(DbParameters.CreateDbParameter("@ProjectCode", "%" + project_code + "%"));
            }
            if (!string.IsNullOrEmpty(project_name))
            {
                strSql.AppendLine(" and epr.ProjectName like @ProjectName");
                parameter.Add(DbParameters.CreateDbParameter("@ProjectName", "%" + project_name + "%"));
            }
            if (available_balance_begin > 0)
            {
                strSql.AppendLine(" and epr.ActualControlTotalAmount >=@ActualControlTotalAmountBegin ");
                parameter.Add(DbParameters.CreateDbParameter("@ActualControlTotalAmountBegin", available_balance_begin));
            }
            if (available_balance_end > 0)
            {
                strSql.AppendLine(" and epr.ActualControlTotalAmount <=@ActualControlTotalAmountEnd ");
                parameter.Add(DbParameters.CreateDbParameter("@ActualControlTotalAmountEnd", available_balance_end));
            }
            parameter.Add(DbParameters.CreateDbParameter("@ApprovalState", (int)ApproveStatus.approved));
            var plist = this.BaseRepository().FindList<OutOnineMall>(strSql.ToString(), parameter.ToArray()).ToList();
            //var plist = new AuthorizeService<OutOnineMall>().FindList(strSql.ToString(), parameter.ToArray(),
            //      AuthorizeUserTypeEnum.LoginCode, SystemTypeEnum.WebApi, login_name, pagination);
            result.datalist = plist;
            result.pageInfo = new pageInfo()
            {
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                rows = pagination.rows,

            };
            return result;
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
            var expression = LinqExtensions.True<EcommerceProjectRelationEntity>();
            if (!string.IsNullOrEmpty(electricity_supplier_id))
            {
                expression = expression.And(t => t.EcommerceID == electricity_supplier_id);
            }
            if (!string.IsNullOrEmpty(electricity_supplier_code))
            {
                expression = expression.And(t => t.EcommerceCode == electricity_supplier_code);
            }
            if (locked_amount > 0)
            {
                expression = expression.And(t => t.ActualControlTotalAmount >= locked_amount);
            }
            if (!string.IsNullOrEmpty(project_id))
            {
                expression = expression.And(t => t.ProjectID == project_id);
            }
            if (!string.IsNullOrEmpty(project_code))
            {
                expression = expression.And(t => t.ProjectCode == project_code);
            }

            var model = this.BaseRepository().FindEntity<EcommerceProjectRelationEntity>(expression);
            return model == null ? false : true;

        }
        #endregion
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
                this.BaseRepository().Delete<EcommerceProjectRelationEntity>(keyValue);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="discountList"></param>
        /// <param name="uploadFiles"></param>
        /// <returns></returns>
        public string GetKeyValue(string keyValue, EcommerceProjectRelationEntity entity, List<EcommerceDiscountProgramEntity> discountList, List<FileModel> uploadFiles)
        {
            try
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    entity.ForceContractAmount = entity.ForceContractAmount;
                    db.Update(entity);
                }
                else
                {
                    entity.Create();
                    entity.ForceContractAmount = entity.ForceContractAmount;
                    db.Insert(entity);
                }
                try
                {
                    var parameter = new List<DbParameter>();
                    parameter.Add(DbParameters.CreateDbParameter("@EcommerceProjectRelationID", entity.EcommerceProjectRelationID));
                    db.ExecuteBySql("delete T_EcommerceDiscountProgram where EcommerceProjectRelationID=@EcommerceProjectRelationID", parameter.ToArray());
                    if (discountList != null)
                    {
                        foreach (EcommerceDiscountProgramEntity item in discountList)
                        {
                            item.Create();
                            item.EcommerceProjectRelationID = entity.EcommerceProjectRelationID;
                            this.BaseRepository().Insert(item);
                        }
                    }
                    attservice.MapingFiles(entity.EcommerceProjectRelationID, uploadFiles, db);
                    db.Commit();
                    return entity.EcommerceProjectRelationID;
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
        /// <summary>
        /// 保存表单（新增、修改）
        /// 增加编辑保存功能 作者：durry, time:2018-06-22 17:15
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, EcommerceProjectRelationEntity entity, List<EcommerceDiscountProgramEntity> discountList, List<FileModel> uploadFiles)
        {
            try
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    entity.ForceContractAmount = entity.ForceContractAmount;
                    db.Update(entity);
                }
                else
                {
                    entity.Create();
                    entity.ForceContractAmount = entity.ForceContractAmount;
                    db.Insert(entity);
                }
                try
                {
                    var parameter = new List<DbParameter>();
                    parameter.Add(DbParameters.CreateDbParameter("@EcommerceProjectRelationID", entity.EcommerceProjectRelationID));
                    db.ExecuteBySql("delete T_EcommerceDiscountProgram where EcommerceProjectRelationID=@EcommerceProjectRelationID", parameter.ToArray());
                    if (discountList != null)
                    {
                        foreach (EcommerceDiscountProgramEntity item in discountList)
                        {
                            item.Create();
                            item.EcommerceProjectRelationID = entity.EcommerceProjectRelationID;
                            this.BaseRepository().Insert(item);
                        }
                    }
                    attservice.MapingFiles(entity.EcommerceProjectRelationID, uploadFiles, db);

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
        /// <summary>
        /// 资金划拨后修改实际可支配金额和支配金额
        /// 作者 clare, time:2018-06-22 17:15
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void UpdateActAmount(string EcommerceID, string ProjectID, decimal FinalAmount, decimal FinalAll)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append(@"update T_EcommerceProjectRelation set");
            if (!FinalAmount.IsEmpty())
            {
                sqlstr.Append(" ActualControlTotalAmount=" + FinalAmount);
            }
            if (!FinalAll.IsEmpty())
            {
                sqlstr.Append(", ControlTotalAmount=" + FinalAll);
            }
            sqlstr.Append(" where IsTrunk=1 and EcommerceID='" + EcommerceID + "'" + " and ProjectID='" + ProjectID + "'");
            this.BaseRepository().ExecuteBySql(sqlstr.ToString());
            //
        }
        public void updateMoneyAA(List<EcommerceProjectRelationEntity> ecomList)
        {

            new SqlDatabase("BaseDb").Connection.Updateable(ecomList).UpdateColumns(p => new { p.ActualControlTotalAmount, p.ControlTotalAmount })
               .ExecuteCommand();
            //this.BaseRepository().Update<EcommerceProjectRelationEntity>(ecomList);

        }
        public IEnumerable<EcommerceProjectRelationEntity> searchActAmount(string EcommerceID, string ProjectID)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append(@"select * from T_EcommerceProjectRelation where where IsTrunk=1 and ApprovalState=4 and ");
            sqlstr.Append(" EcommerceID='" + EcommerceID + "'" + " and ProjectID='" + ProjectID + "'");
            return this.BaseRepository().FindList<EcommerceProjectRelationEntity>(sqlstr.ToString());
            //
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

                var model = this.BaseRepository().FindEntity<EcommerceProjectRelationEntity>(entity.EcommerceProjectRelationID);
                model.ApprovalState = entity.ApprovalState;
                model.IsTrunk = entity.IsTrunk;
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
        #endregion
    }
}
