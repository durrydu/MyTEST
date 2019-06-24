using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.CapitalFlowManage.ViewModel
{
    public class IncomeDetail
    {
        public DataTable mx { get; set; }

        public List<xiangmuxiaoji> xiangmuxiaoji { get; set; }
        public quyuzongji quyuzongji { get; set; }
    }
    public class xiangmuxiaoji
    {
        public string title { get; set; }
        public decimal qian { get; set; }
        public string projectId { get; set; }
    }
    public class quyuzongji
    {
        public string title { get; set; }
        public decimal qian { get; set; }
    }
}
