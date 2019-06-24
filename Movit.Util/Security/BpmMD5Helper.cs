using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Util
{
    public class BpmMD5Helper
    {
        /// <summary>
        /// 加密地址连接字符串预防与系统无关人查看k2页面
        /// </summary>
        /// <param name="str">流程实例ID或业务数据ID</param>
        /// <returns>加密后的值</returns>
        public static string GetEnCodeStr(string strKey)
        {
            string enStr = string.Empty;
            if (!string.IsNullOrEmpty(strKey))
            {
                enStr = GetMD5Str(strKey + DateTime.Now.ToString("yyyy-MM-dd") + "K2", 32);
            }
            return enStr;
        }

        public static string GetMD5Str(string str, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符）   
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            else//32位加密   
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
        }

    }
}
