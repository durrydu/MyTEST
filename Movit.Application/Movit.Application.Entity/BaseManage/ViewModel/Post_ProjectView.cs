using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.BaseManage
{
    /// <summary>
    /// 用户项目权限视图
    /// </summary>
    [SugarTable("view_post_project")]
    public class Post_ProjectView
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string Account { get; set; }
    }
}
