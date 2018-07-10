vUrl = "../GTPT.ashx";

var vMDColumnNames;
var vMDColumnModel;
function InitGrid() {
    vColumnNames = ['日期', '数量', '提示'];
    vColumnModel = [
                { name: 'dRQ', width: 100 },
                { name: 'iXZSL', width: 100, editor: 'text', },
                { name: 'sWXTS', width: 200, editor: 'text', },
    ];

    vMDColumnNames = ['MDID', '门店名称'];
    vMDColumnModel = [
             { name: 'iMDID', hidden: true },
             { name: 'sMDMC', width: 60, },
    ];
}
$(document).ready(function () {
    DrawGrid("listMD", vMDColumnNames, vMDColumnModel);

    $("#AddItem").click(function () {
        var dRQ1 = $("#TB_RQ1").val();
        var dRQ2 = $("#TB_RQ2").val();
        var XZSL = $("#TB_XZSL").val();
        var WXTS = $("#TB_WXTS").val();
        if (dRQ1 == "") {
            ShowMessage("请选择开始日期！", 3);
            return;
        }
        if (dRQ2 == "") {
            ShowMessage("请选择截止日期！", 3);
            return;
        }
        if (XZSL == "") {
            ShowMessage("请填写限制数量！", 3);
            return;
        }
        if (WXTS == "") {
            ShowMessage("请填写达到限制提示！", 3);
            return;
        }



        for (var dRQ = dRQ1; dRQ <= dRQ2;) {
            var rowidarr = $("#list").datagrid("getRows");
            for (var i = 0; i < rowidarr.length  ; i++) {
                var rowData = rowidarr[i];
                if (rowData.dRQ == dRQ) {
                    ShowMessage("选择日期重复");
                    return;
                }
            }
            $("#list").datagrid("appendRow", {
                    dRQ: dRQ,
                    iXZSL: $("#TB_XZSL").val(),
                    sWXTS: $("#TB_WXTS").val(),
                });

            var day = new Date(dRQ);
            dRQ = new Date(day.getTime() + 24 * 60 * 60 * 1000);
            dRQ = getDate(dRQ);
        }


        //var listRowData = $("#list").datagrid("getRows");
        //for (i = 0; i < listRowData.length  ; i++) {
        //    if (listRowData[i].dRQ == dRQ) {
        //        ShowMessage("选择日期重复");
        //        return;
        //    }
        //}
        //$("#list").datagrid("appendRow", {
        //    dRQ: dRQ,
        //    iXZSL: XZSL,
        //    sWXTS: WXTS,
        //});

        $("#TB_RQ1").val("");
        $("#TB_RQ2").val("");
        $("#TB_XZSL").val("");
        $("#TB_WXTS").val("");
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });

    $("#AddMD").click(function () {


        //var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?";
        //vDialogName = "ListWXMD";
        //var DataObject = new Object();
        //OpenDialog(dialogUrl, "listMD", DataObject, vDialogName, "iMDID", false);
        var DataArry = new Object();
        SelectWXMDList('listMD', DataArry, 'iMDID', false);

    });

    $("#DelMD").click(function () {
        DeleteRows("listMD");
    });
    BFButtonClick("TB_GZMC", function () {
        var ConData = new Object();
        ConData["iGZLX"] = vDJLX;
        SelectWXLPFFGZ("TB_GZMC", "HF_GZID", "zHF_GZID", true, ConData);
    });

})
function SetControlState() {
    $("#B_Start").show();
    $("#B_Stop").show();
    if ($("#LB_ZZRMC").text() != "") {
        document.getElementById("B_Stop").disabled = true;
    }
    $("#ZZR").show();
    $("#ZZSJ").show();
    $("#QDR").show();
    $("#QDSJ").show();
}
function IsValidData() {

    if ($("#TB_GZMC").val() == "") {
        ShowMessage("请选择规则！", 3);
        return false;
    }

    if ($("#TB_KSRQ").val() == "") {
        ShowMessage("请选择开始日期！", 3);
        return false;
    }
    if ($("#TB_JSRQ").val() == "") {
        ShowMessage("请选择结束日期！", 3);
        return false;
    }
    if ($("#TB_YXQ").val() == "") {
        ShowMessage("请选择有效期！", 3);
        return false;
    }
    if ($("#TB_KSRQ").val() > $("#TB_JSRQ").val()) {
        ShowMessage("开始日期不得大于结束日期！", 3);
        return false;
    }
    return true;
}

function SaveData() {

    var Obj = new Object();
    Obj.iDJLX = vDJLX;
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iGZID = $("#HF_GZID").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.dLJYXQ = $("#TB_YXQ").val();
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    var lstMD = new Array();
    lstMD = $("#listMD").datagrid("getRows");
    Obj.itemTable2 = lstMD;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}
function StartClick() {
    ShowYesNoMessage("启动本单执行将会终止同规则已启动的单据，是否继续？", function () {
        if (posttosever(SaveDataBase(), vUrl, "Start") == true) {
            vProcStatus = cPS_BROWSE;
            ShowDataBase(vJLBH);
            SetControlBaseState();
        }
    });
};

function InsertClickCustom() {
    window.setTimeout(function () {
        $('#list').datagrid('loadData', { total: 0, rows: [] });
        $('#listMD').datagrid('loadData', { total: 0, rows: [] });
    }, 100);
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_GZID").val(Obj.iGZID);
    $("#TB_GZMC").val(Obj.sGZMC);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#TB_YXQ").val(Obj.dLJYXQ);


    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $('#listMD').datagrid('loadData', Obj.itemTable2, "json");
    $('#listMD').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZSJ);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)


}

function getDate(date) {
    var currentDate = date.getFullYear() + "-";
    if (date.getMonth() < 9) {
        currentDate += "0" + (date.getMonth() + 1) + "-";
    }
    else {
        currentDate += date.getMonth() + 1 + "-";
    }
    if (date.getDate() < 10) {
        currentDate += "0" + (date.getDate());
    }
    else {
        currentDate += date.getDate();
    }
    return currentDate;

};