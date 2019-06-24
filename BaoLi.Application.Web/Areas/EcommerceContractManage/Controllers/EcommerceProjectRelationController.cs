using Movit.Application.Busines;
using Movit.Application.Cache;
using Movit.Application.Code;
using Movit.Application.Entity;
using Movit.Application.Entity.BaseManage;
using Movit.Application.Entity.EcommerceContractManage;
using Movit.Util;
using Movit.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.EcommerceContractManage.Controllers
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-201盟拓软件(苏州)
    /// 创 建：emily
    /// 日 期：2018-06-19 11:08
    /// 描 述：T_EcommerceProjectRelation
    /// </summary>
    public class EcommerceProjectRelationController : MvcControllerBase
    {
        private EcommerceProjectRelationBLL ecommerceprojectrelationbll = new EcommerceProjectRelationBLL();
        private Base_ProjectInfoBLL bpibll = new Base_ProjectInfoBLL();
        private OP_ParcelBLL op_parcelbll = new OP_ParcelBLL();
        private DataItemCache dataItemCache = new DataItemCache();
        private EcommerceBLL ecombll = new EcommerceBLL();
        private EcommerceGroupBLL ecomgroupbll = new EcommerceGroupBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form(string keyValue)
        {
            ViewBag.keyValue = keyValue;
            return View();
        }
        /// <summary>
        /// 详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(string keyValue)
        {
            ViewBag.keyValue = keyValue;
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取项目属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [OutputCache(Duration = 30000)]
        public ActionResult GetProjectTypeEnum()
        {
            var DesInfo = EnumHelper.ToDescriptionList<ProjectTypeEnum>();
            return ToJsonResult(DesInfo);
        }
        /// <summary>
        /// 获取合同性质
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [OutputCache(Duration = 30000)]
        public ActionResult GetContractTypeEnum()
        {
            var DesInfo = EnumHelper.ToDescriptionList<ContractNatureEnum>();
            return ToJsonResult(DesInfo);
        }
        /// <summary>
        /// 获取招标方法
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [OutputCache(Duration = 30000)]
        public ActionResult GetBiddingMethodeEnum()
        {
            var DesInfo = EnumHelper.ToDescriptionList<BiddingMethodEnum>();
            return ToJsonResult(DesInfo);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var geturl = dataItemCache.GetDataItemByCodeAndName("SysConfig", "BPMApplicateRoad");
            string urlname = geturl.ItemValue;
            var data = ecommerceprojectrelationbll.GetPageList(pagination, queryJson, urlname);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取电商公司下拉
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEcoNameJson(string keyValue)
        {
            var data = ecommerceprojectrelationbll.GetList(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取审批通过的列表
        /// </summary>
        /// <param name="keyValue">查询参数</param>
        /// <returns>返回列表</returns>
        [HttpGet]
        public ActionResult GetListJson(string keyValue)
        {
            int istrunk = 1;
            var data = ecommerceprojectrelationbll.GetList(keyValue, (int)ApproveStatus.approved, istrunk);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取未审批的列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetNoApplicateJson(string keyValue)
        {
            int istrunk = 0;
            var data = ecommerceprojectrelationbll.GetList(keyValue, (int)ApproveStatus.in_approval, istrunk);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = ecommerceprojectrelationbll.GetEntity(keyValue);
            var ecomentity = ecombll.GetEntity(data.EcommerceID);
            var ecomgroupentity = ecomgroupbll.GetEntity(data.EcommerceGroupID);
            data.EcommerceName = ecomentity.EcommerceName;
            data.EcommerceGroupName = ecomgroupentity.EcommerceGroupName;
            return ToJsonResult(data);
        }
        [HttpGet]
        public ActionResult GetExistedContract(string projectid, string ecommerceid, string keyValue)
        {
            var data = ecommerceprojectrelationbll.GetList().ToList();
            if (keyValue != null && keyValue != "")
            {
                var item = data.Where(p => p.EcommerceProjectRelationID.Contains(keyValue)).FirstOrDefault();
                bool flag = data.Remove(item);
            }
            bool isExistedContract = false;
            bool draft = data.Any(t => t.EcommerceID == ecommerceid && t.ProjectID == projectid && t.ApprovalState == 1);
            bool approve = data.Any(t => t.EcommerceID == ecommerceid && t.ProjectID == projectid && t.ApprovalState == 4);
            if (!draft && !approve)
            {
                isExistedContract = false;
            }
            else
            {
                isExistedContract = true;
            }
            return ToJsonResult(isExistedContract);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            try
            {
                ecommerceprojectrelationbll.RemoveForm(keyValue);
                return Success("删除成功。");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 提交表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SubmitForm(string keyValue, EcommerceProjectRelationEntity entity, List<EcommerceDiscountProgramEntity> discountList, List<FileModel> uploadFiles)
        {
            try
            {

                var dataedad = ecommerceprojectrelationbll.GetList();
                foreach (var item in dataedad)
                {
                    if (item.EcommerceID == entity.EcommerceID && item.ProjectID == entity.ProjectID
                        && item.ApprovalState == (int)ApproveStatus.in_approval)
                        return Error("此项目和此电商已经有流程在审核中，不允许重复提交!");
                }
                //判断所选择的项目不存在相应地块
                OP_ParcelEntity op_parcelEntity = op_parcelbll.GetEntityByproid(entity.ProjectID);
                if (op_parcelEntity == null)
                {
                    return Error("此项目不存在对应的地块信息!");
                }
                var projectdata = bpibll.GetEntityBase(entity.ProjectID);
                var ecomdata = ecombll.GetEntity(entity.EcommerceID);
                entity.EcommerceCode = ecomdata.EcommerceCode;
                entity.ProjectCode = projectdata.ProjectCode;
                entity.CityCode = projectdata.CityCode;
                entity.CityID = projectdata.CityID;
                entity.CityName = projectdata.CityName;
                if (string.IsNullOrEmpty(keyValue))
                {
                    keyValue = ecommerceprojectrelationbll.GetKeyValue(keyValue, entity, discountList, uploadFiles);
                }
                else
                {
                    entity.EcommerceProjectRelationID = keyValue;
                    string key = ecommerceprojectrelationbll.GetKeyValue(keyValue, entity, discountList, uploadFiles);
                }
                ///作者：durry
                ///time:2018-06-29 10:06
                ///获取发起流程的url
                var starturl = dataItemCache.GetDataItemByCodeAndName("SysConfig", "BPMStartProcess");
                var starturlname = starturl.ItemValue;
                var data = ecommerceprojectrelationbll.GetStartUrl(keyValue, starturlname);
                return Success("操作成功。", data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, EcommerceProjectRelationEntity entity, List<EcommerceDiscountProgramEntity> discountList, List<FileModel> uploadFiles)
        {
            var projectdata = bpibll.GetEntityBase(entity.ProjectID);
            var ecomdata = ecombll.GetEntity(entity.EcommerceID);
            entity.EcommerceCode = ecomdata.EcommerceCode;
            entity.ProjectCode = projectdata.ProjectCode;
            entity.CityCode = projectdata.CityCode;
            entity.CityID = projectdata.CityID;
            entity.CityName = projectdata.CityName;
            ecommerceprojectrelationbll.SaveForm(keyValue, entity, discountList, uploadFiles);
            return Success("操作成功");
        }
        #endregion
    }
}