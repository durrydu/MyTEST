
var keyValue = request('keyValue');
$(function () {
    initControl();
});
//初始化控件
function initControl() {

    $.GetAjax({
        loading: "正在加载查询条件...",
        url: '../../EcommerceTransferManage/Transfer_Info/GetCodeRole?keyValue=' + keyValue,
        success: function (data) {
            $("#Transfer_Code").val(data);
        }
    });
    //项目名称
    $("#ProjectID").ComboBox({
        url: "../../BaseManage/Base_ProjectInfo/GetProjectListByRole",
        //param: { CompanyName: value },
        id: "ProjectID",
        text: "ProjecName",
        description: "==请选择==",
        height: "180px",
        allowSearch: true,
    }).bind("change", function () {
        $("#Transfer_Balance").val("");
        $("#Transfer_Money").val("");
        $("#EcommerceGroupIDzz").val("");
        $("#EcommerceGroupID").val("");
        $("#CompanyId").val("");
        $("#EcommerceProjectRelationID").val("");
        var value = $(this).attr("data-value");
        if (value != '') {
            $("#EcommerceID").ComboBox({
                url: "../../Base_ProjectInfo/GetEcomGroupNameJson?queryJson=" + value,
                id: "EcommerceID",
                text: "EcommerceName",
                description: "==请选择==",
                height: "180px",
            }).bind("change", function () {
                var queryJson = $("#ProjectID").attr("data-value");
                var quertValue = $("#EcommerceID").attr("data-value");
                if (queryJson != "" && quertValue != "") {
                    $.GetAjax({
                        loading: "正在加载查询条件...",
                        url: '../../BaseManage/Base_ProjectInfo/GetMoneyByEconmProjectJson?queryJson=' + queryJson + "&queryValue=" + quertValue,
                        success: function (data) {
                            if (data[0].ActualControlTotalAmount == null) {
                                $("#Transfer_Balance").val("0");
                            } else {
                                var result = money_format(data[0].ActualControlTotalAmount, "F", 2)
                                $("#Transfer_Balance").val(result);
                            }
                            $("#EcommerceGroupID").val(data[0].EcommerceGroupID);
                            $("#CompanyId").val(data[0].CompanyId);
                            $("#EcommerceProjectRelationID").val(data[0].EcommerceProjectRelationID);
                            $.GetAjax({
                                loading: "正在加载查询条件...",
                                url: "../../Base_ProjectInfo/GetEcomGroupNameByEconmJson?queryJson=" + quertValue,
                                success: function (data) {
                                    $("#EcommerceGroupIDzz").val(data[0].EcommerceGroupName);
                                }
                            });


                        }
                    });
                } else {

                    $("#Transfer_Balance").val("");
                    $("#Transfer_Money").val("");
                    $("#EcommerceGroupIDzz").val("");
                }




            });
        } else {
            $("#Transfer_Balance").val("");
            $("#Transfer_Money").val("");
            $("#EcommerceGroupIDzz").val("");


        }

    });
    $("#EcommerceID").ComboBox({
        description: "==请选择==",
        height: "180px",
    });

    //获取表单
    if (!!keyValue) {
        $.SetForm({
            url: "../../BaseManage/Transfer_Info/GetFormJson",
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
    var Transfer_Money = parseFloat($("#Transfer_Money").val());
    var Transfer_Balance = $("#Transfer_Balance").val();
    if (Transfer_Money <= 0) {
        dialogAlert("划拨金额必须大于0的数字", 5);
        return false;
    }
    if (Transfer_Money.toString().indexOf(".") != -1) {
        if (Transfer_Money.toString().split(".")[1].length > 6) {
            dialogAlert("金额最多允许输入六位小数", 5);
            return false;
        }
    }
    if (Transfer_Money > Transfer_Balance) {
        dialogAlert("划拨金额必须小于可用余额", 5);
        return false;
    }
    var postData = $("#form1").GetWebControls(keyValue);
    postData["EcommerceID"] = $("#EcommerceID").attr("data-value");
    postData["ProjectID"] = $("#ProjectID").attr("data-value");
    postData["EcommerceGroupID"] = $("#EcommerceGroupID").val();
    postData["CompanyId"] = $("#CompanyId").val();
    postData["EcommerceProjectRelationID"] = $("#EcommerceProjectRelationID").val();
    var EcommerceID = $("#EcommerceID").attr("data-value");
    var ProjectID = $("#ProjectID").attr("data-value");
    var ActualControlTotalAmount = $("#Transfer_Money").val();
    var Transfer_Code = $("#Transfer_Code").val();
    $.SaveForm({
        url: "../../EcommerceTransferManage/Transfer_Info/SaveForm?keyValue=" + keyValue + "&EcommerceID=" + EcommerceID + "&ProjectID=" + ProjectID + "&ActualControlTotalAmount=" + ActualControlTotalAmount + "&Transfer_Code=" + Transfer_Code,
        param: postData,
        loading: "正在保存数据...",
        success: function () {
            $.currentIframe().$("#gridTable").trigger("reloadGrid");
        }
    })
}
