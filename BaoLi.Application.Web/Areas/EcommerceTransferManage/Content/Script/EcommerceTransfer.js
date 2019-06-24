
$(function () {
    InitialPage();
    GetGrid();
});
//初始化页面
function InitialPage() {
    $("#Project_Id").ComboBox({
        url: "../../BaseManage/Department/GetListByAuthorize",
        id: "DepartmentId",
        text: "FullName",
        description: "==请选择==",
        height: "200px",
        allowSearch: true,
    }).bind("change", function () {
        var value = $(this).attr("data-value");
        //项目名称
        $("#Project_Name").ComboBox({
            url: "../../BaseManage/Base_ProjectInfo/GetProjectListByRole",
            param: { keyValue: value },
            id: "ProjectID",
            text: "ProjecName",
            description: "==请选择==",
            height: "180px",
            allowSearch: true,
        })
    });
    //项目名称
  
    $("#Project_Name").ComboBox({
        url: "../../BaseManage/Base_ProjectInfo/GetProjectListByRole",
       
        id: "ProjectID",
        text: "ProjecName",
        description: "==请选择==",
        height: "180px",
        allowSearch: true,
    })
    //resize重设布局;
    $(window).resize(function (e) {
        window.setTimeout(function () {
            $('#gridTable').setGridWidth($('.gridPanel').width());
            $('#gridTable').setGridHeight($(window).height() - $('.titlePanel').height());
        }, 200);
        e.stopPropagation();
    });
}
//加载表格
function GetGrid() {
    var selectedRowIndex = 0;
    var $gridTable = $('#gridTable');
    $gridTable.jqGrid({
        autowidth: true,
        height: $(window).height() - $('.titlePanel').height() - 70,
        url: "../../EcommerceTransferManage/Transfer_Info/GetPageListJson",
        datatype: "json",
        colModel: [
            { label: 'Transfer_Info_Id', name: 'Transfer_Info_Id', index: 'Transfer_Info_Id', width: 100, align: 'center', sortable: true, hidden: true },
            //{
            //    label: '划拨时间', name: 'Transfer_Date', index: 'Transfer_Date', width: 100, align: 'left', sortable: true,
            //    formatter: function (cellvalue) {
            //        return formatDate(cellvalue, 'yyyy-MM-dd')
            //    }
            //},
            { label: '区域公司', name: 'CompanyName', index: 'CompanyName', width: 100, align: 'left', sortable: true },
            { label: '创建编号', name: 'Transfer_Code', index: 'Transfer_Code', width: 100, align: 'left', sortable: true },
            { label: '项目名称', name: 'ProjecName', index: 'Transfer_Code', width: 100, align: 'left', sortable: true },
            { label: 'ProjectID', name: 'ProjectID', index: 'ProjectID', width: 100, align: 'left', sortable: true, hidden: true },
            { label: 'EcommerceID', name: 'EcommerceID', index: 'EcommerceID', width: 100, align: 'left', sortable: true, hidden: true },
            { label: '电商简称', name: 'EcommerceGroupName', index: 'EcommerceGroupName', width: 100, align: 'left', sortable: true },
            {
                label: '划拨金额(元)', name: 'Transfer_Money', index: 'Transfer_Money', width: 100, align: 'left', sortable: true,
                formatter: function (cellvalue) {
                    return money_format(cellvalue, "F", 2);
                }
            },
            {
                label: '创建时间', name: 'CreateDate', index: 'CreateDate', width: 100, align: 'left', sortable: true,
                formatter: function (cellvalue) {
                    return formatDate(cellvalue, 'yyyy-MM-dd')
                }
            },
            { label: '经办人', name: 'CreateUserName', index: 'CreateUserName', width: 100, align: 'left', sortable: true },
            { label: '付款主题', name: 'Transfer_Title', index: 'Transfer_Title', width: 250, align: 'left', sortable: true },
            
        ],
        viewrecords: true,
        rowNum: 30,
        rowList: [30, 50, 100],
        pager: "#gridPager",
        sortname: 'Transfer_Date desc',
        rownumbers: true,
        shrinkToFit: false,
        gridview: true,
        onSelectRow: function () {
            selectedRowIndex = $('#' + this.id).getGridParam('selrow');
        },
        gridComplete: function () {
            $('#' + this.id).setSelection(selectedRowIndex, false);
        }
    });
    //查询事件
    $("#btn_Search").click(function () {
        var starttime = $("#Transfer_Createtime").val();
        var endtime = $("#Transfer_Endtime").val();
        var startnum = parseInt(starttime.replace(/-/g, ''), 10);
        var endnum = parseInt(endtime.replace(/-/g, ''), 10);
        if (startnum > endnum) {
            dialogMsg("开始日期不能大于结束日期", 5);
            return false;
        }
        var queryJson = {
            C_Name: $("#C_Name").val(),
            Company_Name: $("#Project_Id").attr("data-value"),
            Project_Name: $("#Project_Name").attr("data-value"),
            Transfer_Createtime: $("#Transfer_Createtime").val(),
            Transfer_EndTime: $("#Transfer_Endtime").val(),
        }
        $('#gridTable').jqGrid('setGridParam', {
            postData: { queryJson: JSON.stringify(queryJson) },
            page: 1
        }).trigger('reloadGrid');
    });
    //查询回车
    $('#C_Name').bind('keypress', function (event) {
        if (event.keyCode == "13") {
            $('#btn_Search').trigger("click");
        }
    });
}
//新增
function btn_add() {
    dialogOpen({
        id: 'Form',
        title: '新增划拨资金',
        url: '../../EcommerceTransferManage/Transfer_Info/Form',
        width: '800px',
        height: '450px',
        callBack: function (iframeId) {
            top.frames[iframeId].AcceptClick();
        }
    });
        
}
//详细
function btn_detail() {
    var keyValue = $('#gridTable').jqGridRowValue('Transfer_Info_Id');
    if (checkedRow(keyValue)) {
        location.href = '/EcommerceTransferManage/Transfer_Info/Detail?keyValue=' + keyValue;
    }
}
//编辑
function btn_edit() {
    var keyValue = $('#gridTable').jqGridRowValue('Transfer_Info_Id');
    if (checkedRow(keyValue)) {
        dialogOpen({
            id: 'Form',
            title: '编辑T_Transfer_Info',
            url: '/EcommerceTransferManage/Transfer_Info/Transfer_InfoForm?keyValue=' + keyValue,
            width: 'px',
            height: 'px',
            callBack: function (iframeId) {
                top.frames[iframeId].AcceptClick();
            }
        })
    }
}
//删除
function btn_delete() {
    
    var keyValue = $('#gridTable').jqGridRowValue('Transfer_Info_Id');
    var ProjectID = $('#gridTable').jqGridRowValue('ProjectID');
    var Transfer_Money = $('#gridTable').jqGridRowValue('Transfer_Money'); 
    var EcommerceID = $('#gridTable').jqGridRowValue('EcommerceID');
    if (keyValue) {
        dialogOpen({
            id: 'RemoveForm',
            title: '新增划拨资金',
            url: '../../EcommerceTransferManage/Transfer_Info/ReForm?keyValue=' + keyValue + "&ProjectID=" + ProjectID + "&Transfer_Money=" + Transfer_Money + "&EcommerceID=" + EcommerceID,
            width: '500px',
            height: '200px',
            callBack: function (iframeId) {
                top.frames[iframeId].AcceptClick();
            }
        });
    } else {
        dialogMsg('请选择需要删除的数据！', 0);
    }
}
