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

namespace WinFactorContractService
{
    partial class BaoLiFactorContractService : ServiceBase
    {
        Timer times;
        public BaoLiFactorContractService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log.Info(DateTime.Now.ToString() + "BaoLiFactorContract正常启动");
            times = new System.Timers.Timer();
            times.Interval = 5 * 1000;
            //   times.Elapsed += Times_Elapsed;
            times.Elapsed += new ElapsedEventHandler(Times_Elapsed);
            times.Enabled = true;
            times.Start();
        }
        private void Times_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var IntervalTime = Convert.ToInt32(SettingHelper.Get_ConfigValue("Interval"));
                Log.Info(DateTime.Now.ToString() + "BaoLiFactorContract开始运行");
                ((Timer)sender).Interval = IntervalTime * 1000;
                var syncData = new ISyncData[] {
                        new BaoLiFactorContract()
                    };
                foreach (var item in syncData)
                {
                    item.Execute();
                }
                Log.Info(DateTime.Now.ToString() + "BaoLiFactorContract执行完成");
            }
            catch (Exception ex)
            {
                Log.Error("BaoLiFactorContract运行出错：" + ex.Message);

            }

        }
        protected override void OnStop()
        {
            this.times.Enabled = false;
        }
    }
}
