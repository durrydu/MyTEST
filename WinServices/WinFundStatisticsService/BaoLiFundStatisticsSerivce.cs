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

namespace WinFundStatisticsService
{
    partial class BaoLiFundStatisticsSerivce : ServiceBase
    {
        Timer times;
        public BaoLiFundStatisticsSerivce()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log.Info(DateTime.Now.ToString() + "BaoLiFundStatisticsSerivce正常启动");
            var IntervalTime = Convert.ToInt32(SettingHelper.Get_ConfigValue("Interval"));
            times = new System.Timers.Timer();
            times.Interval = IntervalTime * 1000;
            //   times.Elapsed += Times_Elapsed;
            times.Elapsed += new ElapsedEventHandler(Times_Elapsed);
            times.Enabled = true;
            times.Start();
        }
        private void Times_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //var IntervalTime = Convert.ToInt32(SettingHelper.Get_ConfigValue("Interval"));
                Log.Info(DateTime.Now.ToString() + "BaoLiFundStatisticsSerivce开始运行");
                string startTime = Convert.ToString(SettingHelper.Get_ConfigValue("StartTime"));
                //   string startTime = "09:44";
                //if (DateTime.Now.ToString("HH:mm").Equals(startTime))
                //{
                Log.Info(DateTime.Now.ToString() + "BaoLiFundStatisticsSerivce满足运行时间要求，开始执行同步操作");
                //执行后将下次执行时间设置成23小时后，每天只执行一次
                //((Timer)sender).Interval = IntervalTime * 60 * 60 * 1000;

                var syncData = new ISyncData[] {
                        new FundStatisticsServer()
                    };
                foreach (var item in syncData)
                {
                    item.Execute();
                }
                Log.Info(DateTime.Now.ToString() + "BaoLiFundStatisticsSerivce同步操作执行完成");
                //}
                //else
                //{
                //    //60秒执行一次
                //    ((Timer)sender).Interval = 60 * 1000;
                //}

            }
            catch (Exception ex)
            {
                Log.Error("BaoLiFundStatisticsSerivce运行出错：" + ex.Message);

            }

        }
        protected override void OnStop()
        {
            this.times.Enabled = false;
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
