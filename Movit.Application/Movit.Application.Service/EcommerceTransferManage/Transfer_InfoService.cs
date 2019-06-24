using Movit.Application.Entity.EcommerceTransferManage;
using Movit.Application.Entity.EcommerceTransferManage.ViewModel;
using Movit.Application.IService.EcommerceTransferManage;
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
using Movit.Application.Service.AuthorizeManage;
using Movit.Application.IService.SystemManage;
using Movit.Application.Service.SystemManage;
using Movit.Application.Code;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.IService.EcommerceContractManage;
using Movit.Application.Service.EcommerceContractManage;
using Movit.Application.IService.EcomPartnerCapitalPoolManage;
using Movit.Application.Service.EcomPartnerCapitalPoolManage;
using Movit.Application.Entity.EcomPartnerCapitalPoolManage;

namespace Movit.Application.Service.EcommerceTransferManage
{

    /// <summary>
    ///  
    /// Copyright (c) 2015-201盟拓软件(苏州)
    /// 创 建：超级管理员
    /// 日 期：2018-06-25 14:48
    /// 描 述：T_Transfer_Info
    /// </summary>
    public class Transfer_InfoService : RepositoryFactory, ITransfer_InfoService
    {
        public T_PartnerCapitalPoolIService patnerCapPook = new T_PartnerCapitalPoolService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Transfer_InfoEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Transfer_InfoEntity>();
            var queryParam = queryJson.ToJObject();

