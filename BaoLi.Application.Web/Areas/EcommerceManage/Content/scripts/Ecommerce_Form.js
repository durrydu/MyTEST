var keyValue = request('keyValue');
$(function () {
    initControl();
});
//初始化控件
function initControl() {

    if (keyValue == "") {
        $.GetAjax({
            loading: "正在加载查询条件...",
            url: '../../EcommerceManage/Ecommerce/GetCodeRole',
            success: function (data) {
                $("#EcommerceCode").val(data);
            }
        });
    }
    // 电商集团下拉
    $("#EcommerceGroupID").ComboBox({
        url: "../../EcommerceManage/EcommerceGroup/GetEcommerceGroupNameJson",
        id: "EcommerceGroupID",
        text: "EcommerceGroupName",
        height:"100px",
        description: "==请选择==",
    });
    //电商性质下拉
    $("#EcommerceType").ComboBox({
        url: "../../EcommerceManage/Ecommerce/GetEcommerceTypeEnum",
        id: "Value",
        text: "Description",
        height: "200px",
        description: "=请选择=",
    })
    //获取表单
    if (!!keyValue) {
        $.SetForm({
            url: "../../EcommerceManage/Ecommerce/GetFormJson",
            param: { keyValue: keyValue },
            success: function (data) {
                data.CooperateEndTime = data.CooperateEndTime.replace("00:00:00", "").trim();
                data.CooperateStartTime = data.CooperateStartTime.replace("00:00:00", "").trim();
                $("#form1").SetWebControls(data);
                $("#EcommerceGroupName").ComboBoxSetValue(data.EcommerceGroupID);
                $("#EcommerceType").ComboBoxSetValue(data.EcommerceType);
                $("#PlatformRate").val(data.PlatformRate)
                $("#EcommerceCode").val(data.EcommerceCode);
            }
        })
    }
}
//保存表单;
function AcceptClick() {
    if (!$('#form1').Validform()) {
        return false;
    }
    var starttime = new Date($("#CooperateStartTime").val());
    var endtime = new Date($("#CooperateEndTime").val());
    var datanow = new Date();
    if (starttime > endtime)
    {
        dialogMsg("开始日期不能大于结束日期", 5);
        return false;
    }
    //if (endtime < datanow) {
    //    dialogMsg("结束日期不能小于当前日期", 5);
    //    return false;
    //}
    var platrate = parseFloat($("#PlatformRate").val());
    if (platrate < 0 || platrate > 100)
    {
        dialogAlert("平台费率必须为0-100的数字",5);
        return false;
    }
    var postData = $("#form1").GetWebControls(keyValue);
    postData["EcommerceGroupName"] = $("#EcommerceGroupID").attr("data-text");
    postData["EcommerceType"] = $("#EcommerceType").attr("data-value");
    $.SaveForm({
        url: "../../EcommerceManage/Ecommerce/SaveForm?keyValue=" + keyValue,
        param: postData,
        loading: "正在保存数据...",
        success: function () {
            $.currentIframe().$("#gridTable").trigger("reloadGrid");
        }
    })
}