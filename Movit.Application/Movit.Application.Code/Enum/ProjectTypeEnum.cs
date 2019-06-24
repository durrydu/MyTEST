using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Movit.Application.Code
{
    public enum ProjectTypeEnum
    {
        [Description("独资")]
        solo=0,
        [Description("合资")]
        together=1,
        [Description("代建代销")]
        help=2,
    }
}
