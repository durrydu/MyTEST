using Movit.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Code
{
    /// <summary>
    ///  
    /// Copyright (c) 2018-2016  
    /// 创建人：姚栋
    /// 日 期：2015.11159 10:45
    /// 描 述：系统信息
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// 当前Tab页面模块Id
        /// </summary>
        public static string CurrentModuleId
        {
            get
            {
                return WebHelper.GetCookie("currentmoduleId");
            }
        }
        /// <summary>
        /// 当前登录用户Id
        /// </summary>
        public static string CurrentUserId
        {
            get
            {
                return OperatorProvider.Provider.Current().UserId;
            }
        }
        /// <summary>
        /// 当前登录用户账号
        /// </summary>
        public static string LoginCode
        {
            get
            {
                return OperatorProvider.Provider.Current().Account;
            }
        }
    }
}
