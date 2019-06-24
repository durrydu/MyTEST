 var keyValue = request('keyValue');
var $gridLeader = $("#gridLeader");
$(function () {
    initControl();
});
//初始化控件
function initControl() {
    //获取表单
    if (!!keyValue) {
        $.SetForm({
            url: "../../EcommerceTransferManage/Transfer_Info/GetProEcomJson",
            param: { keyValue: keyValue },
            success: function (data) {
                data.CreateDate = formatDate(data.CreateDate, 'yyyy-MM-dd');
                data.Transfer_Date = formatDate(data.Transfer_Date, 'yyyy-MM-dd');
                data.Transfer_Money = money_format(data.Transfer_Money, "F", 2)
                $("#step-2").SetWebControls(data);
            }
        })
    }
}