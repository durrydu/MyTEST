using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Movit.Application.Code
{
    public enum BiddingMethodEnum
    {
        /// <summary>
        /// 邀请招标
        /// </summary>
        [Description("邀请招标")]
        invitebid=0,
        /// <summary>
        /// 简易招标
        /// </summary>
        [Description("简易招标")]
        simplebid = 1,
        /// <summary>
        /// 非集采类直接委托
        /// </summary>
        [Description("非集采类直接委托")]
        nocollectdirect = 2,
        /// <summary>
        /// 集团集采类直接委托
        /// </summary>
        [Description("集团集采类直接委托")]
        groupcollect = 3,
        /// <summary>
        /// 区域集采类直接委托
        /// </summary>
        [Description("区域集采类直接委托")]
        companycollect = 4,
        /// <summary>
        /// 地区集采类直接委托
        /// </summary>
        [Description("地区集采类直接委托")]
        regioncollect = 5,
        /// <summary>
        /// 行政收费
        /// </summary>
        [Description("行政收费")]
        admincharge = 6,
        /// <summary>
        /// 延续性服务
        /// </summary>
        [Description("延续性服务")]
        longservice = 7,
    }
}
