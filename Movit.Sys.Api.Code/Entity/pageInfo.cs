using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Sys.Api.Code
{
    public class pageInfo
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>

        public int page { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>

        public int records { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>

        public int total { get; set; }

    }
}
