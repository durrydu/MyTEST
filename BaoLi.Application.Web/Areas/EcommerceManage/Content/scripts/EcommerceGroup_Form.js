
    var keyValue = request('keyValue');
$(function () {
    initControl();
});
//初始化控件
function initControl() {
    //获取表单
    if (!!keyValue) {
        $.SetForm({
            url: "../../EcommerceManage/EcommerceGroup/GetFormJson",
            param: { keyValue: keyValue },
            success: function (data) {
                $("#form1").SetWebControls(data);
            }
        })
    }
}
//保存表单;
function AcceptClick() {
    if (!$('#form1').Validform()) {
        return false;
    }
    var postData = $("#form1").GetWebControls(keyValue);
    $.SaveForm({
        url: "../../EcommerceManage/EcommerceGroup/SaveForm?keyValue=" + keyValue,
        param: postData,
        loading: "正在保存数据...",
        success: function () {
            $.currentIframe().$("#gridTable").trigger("reloadGrid");
        }
    })
}