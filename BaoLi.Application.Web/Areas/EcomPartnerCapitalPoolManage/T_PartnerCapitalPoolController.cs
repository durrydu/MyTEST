using Movit.Application.Busines.EcomPartnerCapitalPoolManage;
using Movit.Application.Entity.EcomPartnerCapitalPoolManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.EcomPartnerCapitalPoolManage
{
   
        /// <summary>
        ///  
        /// Copyright (c) 2013-201盟拓软件(苏州)
        /// 创 建：超级管理员
        /// 日 期：2018-07-19 15:28
        /// 描 述：T_PartnerCapitalPool
        /// </summary>
        public class T_PartnerCapitalPoolController : MvcControllerBase
        {
            private T_PartnerCapitalPoolBLL t_partnercapitalpoolbll = new T_PartnerCapitalPoolBLL();

            #region 视图功能
            /// <summary>
            /// 列表页面
            /// </summary>
            /// <returns></returns>
            [HttpGet]
            public ActionResult T_PartnerCapitalPoolIndex()
            {
                return View();
            }
            /// <summary>
            /// 表单页面
            /// </summary>
            /// <returns></returns>
            [HttpGet]
            public ActionResult T_PartnerCapitalPoolForm()
            {
                return View();
            }
            #endregion

            #region 获取数据
            /// <summary>
            /// 获取列表
            /// </summary>
            /// <param name="queryJson">查询参数</param>
            /// <returns>返回列表Json</returns>
            [HttpGet]
            public ActionResult GetListJson(string queryJson)
            {
                var data = t_partnercapitalpoolbll.GetList(queryJson);
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
                var data = t_partnercapitalpoolbll.GetEntity(keyValue);
                return ToJsonResult(data);
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
                    t_partnercapitalpoolbll.RemoveForm(keyValue);
                    return Success("删除成功。");
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
            [HttpPost]
            [ValidateAntiForgeryToken]
            [AjaxOnly]
            public ActionResult SaveForm(string keyValue, T_PartnerCapitalPoolEntity entity)
            {
                try
                {
                    t_partnercapitalpoolbll.SaveForm(keyValue, entity);
                    return Success("操作成功。");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            #endregion
        }
    
}