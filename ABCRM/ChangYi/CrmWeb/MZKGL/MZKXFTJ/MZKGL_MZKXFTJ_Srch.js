vUrl = "../MZKGL.ashx";

var vMDSKTXFColumnNames;
var vMDSKTXFColumnModel;
var iSEARCHMODE = 0;
var iHYID = 0;
var iMDID = 0;
var sSKTNO = "";
var sMDFWDM = "";
var vMDSKTXFMXColumnNames;
var vMDSKTXFMXColumnModel;
var HYKNO = GetUrlParam("HYKNO");

function InitGrid() {
    vColumnNames = ["门店名称", "卡类型", "消费金额", "退卡金额", "调整金额", "MDID", "HYKTYPE", ];
    vColumnModel = [
                { name: 'sMDMC', width: 100 },
                { name: 'sHYKNAME', width: 100 },
                { name: 'fXFJE', width: 90 },
                { name: 'fTKJE', width: 90 },
                { name: 'fTZJE', width: 90 },
                { name: 'iMDID', width: 90, hidden: true },//sortable默认为true width默认150
                { name: 'iHYKTYPE', width: 70, hidden: true },
    ];
    vMDSKTXFColumnNames = ['门店名称', "MDID", "收款台编号", "卡类型", "消费金额", "退卡金额", "调整金额", "MDID", ];
    vMDSKTXFColumnModel = [
               { name: 'sMDMC', width: 100, },
            { name: 'iMDID', hidden: true },
             { name: 'sSKTNO', width: 70 },
            { name: 'sHYKNAME', width: 100 },
            { name: 'fXFJE', width: 80 },
            { name: 'fTKJE', width: 80, },//sortable默认为true width默认150
            { name: 'fTZJE', width: 80 },
            { name: 'iMDID', width: 70, hidden: true },
    ];
    vMDSKTXFMXColumnNames =  ['门店名称', "收款台", "会员卡号", "小票号", "处理时间", "借方金额", "贷方金额", "余额", "摘要", "交易编号", ];
    vMDSKTXFMXColumnModel = [
            { name: 'sMDMC', width: 90, },
            { name: 'sSKTNO', width: 70 },
            { name: 'sHYK_NO', width: 70 },
            { name: 'iJLBH', width: 70, },//sortable默认为true width默认150            
            { name: 'dXFSJ', width: 150 },
            { name: 'fJFJE', width:70 },
            { name: 'fDFJE', width: 70 },
            { name: 'fYE', width: 70 },
            { name: 'sZY', width: 70 },
            { name: 'iJYBH', width: 70 },

    ];
}

$(document).ready(function () {
    BFButtonClick("TB_HYKNAME", function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });
    SetControlStatus();
    DrawGrid("list_MDSKTXF", vMDSKTXFColumnNames, vMDSKTXFColumnModel);
    DrawGrid("list_MDSKTXFMX", vMDSKTXFMXColumnNames, vMDSKTXFMXColumnModel);
    $("#SearchResult .maininput").not("#MDXF_Hidden").css("display", "none");

    RefreshButtonSep();
});

function ToggleHiddenPanelCustomer(elementCurrent) {
    if ($("#" + elementCurrent + "").parent()[0].id = "SearchResult") {
        $("#SearchResult .maininput").not("#" + elementCurrent + "_Hidden").slideUp();
        switch (elementCurrent) {
            case "MDXF": iSEARCHMODE = 0;
                break;
            case "MDSKTXF": iSEARCHMODE = 1;
                break;
            case "MDSKTXFMX": iSEARCHMODE = 2;
                break;
        }
    }
}

function SetControlStatus() {
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    if (iSEARCHMODE == 1 || iSEARCHMODE == 0) {
        MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    }
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dRQ", "<=", true);
    return arrayObj;
}

function AddCustomerCondition(Obj) {
    Obj.iSEARCHMODE = iSEARCHMODE;
    Obj.sSKTNO = sSKTNO;
    Obj.iMDID = iMDID;
}

function DBClickRow() {
    ;
}

function GetJFXFMX() {
    $('#list_MDSKTXF').datagrid('loadData', { total: 0, rows: [] });
    $('#list_MDSKTXFMX').datagrid('loadData', { total: 0, rows: [] });
    $("#SearchResult .maininput").not("#MDSKTXF_Hidden").slideUp();
    $("#MDSKTXF_Hidden").slideDown();
    SearchData(undefined, undefined, undefined, undefined, "list_MDSKTXF");

}

function GetSPXFMX() {
    $('#list_MDSKTXFMX').datagrid('loadData', { total: 0, rows: [] });
    $("#SearchResult .maininput").not("#MDSKTXFMX_Hidden").slideUp();
    $("#MDSKTXFMX_Hidden").slideDown();
    SearchData(undefined, undefined, undefined, undefined, "list_MDSKTXFMX");

}

function OnClickRow(rowIndex, rowData) {
    var id = this.id;
    switch (id) {
        case "list":
            iSEARCHMODE = 1;
            iMDID = rowData.iMDID;
            GetJFXFMX();
            break;
        case "list_MDSKTXF":
            iSEARCHMODE = 2;
            sSKTNO = rowData.sSKTNO;
            GetSPXFMX();
            break;
        default:
            iSEARCHMODE = 0;
    }
}

