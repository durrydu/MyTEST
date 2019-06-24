using Movit.Application.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoLi.Application.Web.Areas.GeneratorManage.Controllers
{
    /// <summary>
    ///  
    /// Copyright (c) 2013-2016   
    /// 创建人：姚栋
    /// 日 期：2016.1.11 14:29
    /// 描 述：模板管理
    /// </summary>
    public class TemplateController : MvcControllerBase
    {
        /// <summary>
        /// 模板列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 单表生成器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SingleTable()
        {
            return View();
        }
        /// <summary>
        /// 多表生成器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MultiTable()
        {
            return View();
        }
    }
}
