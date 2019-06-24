
var keyValue = request('keyValue');
$(function () {
    initControl();
    $("#form1").find('.from-control,.ui-select,input,.input-wdatepicker').attr('readonly', 'readonly');
})
function initControl() {
    // 电商集团下拉
    $("#EcommerceGroupID").ComboBox({
        url: "../../EcommerceManage/EcommerceGroup/GetEcommerceGroupNameJson",
        id: "EcommerceGroupID",
        text: "EcommerceGroupName",
        height: "200px",
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
                $("#PlatformRate").val(data.PlatformRate);
            }
        })
    }
}