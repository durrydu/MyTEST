using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Movit.Application.Code
{
   public enum ContractNatureEnum
    {
       /// <summary>
        /// 直接合同
       /// </summary>
       [Description("直接合同")]
       direct=0,
       /// <summary>
       /// 三方合同
       /// </summary>
       [Description("三方合同")]
       third=1,
    }
}
