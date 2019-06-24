using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movit.Application.Entity.CapitalFlowManage.ViewModel
{
    public class CapitalFlow_ProRelaView
    {
        public string ProjectID{get;set;}
        public string EcommerceID { get; set; }
        public decimal CapitalPoolAdd { get; set; }
        public decimal ControlTotalAmount { get; set; }
        public decimal ActualControlTotalAmount { get; set; }
        public string EcommerceProjectRelationID { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public string CapitalFlow_Details_Id { get; set; }
        public DateTime? UploadDate { get; set; }
    }
}
