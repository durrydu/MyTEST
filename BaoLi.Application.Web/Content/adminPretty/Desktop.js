$(function () {
    mainChartShow();
    CooperationRankingYear();
    CooperationRankingShow();
    CooperationRankingClick();
    CirculationYear();
    CirculationShow();
    HighMoneyYear();
    HighMoneyShow();
})
function mainChartShow() {
    var n = echarts.init(document.getElementById("mainChart")), t;
    n.setOption({
        backgroundColor: "#fff",
        title: {
            text: "月度销售趋势图", left: 15, top: 15,
            textStyle: { color: "#666666", fontSize: 24, fontWeight: 100, fontFamily: "微软雅黑" },
            subtext: "昨日订单：298 | 累计订单：897 | 昨日交易额：99840 | 累计交易额：1890894",
            subtextStyle: { color: "#878787", fontSize: 14, fontWeight: 100, fontFamily: "微软雅黑" },
            itemGap: 20
        },
        tooltip: {
            trigger: "axis",
            axisPointer: { type: "shadow", textStyle: { color: "#fff" } }
        },
        legend: {
            data: ["新增订单", "新增客户", "其它"], x: 15, y: 95,
            textStyle: { color: "#878787" }
        }, grid: {
            borderWidth: 0, top: 130, bottom: 2, x: 15, y: 0, x2: 0, y2: 0,
            textStyle: { color: "#fff" }
        },
        xAxis: [{ type: "category", boundaryGap: !1, data: ["1号", "2号", "3号", "4号", "5号", "6号", "7号", "8号", "9号", "10号", "11号", "12号", "13号", "14号", "15号", "16号", "17号", "18号", "19号", "20号", "21号", "22号", "23号", "24号", "25号", "26号", "27号", "28号", "29号", "30号", "31号"], splitLine: !1, show: !1 }],
        yAxis: [{ type: "value", splitLine: !1, show: !1 }], series: [{ name: "其它", type: "bar", itemStyle: { normal: { color: "#DDE0E5" } }, data: [160, 80, 120, 150, 200, 150, 320, 220, 150, 140, 154, 220, 150, 232, 201, 154, 190, 250, 200, 150, 232, 201, 154, 190, 150, 232, 201, 154, 190, 160, 120] },
               { name: "新增订单", type: "line", smooth: !0, areaStyle: { normal: { color: "#7b93e0" } }, lineStyle: { normal: { color: "#7b93e0", width: 0 } }, data: [0, 0, 80, 100, 150, 160, 170, 200, 220, 240, 200, 180, 120, 100, 160, 180, 200, 210, 250, 200, 180, 170, 200, 240, 280, 300, 330, 260, 190, 150, 0, 0], symbol: "none" }, {
                   name: "新增客户", type: "line", smooth: !0, areaStyle: { normal: { color: "#2AE8DE" } },
                   lineStyle: { normal: { color: "#2AE8DE", width: 0 } }, data: [0, 0, 100, 220, 290, 190, 100, 80, 100, 110, 133, 150, 100, 80, 111, 130, 160, 144, 110, 90, 80, 100, 110, 120, 140, 190, 270, 200, 160, 100, 0, 0], symbol: "none"
               }]
    });
}
function CooperationRankingShow() {
    var n = echarts.init(document.getElementById("CooperationRankingShow")), t;
    n.setOption({
        grid: {
            left:'1%',
            containLabel: true
        },
        tooltip: {
            trigger: "axis",
            axisPointer: { type: "shadow", textStyle: { color: "#fff" } }
        },
        color:'#FB4655',
        xAxis: [
         {
            
             data: [],
             axisTick: {
                 alignWithLabel: true
             }
             , splitLine: { show: false }//去除网格线
             , axisLabel: {//坐标轴刻度标签的相关设置。
                 interval: 0,
                 rotate: "45"
             }
         }
        ],
        yAxis: [
            {
                type: 'value',
                splitLine: { show: false }//去除网格线
                
            }
        ],
        series : [
       {
           name:'直接访问',
           type:'bar',
           barWidth: '40%',
           data:[]
       }
        ]
    });
    var areas = [];    //地区数组（实际用来盛放X轴坐标值）
    var nums = [];    //合作次数数组（实际用来盛放Y坐标值）
    //获取合作次数最多的
    var year=$("#CooperationRankingYear").val();
    //$.GetAjax({
    //    url: "../Home/CooperationRankingNum?where="+year,
    //    dataType: "json",
        
    //    async: true,
    //    success: function (data) {
    //        if (data) {
    //            for (var i = 0; i < data.length; i++) {
    //                areas.push(data[i].ProviderName);    //挨个取出类别并填入类别数组
    //            }
    //            for (var i = 0; i < data.length; i++) {
    //                nums.push(data[i].Quantity);    //挨个取出销量并填入销量数组
    //            }
    //            n.hideLoading();    //隐藏加载动画
    //            n.setOption({        //加载数据图表
    //                xAxis: {
    //                    data: areas
    //                },
    //                series: [{
    //                    // 根据名字对应到相应的系列
    //                    name: '销量',
    //                    data: nums
    //                }]
    //            });

    //        }
    //    }
    //});
}
//合作次数年份点击事件
function CooperationRankingClick() {
    //$("#CooperationRankingYear").change("bind",function () {
    //    var year = $("#CooperationRankingYear").val();
    //    var n = echarts.init(document.getElementById("CooperationRankingShow")), t;
    //    var areas = [];    //地区数组（实际用来盛放X轴坐标值）
    //    var nums = [];    //合作次数数组（实际用来盛放Y坐标值）
    //    $.GetAjax({
    //        url: "../Home/CooperationRankingNum?where=" + year,
    //        dataType: "json",

    //        async: true,
    //        success: function (data) {
    //            if (data) {
    //                for (var i = 0; i < data.length; i++) {
    //                    areas.push(data[i].ProviderName);    //挨个取出类别并填入类别数组
    //                }
    //                for (var i = 0; i < data.length; i++) {
    //                    nums.push(data[i].Quantity);    //挨个取出销量并填入销量数组
    //                }
    //                n.hideLoading();    //隐藏加载动画
    //                n.setOption({        //加载数据图表
    //                    xAxis: {
    //                        data: areas
    //                    },
    //                    series: [{
    //                        // 根据名字对应到相应的系列
    //                        name: '销量',
    //                        data: nums
    //                    }]
    //                });

    //            }
    //        }
    //    });
    //});
    
}
function HighMoneyShow() {
    var n = echarts.init(document.getElementById("HighMoneyShow")), t;
    n.setOption({
        grid: {
            left: '1%',
            containLabel: true
        },
        tooltip: {
            trigger: "axis",
            axisPointer: { type: "shadow", textStyle: { color: "#fff" } }
        },
        color: '#FFA135',
        xAxis: [
         {
             type: 'category',
             data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
             axisTick: {
                 alignWithLabel: true
             }
             , splitLine: { show: false }//去除网格线
         }
        ],
        yAxis: [
            {
                type: 'value',
                splitLine: { show: false }//去除网格线

            }
        ],
        series: [
       {
           name: '直接访问',
           type: 'bar',
           barWidth: '40%',
           data: [1000, 520, 2000, 2330, 2001, 1950, 2200]
       }
        ]
    });


}

