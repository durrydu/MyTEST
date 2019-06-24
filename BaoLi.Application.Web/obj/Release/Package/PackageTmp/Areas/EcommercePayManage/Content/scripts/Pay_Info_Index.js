
    $(function () {
        InitialPage();
        GetGrid();
    });
function InitialPage() {
    //区域公司
    $("#Project_Id").ComboBox({
        url: "../../BaseManage/Department/GetListByAuthorize",
        id: "DepartmentId",
        text: "FullName",
        description: "==请选择==",
        height:"200px",
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
            height: "200px",
            
        })
    });
    $("#Project_Name").ComboBox({
        url: "../../BaseManage/Base_ProjectInfo/GetProjectListByRole",
        description: "==请选择==",
        id: "ProjectID",
        text: "ProjecName",
        height: "180px",
        allowSearch: true,
    })
    //resize重设布局;
    $(window).resize(function (e) {
        window.setTimeout(function () {
            $('#gridTable').setGridWidth(($('.gridPanel').width()));
            $('#gridTable').setGridHeight($(window).height() - $('.titlePanel').height() - 120);
        }, 200);
        e.stopPropagation();
    });
}
function GetGrid() {
    var selectedRowIndex = 0;
    var $gridTable = $('#gridTable');
    $gridTable.jqGrid({
        autowidth: true,
        height: $(window).height() - $('.titlePanel').height() - 70,
        url: '../../EcommercePayManage/Pay_Info/GetPageListJson',
        datatype: "json",
        colModel: [
            { label: '付款单ID', name: 'Pay_Info_Id', index: 'Pay_Info_Id', align: 'center', sortable: true, hidden: true },
            { label: '区域公司', name: 'CompanyName', index: 'CompanyName', align: 'center', sortable: true },
            { label: '项目名称', name: 'Project_Name', index: 'Project_Name', align: 'center', sortable: true },
            { label: '电商简称', name: 'EcommerceGroupName', index: 'EcommerceGroupName', align: 'center', sortable: true },
            { label: '电商公司', name: 'Electricity_Supplier_Name', index: 'Electricity_Supplier_Name', align: 'center', sortable: true },        
            { label: '支付流程序号', name: 'Pay_Info_Code', index: 'Pay_Info_Code', align: 'center', sortable: true,width:200 },
            { label: '付款主题', name: 'Pay_Reason', index: 'Pay_Reason', align: 'center', sortable: true,hidden:true },
            { label: '合同名称', name: 'Contract_Name', index: 'Contract_Name', align: 'center', sortable: true, width: 200 },
            {
                label: '付款金额(元)', name: 'Pay_Money', index: 'Pay_Money', align: 'right', sortable: true,
                formatter: function (cellvalue) {
                    return money_format(cellvalue, "F", 2);
                }
            },
            {
                label: '支付流程发起时间', name: 'Pay_Createtime', index: 'Pay_Createtime', align: 'center', sortable: true, width: 200,
                formatter:function(cellvalue)
                {
                    return formatDate(cellvalue,"yyyy-MM-dd")
                }
            },
            {
                label: '支付流程审批通过时间', name: 'Pay_Completetime', index: 'Pay_Completetime', align: 'center', sortable: true, width: 250,
                formatter: function (cellvalue) {
                    return formatDate(cellvalue, "yyyy-MM-dd")
                }
            },
            { label: '经办人', name: 'Login_Name', index: 'Login_Name', align: 'center', sortable: true },
            {
                label: '审批状态', name: 'Approval_Status', index: 'Approval_Status', align: 'center', sortable: true,
                formatter: function (cellvale, options, rowobject) {
                    return "<a target='_blank' href='" + rowobject.Url + "'  style=\"cursor: pointer; color:#9933FF; \">" + GetEnumDescription(cellvale, top.enumData.ApprovalStateEnumArray) + "</a>";
                }
            },
        ],
        viewrecords: true,
        rowNum: 30,
        rowList: [30, 50, 100],
        pager: "#gridPager",
        sortname: 'Pay_Createtime desc',
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
        var queryJson = {
            Electricity_Supplier_Name: $("#Electricity_Supplier_Name").val(),
            CompanyID: $("#Project_Id").attr("data-value"),
            ProjectID: $("#Project_Name").attr("data-value"),
            Pay_Createtime: $("#Pay_Createtime").val(),
            Pay_EndTime: $("#Pay_Endtime").val(),
        }

        $('#gridTable').jqGrid('setGridParam', {
            postData: { queryJson: JSON.stringify(queryJson) },
            page: 1
        }).trigger('reloadGrid');
    });
    //查询回车
    $('#Electricity_Supplier_Name').bind('keypress', function (event) {
        if (event.keyCode == "13") {
            $('#btn_Search').trigger("click");
        }
    });
}
