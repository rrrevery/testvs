vUrl = "../HYKGL.ashx";
var ListName = "";
colmodels = [
     { name: "iSJLX", hidden: true, },
     { name: "sSJMC", width: 150, },
     { name: "iSJNR", width: 150, hidden: true, },
     { name: "sSJDM", width: 150, },
];
function InitGrid() {
    vColumnNames = ['SJLX', "商品名称", "商品ID", "商品代码"];
    vColumnModel = colmodels;

    vSPFLColumnNames = ['SJLX', "商品分类名称", "商品分类ID", "商品分类代码"];
    vSPFLColumnModel = colmodels;

    vPLColumnNames = ['SJLX', "品牌名称", "品牌ID", "品牌代码"];
    vPLColumnModel = colmodels;


};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("h2").show();
    $("#ZXR").hide();
    $("#ZXRQ").hide();

    DrawGrid("SPFLList", vSPFLColumnNames, vSPFLColumnModel);
    DrawGrid("PLList", vPLColumnNames, vPLColumnModel);


    $("#TB_HYBQ").click(function () {
        var condData = new Object();
        condData["iLABELLX"] = 0;
        SelectHYBQ("TB_HYBQ", "HF_HYBQ", "zHF_HYBQ", false, condData);
        ListName = ""
    });
    $("#TB_XHYBQ").click(function () {
        var condData = new Object();
        condData["iLABELLX"] = 0;
        SelectHYBQ("TB_XHYBQ", "HF_XHYBQ", "zHF_XHYBQ", false, condData);
        ListName = ""
    });

    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", false);
        ListName = ""
    });

    $("#TB_SPFLMC").click(function () {
        if ($("#HF_SHDM").val() == "") {
            ShowMessage("请先选择商户", 3);
            return;
        }
        var condData = new Object();
        condData["sSHDM"] = $("#HF_SHDM").val();
        SelectSHSPFL("TB_SPFLMC", "HF_SPFLDM", "zHF_SPFLDM", true, condData);
        ListName = ""
    });

    $("#Add_SPXX").click(function () {
        //var dialogUrl = "../../CrmArt/ListSHSP/CrmArt_ListSHSP.aspx";
        //OpenDialog(dialogUrl, listName, DataObject, 'ListSHSP', CheckFieldId);
        var condData = new Object();
        if ($("#HF_SPFLDM").val() != "")
            condData["sSPFLDM"] = $("#HF_SPFLDM").val();
        SelectSHSP("", "", "", false, condData);
        ListName = "list";
    });
    $("#Del_SPXX").click(function () {
        DeleteRows("list");
    });

    $("#Add_SPFL").click(function () {
        if ($("#HF_SHDM").val() == "") {
            ShowMessage("请先选择商户", 3);
            return;
        }
        else {
            var condData = new Object();
            condData["sSHDM"] = $("#HF_SHDM").val();
            SelectSHSPFL("", "", "", false, condData);
            ListName = "SPFLList";
        }
    });
    $("#Del_SPFL").click(function () {
        DeleteRows("SPFLList");
    });

    $("#Add_PL").click(function () {
        SelectSPSB("", "", "", false);
        ListName = "PLList";
    });
    $("#Del_PL").click(function () {
        DeleteRows("PLList");
    });
});


function InsertClickCustom() {
    $("#HF_SPFLDM").val("");
    window.setTimeout(function () {
        $('#list').datagrid('loadData', { total: 0, rows: [] });
        $('#SPFLList').datagrid('loadData', { total: 0, rows: [] });
        $('#PLList').datagrid('loadData', { total: 0, rows: [] });
    }, 100);
}


