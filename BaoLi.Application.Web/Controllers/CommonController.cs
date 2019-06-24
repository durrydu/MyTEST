using Movit.Application.Code;
using Movit.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaoLi.Application.Web.Controllers
{
    /// <summary>
    /// 作者:姚栋
    /// 日期:2018.06.01
    /// 描述:公共帮助类
    /// </summary>
    public class CommonController : Controller
    {
        #region 获取枚举值
        /// <summary>
        /// 枚举值绑定下拉框
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [OutputCache(Duration = 30000)]
        public ActionResult GetEnumList(string type)
        {
            // 前台调用示例
            //$("#divTypeId").ComboBox({
            //    url: "../../EpcCommon/GetEnumList",
            //    param: { type: "IndicatoType" },
            //    id: "Value",
            //    text: "Description",
            //    description: "==请选择==",
            //    height: "100px",
            //    success: function (data) {

            //    }
            //});
            var DesInfo = EnumHelper.ToDescriptionDictionary<AuthorizeTypeEnum>();
            return Content(DesInfo.ToJson());
        }
        [HttpGet]
        [OutputCache(Duration = 30000)]
        public ActionResult GetAuthorizeTypEnumList()
        {

            var DesInfo = EnumHelper.ToDescriptionDictionary<AuthorizeTypeEnum>();
            return Content(DesInfo.ToJson());
        }

        /// <summary>
        /// 获取授权方式 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [OutputCache(Duration = 30000)]
        public ActionResult GetAuthorizationMethodEnumList()
        {

            var DesInfo = EnumHelper.ToDescriptionDictionary<AuthorizationMethodEnum>();
            return Content(DesInfo.ToJson());
        }

        #endregion
    }
}