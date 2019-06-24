
    $(function () {
        InitialPage();
        GetGrid();
    });
//初始化页面
function InitialPage() {
    //resize重设布局;
    $(window).resize(function (e) {
        window.setTimeout(function () {
            $('#gridTable').setGridWidth(($('.gridPanel').width()));
            $('#gridTable').setGridHeight($(window).height() - $('.titlePanel').height() - 100);
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
        url: "../../EcommerceManage/EcommerceGroup/GetPageListJson",
        datatype: "json",
        colModel: [
        { label: 'EcommerceGroupID', name: 'EcommerceGroupID', index: 'EcommerceGroupID', width: 100, align: 'left', sortable: true, hidden: true },
        { label: '电商简称', name: 'EcommerceGroupName', index: 'EcommerceGroupName', width: 150, align: 'left', sortable: true },
        { label: '创建日期', name: 'CreateDate', index: 'CreateDate', width: 100, align: 'left', sortable: true },
        { label: '创建用户', name: 'CreateUserName', index: 'CreateUserName', width: 100, align: 'left', sortable: true },
        { label: '修改日期', name: 'ModifyDate', index: 'ModifyDate', width: 100, align: 'left', sortable: true },
        { label: '修改用户', name: 'ModifyUserName', index: 'ModifyUserName', width: 100, align: 'left', sortable: true },
        ],
        viewrecords: true,
        rowNum: 30,
        rowList: [30, 50, 100],
        pager: "#gridPager",
        sortname: 'CreateDate desc',
        rownumbers: true,
        shrinkToFit: true,
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
        var queryJson = {
            EcommerceGroupName: $("#EcommerceGroupName").val(),
        }
        $('#gridTable').jqGrid('setGridParam', {
            postData: { queryJson: JSON.stringify(queryJson) },
            page: 1
        }).trigger('reloadGrid');
    });
    //查询回车
    $('#EcommerceGroupName').bind('keypress', function (event) {
        if (event.keyCode == "13") {
            $('#btn_Search').trigger("click");
        }
    });
}
//新增
function btn_add() {
    dialogOpen({
        id: 'Form',
        title: '新增电商集团档案',
        url: '/EcommerceManage/EcommerceGroup/Form',
        width: '400px',
        height: '200px',
        callBack: function (iframeId) {
            top.frames[iframeId].AcceptClick();
        }
    });
}
//编辑
function btn_edit() {
    var keyValue = $('#gridTable').jqGridRowValue('EcommerceGroupID');
    if (checkedRow(keyValue)) {
        dialogOpen({
            id: 'Form',
            title: '编辑电商集团档案',
            url: '/EcommerceManage/EcommerceGroup/Form?keyValue=' + keyValue,
            width: '400px',
            height: '200px',
            callBack: function (iframeId) {
                top.frames[iframeId].AcceptClick();
            }
        })
    }
}
//删除
function btn_delete() {
    var keyValue = $("#gridTable").jqGridRowValue("EcommerceGroupID");
    if (keyValue) {
        $.RemoveForm({
            url: "../../EcommerceManage/EcommerceGroup/RemoveForm",
            param: { keyValue: keyValue },
            success: function (data) {
                $("#gridTable").trigger("reloadGrid");
            }
        })
    } else {
        dialogMsg('请选择需要删除的电商公司！', 0);
    }
}