function IsValidData() {
    if ($("#HF_HYBQ").val() == "") {
        ShowMessage('请选择标签', 3);
        return false;
    }
    if ($("#HF_XHYBQ").val() != "" && $("#TB_YXYF").val() == "") {
        ShowMessage('请输入有效月份', 3);
        return false;
    }

    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = $("#HF_HYBQ").val();
    Obj.iLABELID = $("#HF_HYBQ").val();
    if ($("#TB_YXYF").val() != "") {
        Obj.iYXYF = $("#TB_YXYF").val();
    }
    if ($("#HF_XHYBQ").val() != "") {
        Obj.iNEW_LABELID = $("#HF_XHYBQ").val();
    }
    Obj.iSTATUS = $("#CB_TY").prop("checked") ? "-1" : "0";
    var lst = new Array();
    var SPXXList = $("#list").datagrid("getRows");
    var SPFLList = $("#SPFLList").datagrid("getRows");
    var PLList = $("#PLList").datagrid("getRows");
    lst = lst.concat(SPXXList, SPFLList, PLList);

    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_HYBQ").val(Obj.iLABELID);
    $("#TB_HYBQ").val(Obj.sLABELVALUE)
    $("#TB_YXYF").val(Obj.iYXYF)
    $("#HF_XHYBQ").val(parseInt(Obj.iNEW_LABELID) == 0 ? "" : Obj.iNEW_LABELID);
    $("#TB_XHYBQ").val(Obj.sNEW_LABELVALUE);
    $("#CB_TY").prop("checked", Obj.iSTATUS == "-1");
    $("#list").datagrid("loadData", { rows: [], total: 0 });
    $("#SPFLList").datagrid("loadData", { rows: [], total: 0 });
    $("#PLList").datagrid("loadData", { rows: [], total: 0 });
    for (var i = 0; i < Obj.itemTable.length ; i++) {
        if (parseInt(Obj.itemTable[i].iSJLX) == 0) {
            $("#list").datagrid("appendRow", Obj.itemTable[i]);
        }
        else if (parseInt(Obj.itemTable[i].iSJLX) == 1) {
            $("#SPFLList").datagrid("appendRow", Obj.itemTable[i]);
        }
        else {
            $("#PLList").datagrid("appendRow", Obj.itemTable[i]);
        }
    }
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
}

function MoseDialogCustomerReturn(dialogName, lst, showField) {
    var sjlx;
    switch (ListName) {
        case "list": sjlx = 0; break;
        case "SPFLList": sjlx = 1; break;
        case "PLList": sjlx = 2; break;
    }
    if (ListName == "list") {
        for (var i = 0; i <= lst.length - 1; i++) {
            if (CheckRepeat(ListName, lst[i].Id)) {
                $("#" + ListName).datagrid("appendRow", {
                    iSJLX: sjlx,
                    sSJMC: lst[i].sSPMC,
                    sSJDM: lst[i].sSPDM,
                    iSJNR: lst[i].iSHSPID,
                });
            }
        }
    }
    if (ListName == "PLList") {
        for (var i = 0; i <= lst.length - 1; i++) {
            if (CheckRepeat(ListName, lst[i].Id)) {
                $("#" + ListName).datagrid("appendRow", {
                    iSJLX: sjlx,
                    sSJMC: lst[i].sSBMC,
                    sSJDM: lst[i].sSBDM,
                    iSJNR: lst[i].iSHSBID,
                });
            }
        }
    }
    if (ListName == "SPFLList") {
        for (var i = 0; i <= lst.length - 1; i++) {
            if (CheckRepeat(ListName, lst[i].actid)) {
                $("#" + ListName).datagrid("appendRow", {
                    iSJLX: sjlx,
                    sSJMC: lst[i].shortName,
                    sSJDM: lst[i].id,
                    iSJNR: lst[i].actid,
                });
            }
        }
    }
}

function IsValidInputData() {
    try {
        if (typeof (eval("InitGrid")) == "function") {
            var rows = $("#list").datagrid("getData").rows;
            var SPFLrows = $("#SPFLList").datagrid("getData").rows;
            var PLrows = $("#PLList").datagrid("getData").rows;
            if (bNeedItemData && (rows.length <= 0 && SPFLrows.length <= 0 && PLrows.length <= 0)) {
                ShowMessage("子表没有数据，请添加!", 3);
                return false;
            }
            for (var i = 0; i < rows.length; i++) {
                $("#list").datagrid("endEdit", i);
            }
            if ($(".datagrid-editable-input").length > 0) {
                ShowMessage("子表数据正在编辑中，请保存!", 3);
                return false;
            }

        }

    }
    catch (ex) { };
    if (!IsValidData())
        return false;
    return true;
}



function CheckRepeat(tablename, value) {
    var boolInsert = true;
    var rowData = $("#" + tablename + "").datagrid("getRows");
    for (var i = 0; i < rowData.length; i++) {
        if (parseInt(rowData[i].sSJNR) == parseInt(value)) {
            boolInsert = false;
        }
    }
    return boolInsert;
}

