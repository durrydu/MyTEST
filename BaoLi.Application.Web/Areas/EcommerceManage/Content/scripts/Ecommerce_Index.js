    $(function () {
        InitialPage();
        GetGrid();
    });
//初始化页面
    function InitialPage() {
    // 电商集团下拉
    $("#EcommerceGroupID").ComboBox({
        url: "../../EcommerceManage/EcommerceGroup/GetEcommerceGroupNameJson",
        description: " ==请选择== ",
        id: "EcommerceGroupID",
        text: "EcommerceGroupName",
        allowSearch: true,
        height: "200px",
    });
    
    //resize重设布局;
    $(window).resize(function (e) {
        window.setTimeout(function () {
            $('#gridTable').setGridWidth(($('.gridPanel').width()));
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
        height: $(window).height() - $('.titlePanel').height()-70,
        url: "../../EcommerceManage/Ecommerce/GetPageListJson",
        datatype: "json",
        colModel: [
        { label: 'EcommerceID', name: 'EcommerceID', index: 'EcommerceID', width: 100, align: 'left', sortable: true, hidden: true },
        { label: 'EcommerceGroupID', name: 'EcommerceGroupID', index: 'EcommerceGroupID', width: 100, align: 'left', sortable: true, hidden: true },
        { label: '电商简称', name: 'EcommerceGroupName', index: 'EcommerceGroupName', width: 150, align: 'left', sortable: true },
        { label: '电商公司', name: 'EcommerceName', index: 'EcommerceName', width: 100, align: 'left', sortable: true },
        { label: 'shijian', name: 'CreateDate', index: 'CreateDate', width: 100, align: 'left', sortable: true, hidden: true },
        {
            label: '电商性质', name: 'EcommerceType', index: 'EcommerceType', width: 100, align: 'left', sortable: true,
            formatter: function (cellvalue)
            {
                if (cellvalue == 0) { return "平台"; }
                else { return "渠道" };
            }
        },
        { label: '平台费率(%)', name: 'PlatformRate', index: 'PlatformRate', width: 100, align: 'left', sortable: true },
        { label: '经办人', name: 'AgentUserID', index: 'AgentUserID', width: 100, align: 'left', sortable: true },
        ],
        viewrecords: true,
        rowNum: 30,
        rowList: [30, 50, 100],
        pager: "#gridPager",
        sortname: 'EcommerceName',
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
            EcommerceGroupID: $("#EcommerceGroupID").attr("data-value"),
            EcommerceName: $("#EcommerceName").val(),
        }

        $('#gridTable').jqGrid('setGridParam', {
            postData: { queryJson: JSON.stringify(queryJson) },
            page: 1
        }).trigger('reloadGrid');
    });
    //查询回车
    $('#EcommerceGroupID,#EcommerceName').bind('keypress', function (event) {
        if (event.keyCode == "13") {
            $('#btn_Search').trigger("click");
        }
    });
}
//新增
function btn_add() {
    dialogOpen({
        id: 'Form',
        title: '新增电商档案',
        url: '/EcommerceManage/Ecommerce/Form',
        width: '700px',
        height: '300px',
        callBack: function (iframeId) {
            top.frames[iframeId].AcceptClick();
        }
    });
}
//编辑
function btn_edit() {
    var keyValue = $('#gridTable').jqGridRowValue('EcommerceID');
    if (checkedRow(keyValue)) {
        dialogOpen({
            id: 'Form',
            title: '编辑电商档案',
            url: '/EcommerceManage/Ecommerce/Form?keyValue=' + keyValue,
            width: '900px',
            height: '300px',
            callBack: function (iframeId) {
                top.frames[iframeId].AcceptClick();
            }
        })
    }
}
//详细
function btn_detail()
{
    var keyValue = $("#gridTable").jqGridRowValue("EcommerceID");
    if(checkedRow(keyValue))
    {
        dialogOpen({
            id: "Detail",
            title: "电商明细",
            url: '/EcommerceManage/Ecommerce/Detail?keyValue=' + keyValue,
            width: "900px",
            height: "300px",
            btn:null
        })
    }
}
//资金明细
function btn_view() {
    var keyValue = $("#gridTable").jqGridRowValue("EcommerceID");
    var queryJson = {
        EcommerceGroupID: $("#gridTable").jqGridRowValue("EcommerceGroupID"),
    }
    if (checkedRow(keyValue)) {
        location.href = '../../EcommercePayQueryManage/EcomPayEcommerce/Index?queryJson=' + JSON.stringify(queryJson);
    }
}