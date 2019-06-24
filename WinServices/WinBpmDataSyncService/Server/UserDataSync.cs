using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinServiceBase;

namespace WinBpmDataSyncService
{
    public class UserDataSync : ISyncData
    {
        public void Execute()
        {
            AutoDataSync();

        }
        private void AutoDataSync()
        {

            try
            {
                var IntervalTime = Convert.ToInt32(SettingHelper.Get_ConfigValue("Interval"));
                Log.Info(DateTime.Now.ToString() + "AutoDataSync运行正常");
            }
            catch (Exception ex)
            {
                Log.Error("AutoDataSync允许出错：" + ex.Message);

            }

        }
    }
}
