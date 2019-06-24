using Movit.Application.Code;
using Movit.Application.Entity;
using Movit.Application.Interface;
using Movit.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movit.Util;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Application.Service.EcommerceContractManage;
using Movit.Application.Service.SystemManage;
using Movit.Application.IService.SystemManage;

namespace Movit.Application.Service.MoneyManager
{
    /// <summary>
    /// 付款单基础类
    /// 作者:姚栋
    /// 日期:20180627
    /// </summary>
    public class PayToolsBase : InterfacePay
    {
        private Base_ProjectInfoService projectService = new Base_ProjectInfoService();
        private T_Pay_Info_DetailsService payDeailsService = new T_Pay_Info_DetailsService();
        private EcommerceProjectRelationService eprService = new EcommerceProjectRelationService();
        private Pay_InfoService payInfoService = new Pay_InfoService();
        private ICodeRuleService coderuleService = new CodeRuleService();
        #region 属性
        #region 需要处理的付款单实体
        private Pay_InfoEntity _inputPayEntity;
        /// <summary>
        /// 付款单实体
        /// </summary>
        internal Pay_InfoEntity inputPayEntity
        {


            get
            {
                return _inputPayEntity;
            }
        }
        #endregion
        #region 现有付款单实体
        private Pay_InfoEntity _existingPayEntity;
        /// <summary>
        /// 现有付款单实体
        /// </summary>
        internal Pay_InfoEntity existingPayEntity
        {

            get
            {
                return _existingPayEntity;
            }
        }
        #endregion
        #region 最新一条流水记录
        private T_Pay_Info_DetailsEntity _lastPayInfoDetailsEntity;
        /// <summary>
        /// 最新一条流水记录
        /// </summary>
        internal T_Pay_Info_DetailsEntity lastPayInfoDetailsEntity
        {

            get
            {
                return _lastPayInfoDetailsEntity;
            }
        }
        #endregion

        #region 电商与项目的资金池信息
        private EcommerceProjectRelationEntity _ecommerceProjectMoneyCapacity;
        /// <summary>
        /// 电商与项目的资金池信息
        /// </summary>
        internal EcommerceProjectRelationEntity EcommerceProjectMoneyCapacity
        {

            get
            {
                return _ecommerceProjectMoneyCapacity;
            }
        }
        #endregion
        #region 需要处理的金额
        /// <summary>
        /// 需要处理的金额
        /// </summary>
        internal decimal operationAmount
        {

            get
            {
                decimal money = 0;
                if (inputPayEntity != null)
                {
                    money = inputPayEntity.Pay_Money ?? 0;
                }
                return money;
            }
        }
        #endregion
        #region 流水单号
        private string _codeNumber;
        /// <summary>
        /// 电商与项目的资金池信息
        /// </summary>
        internal string CodeNumber
        {

            get
            {
                return _codeNumber;
            }
        }
        #endregion
        #region 项目信息
        private Base_ProjectInfoEntity _projectInfoEntity;
        /// <summary>
        /// 最新一条流水记录
        /// </summary>
        internal Base_ProjectInfoEntity projectInfoEntity
        {

            get
            {
                return _projectInfoEntity;
            }
        }
        #endregion

        /// <summary>
        /// 数据库上下文 保证 操作要么一起成功要么一起失败,保证资金不混乱
        /// </summary>
        internal IRepository Transdb;

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputPayEntity"></param>
        /// <param name="existingPayEntity"></param>
        /// <param name="lastPayInfoDetailsEntity"></param>
        /// <param name="db"></param>
        public PayToolsBase(Pay_InfoEntity entity)
        {

            this._inputPayEntity = entity;
            #region 为了保证下面的数据都是一致的所以这里把该查的数据都一起查出来
            //项目信息
            var projectInfo = new Base_ProjectInfoEntity();


            if (entity.Project_Id != null)
            {
                _projectInfoEntity = projectService.GetEntityBase(entity.Project_Id);
            }
            if (_projectInfoEntity == null)
            {
                throw new Exception("项目信息不正确!");

            }
            //数据库存储的付款单
            this._existingPayEntity = payInfoService.GetEntity(entity.Pay_Info_Id);
            if (existingPayEntity != null)
            {
                this._lastPayInfoDetailsEntity = payDeailsService.GetEntityByCode(existingPayEntity.LastPayInfoDetailsCode);

            }
            this._ecommerceProjectMoneyCapacity = eprService.GetTrunkEntity(entity.Project_Id, entity.EcommerceID);
            if (_ecommerceProjectMoneyCapacity == null)
            {
                throw new Exception("未找到对应的电商合同!");

            }
            _codeNumber = coderuleService.SetBillCodeByCode("System", "100");
            #endregion

            try
            {

                string errMsg = string.Empty;

                if (existingPayEntity != null)
                {
                   
                    inputPayEntity.Modify(inputPayEntity.Pay_Info_Id);
                    new RepositoryFactory().BaseRepository().Update(inputPayEntity);
                }
                else
                {
                    inputPayEntity.CompanyID = projectInfoEntity.CompanyCode;
                    inputPayEntity.CompanyName = projectInfoEntity.CompanyName;
                    inputPayEntity.Create();
                    new RepositoryFactory().BaseRepository().Insert(inputPayEntity);
                }

                Transdb = new RepositoryFactory().BaseRepository().BeginTrans();
            }
            catch (Exception ex)
            {
                Transdb.Rollback();
                throw new Exception(ex.Message);
            }
        }


        public bool Check(out string errMsg)
        {
            errMsg = string.Empty;
            return true;
        }

        public void Execute()
        {

        }


        public void AdjustTheFundsPool()
        {

        }

        public void GenerateFlowOrders()
        {

        }
    }
}
