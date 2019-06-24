using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoLi.Application.Web
{

    /// <summary>
    /// 用于标记需要哪些配置项
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
    public class DependAppSettingsAttribute : System.Attribute
    {
        /// <summary>
        /// 配置项
        /// </summary>
        /// <returns></returns>
        public string ConfigKey { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }

        /// <summary>
        /// 可以选择的值
        /// </summary>
        /// <returns></returns>
        public string OptionalValues { get; set; }


        /// <summary>
        /// 赋值的示例
        /// </summary>
        /// <returns></returns>
        public string Sample { get; set; }


        public DependAppSettingsAttribute()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configKey">配置项的键名</param>
        /// <param name="description">配置项的描述</param>
        public DependAppSettingsAttribute(string configKey, string description)
        {
            this.ConfigKey = configKey;
            this.Description = description;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="configKey">配置项的键名</param>
        /// <param name="description">配置项的描述</param>
        /// <param name="optionalValues">配置项可以选择的值</param>
        public DependAppSettingsAttribute(string configKey, string description, string optionalValues)
        {
            this.ConfigKey = configKey;
            this.Description = description;
            this.OptionalValues = optionalValues;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="configKey">配置项的键名</param>
        /// <param name="description">配置项的描述</param>
        /// <param name="optionalValues">配置项可以选择的值</param>
        public DependAppSettingsAttribute(string configKey, string description, string optionalValues, string sample)
        {
            this.ConfigKey = configKey;
            this.Description = description;
            this.OptionalValues = optionalValues;
            this.Sample = sample;
        }

    }
}
