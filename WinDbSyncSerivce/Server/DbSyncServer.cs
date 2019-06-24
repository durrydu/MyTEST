using Movit.Application.Busines;
using Movit.Application.Busines.BaseManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WinServiceBase;

namespace WinDbSyncSerivce
{

    /// <summary>
    /// 保理项目数据同步服务
    /// 作者:姚栋
    /// 日期:2018.06.04
    /// </summary>
    public class DbSyncServer : ISyncData
    {
        private UserBLL userBll = new UserBLL();
        private Base_ProjectInfoBLL projectBll = new Base_ProjectInfoBLL();
        private DepartmentBLL depBll = new DepartmentBLL();
        private ProvideBLL provideBll = new ProvideBLL();
        private PU_SupplierBankBLL supBankBll = new PU_SupplierBankBLL();
        public void Execute()
        {
            try
            {
                #region 同步用户
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncNewUser=>Strat");
                userBll.SyncNewUser();
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncNewUser=>End");
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncUpdateUser=>Strat");
                userBll.SyncUpdateUser();
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncUpdateUser=>End");
                #endregion

                #region 同步部门
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncNewDep=>Strat");
                depBll.SyncNewDep();
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncNewDep=>End");
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncUpdateDep=>Strat");
                depBll.SyncUpdateDep();
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncUpdateDep=>End");
                #endregion

                #region 同步项目
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncNewProject=>Strat");
                projectBll.SyncNewProject();
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncNewProject=>End");
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncUpdateProject=>Strat");
                projectBll.SyncUpdateProject();
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncUpdateProject=>End");
                #endregion

                #region 同步供应商
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncNewProvide=>Strat");
                provideBll.SyncNewProvide();
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncNewProvide=>End");
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncUpdateProvide=>Strat");
                provideBll.SyncUpdateProvide();
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncUpdateProvide=>End");

                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncNewPU_SupplierBankService=>Strat");
                supBankBll.SyncNewPU_SupplierBankService();
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncNewPU_SupplierBankService=>End");
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncUpdatePU_SupplierBankService=>Strat");
                supBankBll.SyncUpdatePU_SupplierBankService();
                Log.Info(DateTime.Now.ToString() + "DbSyncServer=>Execute=>SyncUpdatePU_SupplierBankService=>End");
                #endregion

                

            }
            catch (Exception ex)
            {

                Log.Error(DateTime.Now.ToString() + "DbSyncServer=>Execute>=Exception:" + ex.Message);
            }


        }

    }
}
