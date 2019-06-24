
$(function () {
    InitialPage();
    GetGrid();
});
function InitialPage() {
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
        height: $(window).height() - $('.titlePanel').height(),
        url: "../../CapitalFlowManage/T_CapitalFlow/GetPageListJson",
        datatype: "json",
        colModel: [
           { label: 'CapitalFlow_Id', name: 'CapitalFlow_Id', index: 'CapitalFlow_Id', width: 100, align: 'left', sortable: true, hidden: true },

            {
                label: '区域名称', name: 'FullName', index: 'FullName', width: 100, align: 'left', sortable: true

            },
            {
                label: '上传数据月份', name: 'Year', index: 'Year', width: 100, align: 'left', sortable: false,

                formatter: function (cellvale, options, rowobject) {
                    console.log(options.Year);
                    return rowobject.Year + "年" + rowobject.Month + "月";
                }

            },
            {
                label: '申请时间', name: 'CreateDate', index: 'CreateDate', width: 140, align: 'left', sortable: true
            },
            {
                label: '新增电商收入(元)', name: 'IncomeAmount', index: 'IncomeAmount', width: 160, align: 'right', sortable: true,
                formatter: function (cellvalue) {
                    return money_format(cellvalue, "F", 2);
                }
              
            },
            {
                label: '新增电商结算(元)', name: 'ClearingAmount', index: 'ClearingAmount', width: 160, align: 'right', sortable: true,
                formatter: function (cellvalue) {
                    return money_format(cellvalue, "F", 2);
                }

            },
            {
                label: '平台费支出(元)', name: 'PlatformExpensesAmount', index: 'PlatformExpensesAmount', width: 160, align: 'right', sortable: true,
                formatter: function (cellvalue) {
                    return money_format(cellvalue, "F", 2);
                }
           
            },
            {
                label: '我司可支配金额(元)', name: 'CapitalPoolAdd', index: 'CapitalPoolAdd', width: 160, align: 'right', sortable: true,
                formatter: function (cellvalue) {
                    return money_format(cellvalue, "F", 2);
                }
               
            },
            {
                label: '审批状态', name: 'ApprovalState', index: 'ApprovalState', width: 130, align: 'left', sortable: true,
                formatter: function (cellvale, options, rowobject) {
                    if (cellvale == 1 && rowobject.Procinstid==null) {
                        return "<a target='_blank'  onclick='edit_a(\"" + rowobject.CapitalFlow_Id + "\")' style=\"cursor: pointer; color:#9933FF; \">草稿</a>"
                    } else {
                        return "<a target='_blank' href='" + rowobject.url + "'  style=\"cursor: pointer; color:#9933FF; \">" + GetEnumDescription(cellvale, top.enumData.ApprovalStateEnumArray) + "</a>";

                    }
                   
                },
            },
            { label: 'Procinstid', name: 'Procinstid', index: 'Procinstid', width: 100, align: 'center', sortable: true, hidden: true },

            

        ],
        viewrecords: true,
        rowNum: 30,
        rowList: [30, 50, 100],
        pager: "#gridPager",
        sortname: 'CreateDate',
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
        var ApprovalState = $("#ApprovalState option:selected").val();
        var FullName = $("#FullName").val();
        var queryJson = {
            ApprovalState: $("#ApprovalState option:selected").val(),
            FullName: $("#FullName").val(),
        }
        $('#gridTable').jqGrid('setGridParam', {
            postData: { queryJson: JSON.stringify(queryJson) },
            page: 1
        }).trigger('reloadGrid');
    });
    //查询回车
    $('#FullName').bind('keypress', function (event) {
        if (event.keyCode == "13") {
            $('#btn_Search').trigger("click");
        }
    });
}
//新增
function btn_add() {
    location.href = '/CapitalFlowManage/T_CapitalFlow/AddForm';

}
//编辑
function btn_edit() {
    var keyValue = $("#gridTable").jqGridRowValue('CapitalFlow_Id');
    var state = $("#gridTable").jqGridRowValue('ApprovalState');


    if (checkedRow(keyValue)) {
        var arr = state.split(">");
        var array = arr[1].split("<");
        if (array[0] == "草稿") {
            location.href = '../../CapitalFlowManage/T_CapitalFlow/Form?keyValue=' + keyValue;
        }
        else {
            dialogMsg('只有草稿状态的数据允许被编辑！', 0);
            return;
        }
    }


}
//详细
function btn_detail() {
    var keyValue = $('#gridTable').jqGridRowValue('CapitalFlow_Id');
    if (checkedRow(keyValue)) {
        location.href = '../../CapitalFlowManage/T_CapitalFlow/Detail?keyValue=' + keyValue;
    }
}
function edit_a(keyValue)
{
    location.href = '../../CapitalFlowManage/T_CapitalFlow/Form?keyValue=' + keyValue;
}
////编辑
//function btn_edit() {
//    var keyValue = $('#gridTable').jqGridRowValue('Transfer_Info_Id');
//    if (checkedRow(keyValue)) {
//        dialogOpen({
//            id: 'Form',
//            title: '编辑T_Transfer_Info',
//            url: '/EcommerceTransferManage/Transfer_Info/Transfer_InfoForm?keyValue=' + keyValue,
//            width: 'px',
//            height: 'px',
//            callBack: function (iframeId) {
//                top.frames[iframeId].AcceptClick();
//            }
//        })
//    }
//}
//删除
//function btn_delete() {

//    var keyValue = $('#gridTable').jqGridRowValue('Transfer_Info_Id');
//    var ProjectID = $('#gridTable').jqGridRowValue('ProjectID');
//    var Transfer_Money = $('#gridTable').jqGridRowValue('Transfer_Money');
//    var EcommerceID = $('#gridTable').jqGridRowValue('EcommerceID');
//    if (keyValue) {
//        dialogOpen({
//            id: 'RemoveForm',
//            title: '新增划拨资金',
//            url: '../../EcommerceTransferManage/Transfer_Info/ReForm?keyValue=' + keyValue + "&ProjectID=" + ProjectID + "&Transfer_Money=" + Transfer_Money + "&EcommerceID=" + EcommerceID,
//            width: '500px',
//            height: '200px',
//            callBack: function (iframeId) {
//                top.frames[iframeId].AcceptClick();
//            }
//        });
//    } else {
//        dialogMsg('请选择需要删除的T_Transfer_Info！', 0);
//    }
//}
