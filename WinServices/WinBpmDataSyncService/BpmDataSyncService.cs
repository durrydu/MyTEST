using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WinServiceBase;

namespace WinBpmDataSyncService
{
    partial class BpmDataSyncService : ServiceBase
    {
        /// <summary>
        /// 定时器
        /// </summary>
        Timer times;
        public BpmDataSyncService()
        {
            InitializeComponent();

        }

        protected override void OnStart(string[] args)
        {
            try
            {
                var IntervalTime = Convert.ToInt32(SettingHelper.Get_ConfigValue("Interval"));

                times = new System.Timers.Timer();
                times.Interval = IntervalTime * (60 * 1000);
                times.Elapsed += new ElapsedEventHandler(Times_Elapsed);
                times.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.Error("BpmDataSyncService=>OnStart运行出错：" + ex.Message);

            }

        }

        private void Times_Elapsed(object sender, ElapsedEventArgs e)
        {
            var syncData = new ISyncData[] {
                    new UserDataSync(),
                  
                };
            foreach (var item in syncData)
            {
                item.Execute();
            }
        }

        protected override void OnStop()
        {
            this.times.Enabled = false;
        }
    }
}