function CirculationShow() {
    var n = echarts.init(document.getElementById("CirculationShow")), t;
    n.setOption({
        
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            show: true,
            data: ['发行数量', '发行速度']
        },
        calculable: true,

        xAxis: [
         {
             type: 'category',
             data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
             ,splitLine: { show: false }
         }
        ],
        yAxis: [
       {
           type: 'value'
           ,splitLine: { show: false }
       }
        ],
        color: ['#FB4655', '#82C9FF'],
        series: [
          
        {
            name:'蒸发量',
            type:'bar',
            data:[2.0, 4.9, 7.0, 23.2, 25.6, 76.7, 135.6, 162.2, 32.6, 20.0, 6.4, 3.3],
            itemStyle: {
                normal: {
                    color: '#FB4655'
                }
            }
            
        },
        {
            name: '降水量',
            type: 'bar',
            data: [2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3],
            itemStyle: {
                normal: {
                    color: '#82C9FF'
                }
            }
        }
        ]
    
        
    });


}

function CooperationRankingYear(){
    var myDate= new Date(); 
    var startYear=myDate.getFullYear()-50;//起始年份 
    
    var obj = document.getElementById('CooperationRankingYear')
    for (var i = myDate.getFullYear() ; i >= startYear ; i--)
    {
        var year = new Option(i+"年", i);
        
        obj.options.add(year);
    } 
    obj.options[obj.options.length-51].selected=1; 
}
function CirculationYear() {
    var myDate = new Date();
    var startYear = myDate.getFullYear() - 50;//起始年份 

    var obj = document.getElementById('CirculationYear')
    for (var i = myDate.getFullYear() ; i >= startYear ; i--) {
        var year = new Option(i + "年", i);

        obj.options.add(year);
    }
    obj.options[obj.options.length - 51].selected = 1;
}
function HighMoneyYear() {
    var myDate = new Date();
    var startYear = myDate.getFullYear() - 50;//起始年份 

    var obj = document.getElementById('HighMoneyYear')
    for (var i = myDate.getFullYear() ; i >= startYear ; i--) {
        var year = new Option(i + "年", i);

        obj.options.add(year);
    }
    obj.options[obj.options.length - 51].selected = 1;
}