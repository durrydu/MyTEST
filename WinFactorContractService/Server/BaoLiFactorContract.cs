using Movit.Application.Busines.AccountPayableManage;
using Movit.Application.Busines.BaseManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinServiceBase;

namespace WinFactorContractService
{
    public class BaoLiFactorContract : ISyncData
    {
        private AccountPayableInfomationBLL accountPayableBll = new AccountPayableInfomationBLL();
        public void Execute()
        {
            accountPayableBll.ExportWord();
        }
    }
}