            if (!queryParam["Transfer_DateBegin"].IsEmpty())
            {
                var Transfer_DateBegin = DateTime.Parse(queryParam["Transfer_DateBegin"].ToString());
                expression = expression.And(t => t.Transfer_Date >= Transfer_DateBegin);

            }
            if (!queryParam["Transfer_DateEnd"].IsEmpty())
            {
                var Transfer_DateEnd = DateTime.Parse(queryParam["Transfer_DateEnd"].ToString());
                expression = expression.And(t => t.Transfer_Date <= Transfer_DateEnd);

            }
            expression = expression.And(t => t.DeleteMark == 0);
            return this.BaseRepository().IQueryable<Transfer_InfoEntity>(expression).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Transfer_InfoEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Transfer_InfoEntity>(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Transfer_Project_EcommerceView GetProEcomJson(string keyValue)
        {
            var result = new Transfer_Project_EcommerceView();
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.Append(@"select a.CreateDate,a.Transfer_Code,c.ProjecName,a.Transfer_Info_Id,
                         b.EcommerceGroupName as EcommerceGroupName ,a.ProjectID,c.CompanyName,
                         a.Transfer_Money,a.Transfer_Date,a.Transfer_Title,a.CreateUserName 
                         from T_Transfer_Info a left join T_Ecommerce b on b.EcommerceID=a.EcommerceID 
                         left join Base_ProjectInfo c on c.ProjectID=a.ProjectID WHERE 1=1 and a.DeleteMark=0 and a.Transfer_Info_Id=@Transfer_Info_Id");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@Transfer_Info_Id", keyValue));
            var plist = this.BaseRepository().FindList<Transfer_Project_EcommerceView>(sqlstr.ToString(), parameter.ToArray());
            if (plist != null && plist.Count() > 0)
            {

                result = plist.FirstOrDefault();
            }
            return result;
        }
        /// <summary>
        /// 获取页面分页信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Transfer_Project_EcommerceView> GetPageListJson(string keyValue, Pagination pagination, string queryJson)
        {
            StringBuilder sqlstr = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            sqlstr.Append(@"select a.CreateDate,a.Transfer_Code,c.ProjecName,a.Transfer_Info_Id,
                         b.EcommerceGroupName as EcommerceGroupName ,a.ProjectID as ProjectID,c.CompanyName,b.EcommerceID as EcommerceID,
                         a.Transfer_Money,a.Transfer_Date,a.Transfer_Title,a.CreateUserName 
                         from T_Transfer_Info a left join T_Ecommerce b on b.EcommerceID=a.EcommerceID 
                         left join Base_ProjectInfo c on c.ProjectID=a.ProjectID WHERE 1=1 and a.DeleteMark=0");
            if (!queryParam["C_Name"].IsEmpty())
            {
                sqlstr.Append(" and b.EcommerceGroupName like '%" + queryParam["C_Name"].ToString() + "%'");
            }
            if (!queryParam["Company_Name"].IsEmpty())
            {
                sqlstr.Append(" and c.CompanyCode='" + queryParam["Company_Name"].ToString() + "'");
            }
            if (!queryParam["Project_Name"].IsEmpty())
            {
                sqlstr.Append(" and c.ProjectID='" + queryParam["Project_Name"].ToString() + "'");
            }
            if (!queryParam["Transfer_Createtime"].IsEmpty())
            {
                sqlstr.Append(" and a.Transfer_Date>='" + queryParam["Transfer_Createtime"].ToString() + "'");
            }
            if (!queryParam["Transfer_EndTime"].IsEmpty())
            {
                sqlstr.Append(" and a.Transfer_Date<='" + queryParam["Transfer_EndTime"].ToString() + "'");
            }
            return new AuthorizeService<Transfer_Project_EcommerceView>().FindList(sqlstr.ToString(), pagination, "ProjectID");
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteRemark(string keyValue, string queryJson, string ProjectID, string Transfer_Money, string EcommerceID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                string queryJson1 = ProjectID;
                string queryValue = EcommerceID;
                IEnumerable<EcommerceProjectRelationEntity> ecomPro = projectInfo.GetMoneyByEconmProjectJson(queryJson1, queryValue);
                decimal Amount = ecomPro.First().ActualControlTotalAmount;
                decimal AllAmount = ecomPro.First().ControlTotalAmount;
                decimal Money ;
                if (!Transfer_Money.IsEmpty() || Transfer_Money != "")
                {
                    Money = decimal.Parse(Transfer_Money);
                }
                else {
                    Money = 0;
                }
                
                decimal FinalAmount = Amount + Money;
                decimal FinalAll = AllAmount + Money;
                proRelation.UpdateActAmount(EcommerceID, ProjectID, FinalAmount, FinalAll);
                strSql.Append(@" update T_Transfer_Info set DeleteMark=1,Remark='" + queryJson + "' where Transfer_Info_Id='" + keyValue + "'");
                this.BaseRepository().ExecuteBySql(strSql.ToString());
                string value = string.Empty;
                IEnumerable<T_PartnerCapitalPoolEntity> paCaPoolList = patnerCapPook.getPaCaPoolList(keyValue);
                T_PartnerCapitalPoolEntity pcEntityzzz = new T_PartnerCapitalPoolEntity();
                pcEntityzzz = paCaPoolList.First();
                pcEntityzzz.DeleteMark = 1;
                //if (paCaPoolList.FirstOrDefault() == null)
                //{
                T_PartnerCapitalPoolEntity pcEntity = new T_PartnerCapitalPoolEntity();
                pcEntity.EcommerceProjectRelationID = ecomPro.First().EcommerceProjectRelationID;
                pcEntity.OperationTitle = "资金划拨";
                pcEntity.T_P_PartnerCapitalPoolID = paCaPoolList.First().PartnerCapitalPoolID;
                pcEntity.OperationType = 0;
                pcEntity.OperationMoney = Money;
                pcEntity.CurrentMoney = AllAmount;
                pcEntity.CurrentBalance = FinalAll;
                pcEntity.AccountingType = 0;
                pcEntity.ObjectID = keyValue;
                pcEntity.StatisticalDate = DateTime.Now;
                pcEntity.DeleteMark = 1;
                patnerCapPook.SaveForm(value, pcEntity);
                patnerCapPook.SaveForm(paCaPoolList.First().PartnerCapitalPoolID, pcEntityzzz);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<Transfer_InfoEntity>(keyValue);
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
        /// 
        private ICodeRuleService coderuleService = new CodeRuleService();
        private Base_ProjectInfoIService projectInfo = new Base_ProjectInfoService();
        private IEcommerceProjectRelationService proRelation = new EcommerceProjectRelationService();
        public void SaveForm(string keyValue, Transfer_InfoEntity entity, string ProjectID, string EcommerceID, string ActualControlTotalAmount,string Transfer_Code,out string errMsg)
        {
            errMsg = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    string queryJson = ProjectID;
                    string queryValue = EcommerceID;

                    IEnumerable<EcommerceProjectRelationEntity> ecomPro = projectInfo.GetMoneyByEconmProjectJson(queryJson, queryValue);
                    decimal Amount = ecomPro.First().ActualControlTotalAmount;
                    decimal AllAmount = ecomPro.First().ControlTotalAmount;
                    decimal Money = decimal.Parse(ActualControlTotalAmount);
                    if (Amount >= Money)
                    {
                        decimal FinalAmount = Amount - Money;
                        decimal FinalAll = AllAmount - Money;
                        proRelation.UpdateActAmount(EcommerceID, ProjectID, FinalAmount, FinalAll);
                        entity.Transfer_Code = Transfer_Code;
                        entity.Transfer_Info_Id = Guid.NewGuid().ToString();
                        entity.CreateDate = DateTime.Now;
                        entity.DeleteMark = 0;
                        entity.Transfer_Money = Money;
                        entity.Transfer_Balance = FinalAmount;
                        entity.Transfer_Date = DateTime.Now;
                        entity.CreateUserId = OperatorProvider.Provider.Current().UserId;
                        entity.CreateUserName = OperatorProvider.Provider.Current().UserName;
                        this.BaseRepository().Insert(entity);
                            string value = string.Empty;
                            T_PartnerCapitalPoolEntity pcEntity = new T_PartnerCapitalPoolEntity();
                            pcEntity.EcommerceProjectRelationID = ecomPro.First().EcommerceProjectRelationID;
                            pcEntity.OperationTitle = "资金划拨";
                            pcEntity.T_P_PartnerCapitalPoolID = null;
                            pcEntity.OperationType = 1;
                            pcEntity.OperationMoney = Money;
                            pcEntity.CurrentMoney = AllAmount;
                            pcEntity.CurrentBalance = FinalAll;
                            pcEntity.AccountingType = 1;
                            pcEntity.DeleteMark = 0;
                            pcEntity.StatisticalDate = DateTime.Now;
                            pcEntity.ObjectID = entity.Transfer_Info_Id;
                            patnerCapPook.SaveForm(value, pcEntity);
                    }
                    else
                    {
                        errMsg = "可以金额已经发生变化，请重写划拨！";
                    }
                   
                } 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
