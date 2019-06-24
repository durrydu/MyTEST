using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Interface
{
    /// <summary>
    /// 标记需要依赖注入的项
    /// 作者：姚栋
    /// 日期：20160504
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
    public class DependInjectionAttribute : System.Attribute
    {
        /// <summary>
        /// 需要注入的接口类型
        /// </summary>
        /// <returns></returns>
        public System.Type InterfaceType { get; set; }

        /// <summary>
        /// 描述信息,比如接口的用途等
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }

        /// <summary>
        /// 默认注入的类型
        /// </summary>
        /// <returns></returns>
        public string DefaultType { get; set; }

        /// <summary>
        /// 标记对依赖注入的使用
        /// </summary>
        public DependInjectionAttribute()
        {

        }


        /// <summary>
        /// 标记对依赖注入的使用
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="desc"></param>
        /// <param name="defaultType"></param>
        public DependInjectionAttribute(System.Type interfaceType, string desc = "", string defaultType = "")
        {
            this.InterfaceType = interfaceType;
            this.Description = desc;
            this.DefaultType = defaultType;
        }

        /// <summary>
        /// 标记对依赖注入的使用
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="defaultType"></param>
        /// <param name="desc"></param>
        public DependInjectionAttribute(System.Type interfaceType, System.Type defaultType, string desc = "")
        {
            this.InterfaceType = interfaceType;
            this.Description = desc;
            if (defaultType != null)
            {
                this.DefaultType = defaultType.FullName;
            }
            else
            {
                this.DefaultType = "";
            }
        }

    }
}
