﻿//=====================================================================================
// All Rights Reserved , Copyright © movit 2018
//=====================================================================================

namespace Movit.Util
{
    /// <summary>
    /// MD5加密帮助类
    /// 版本：2.0
    /// <author>
    ///		<name>ivan</name>
    ///		<date>2018.09.27</date>
    /// </author>
    /// </summary>
    public class Md5Helper
    {
        #region "MD5加密"
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string MD5(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }

            if (code == 32)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }

            return strEncrypt;
        }
        #endregion
    }
}
