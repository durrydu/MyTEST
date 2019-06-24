using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movit.Util;
using WinFundStatisticsService;

namespace WinServicerUnitTest
{
    [TestClass]
    public class ServicerUnitTest
    {
        [TestMethod]
        public void FundStatisticsServerTestMethod()
        {

            FundStatisticsServer serverTest = new FundStatisticsServer();
            serverTest.Execute();
        }

        [TestMethod]
        public void UserDataSyncTestMethod()
        {

            //DateTime dtFirstDay = new DateTime(DateTime.Now.Year, 1, 1);
            //DateTime dtLastDay = dtFirstDay.AddYears(1).AddDays(-1);
            //var da = Time.IsFirstDayOfMonth(DateTime.Now);
            //var daa = Time.IsLastDayOfMonth(DateTime.Now);
            //var ddddd = Time.IsFirstDayOfYear(DateTime.Now);
        }
        ///// <summary>
        ///// BPM同步测试程序单元测试 ivan.yao 20180304
        ///// </summary>
        //[TestMethod]
        //public void UserDataSyncTestMethod()
        //{
        //    WinBpmDataSyncService.UserDataSync dataSyncTest = new WinBpmDataSyncService.UserDataSync();
        //    dataSyncTest.Execute();
        //}

        ///// <summary>
        ///// 主数据同步测试程序单元测试 ivan.yao 20180304
        ///// </summary>
        //[TestMethod]
        //public void UDbSyncServerTestMethod()
        //{
        //    try
        //    {
        //        WinDbSyncSerivce.DbSyncServer dataSyncTest = new WinDbSyncSerivce.DbSyncServer();
        //        dataSyncTest.Execute();
        //    }
        //    catch (Exception)
        //    {


        //    }
        //}
    }
}
