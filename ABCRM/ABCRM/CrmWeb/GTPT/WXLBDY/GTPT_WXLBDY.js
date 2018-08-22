
vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ["礼品名称", "LPID", "类型", "LPLX", "金额/积分值", "结束日期", ];
    vColumnModel = [
               { name: "sLPMC", },
              { name: "iLPID", hidden: true, },
              { name: "sLPLXMC", width: 100, },
              { name: "iLPLX", hidden: true },
              { name: "fJE", width: 100, editor: 'text', },
              {
                 name: "dJSRQ", width: 100, editor: 'text'
               },
          //WdatePicker
    ];


}





$(document).ready(function () {
    $("#Add1").click(function () {
        if ($("#TB_LBMC").val() == "") {
            ShowMessage("礼包名称不能为空");
            return;
        }
        var DataArry = new Object();

        SelectLP('list', DataArry, 'iLPID');



    });



    $("#Add2").click(function () {
        if ($("#TB_LBMC").val() == "") {
            ShowMessage("礼包名称不能为空");

            return;
        }
        var DataArry = new Object();
        SelectYHQList('list', DataArry, 'iYHQID');
    });

    $("#Add3").click(function () {
        if ($("#TB_LBMC").val() == "") {
            ShowMessage("礼包名称不能为空");

            return;
        }
        $('#list').datagrid('appendRow', {
            iLPID: iLPID = -1,
            sLPMC: sLPMC = "积分",
            iLPLX: iLPLX = 2,
            sLPLXMC: sLPMC = "积分",
        });


    });


    $("#Del").click(function () {
        DeleteRows("list");
    });

});

function SetControlState() {
    $("#status-bar").hide();
    $("#B_Exec").hide();
}

function IsValidData() {

    if ($("#TB_LPMC").val() == "") {
        ShowMessage("礼包名称不能为空");
        return false;
    }

    var rowidarr = $("#list").datagrid("getRows");
    for (var i = 0; i < rowidarr.length; i++) {
        var rowData = rowidarr[i];
        switch (parseInt(rowData.iLPLX)) {
            case 1:
                if (isNaN(rowData.fJE) || rowData.fJE <= 0) {
                    ShowMessage("请输入优惠券金额");
                    return false;
                }
                if (!IsDate(rowData.dJSRQ)) {
                    ShowMessage("请输入有效的结束日期!");
                    return false;
                }
                break;
            case 2:
                if (isNaN(rowData.fJE) || rowData.fJE <= 0) {
                    ShowMessage("请输入积分值");
                    return false;
                }
                break;
            default:
                break;
        }
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    Obj.sLBMC = $("#TB_LBMC").val();
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    Obj.status = vProcStatus

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_LBMC").val(Obj.sLBMC);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

}


function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {

    if (CheckFieldId == "iLPID") {
        for (var i = 0; i < lst.length; i++) {
            if (CheckReapet(array, CheckFieldId, lst[i])) {//[CheckFieldId]
                $('#list').datagrid('appendRow', {
                    iLPID: lst[i].iLPID,
                    sLPMC: lst[i].sLPMC,
                    iLPLX: 0,
                    sLPLXMC: "礼品",
                });
            }
        }
    }


    else {
        for (var i = 0; i < lst.length; i++) {
            if (CheckReapet(array, CheckFieldId, lst[i])) {//[CheckFieldId]
                $('#list').datagrid('appendRow', {
                    iLPID: lst[i].iYHQID,
                    sLPMC: lst[i].sYHQMC,
                    iLPLX: 1,
                    sLPLXMC: "优惠券",

                });
            }
        }


    }


}