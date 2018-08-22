vUrl = "../HYKGL.ashx";
vCaption = "优惠券账户明细";
var vZHCLMXColumnNames;
var vZHCLMXColumnModel;
var iSEARCHMODE = 0;
var iHYID = 0;
var iYHQID = 0;
var dJSRQ = "";
var iCXID = 0;
var sMDFWDM = "";
var HYKNO = GetUrlParam("HYKNO");



$(document).ready(function () {
    if (HYKNO) {
        $("#TB_HYK_NO").val(HYKNO);
        window.setTimeout(function ()
        { SearchData(); }, 100);
    }
    SetControlStatus();
    DrawGrid("list_ZHCLMX", vZHCLMXColumnNames, vZHCLMXColumnModel);
    $("#SearchResult .maininput").not("#ZHMX_Hidden").css("display", "none");
    RefreshButtonSep();
});
function InitGrid() {
    vColumnNames = ["优惠券名称", 'HYK_NO', "HYID", "YHQID", "CXID", "促销活动", "结束日期", "总金额", '门店范围代码', '交易冻结金额', ];
    vColumnModel = [
             { name: 'sYHQMC', width: 100 },
             { name: 'sHYK_NO', hidden: true },
             { name: 'iHYID', hidden: true },
             { name: 'iYHQID', hidden: true },
             { name: 'iCXID', hidden: true },
             { name: 'sCXZT', width: 150, },
             { name: 'dJSRQ', width: 80, },
             { name: 'fJE', width: 70 },
             { name: 'sMDFWDM', width: 70 },
             { name: 'fJYDJJE', width: 100, },
    ];
    vZHCLMXColumnNames = ['处理时间', 'MDID', 'HYID', '处理类型', '处理类型', '优惠券名称', '结束日期', '借方金额', 'YHQID', '贷方金额', '余额', '门店名称', '摘要', ];
    vZHCLMXColumnModel = [
             { name: 'dCLSJ', width: 150, },
             { name: 'iMDID', hidden: true },
             { name: 'iHYID', hidden: true },
             { name: 'sCLLXMC', width: 100, hidden: true },
             {
                 name: 'iCLLX',  formatter: function (cellvalue, options, rowObject) {
                     if (cellvalue == 1) { return "存款"; }
                     if (cellvalue == 2) { return "取款"; }
                 },
             },
             { name: 'sYHQMC', width: 100, },
             { name: 'dJSRQ', width: 70, },
             { name: 'fJFJE', width: 70, },
             { name: 'iYHQID', hidden: true },
             { name: 'fDFJE', width: 70, },
             { name: 'fYE', width: 70, },
             { name: 'sMDMC', width: 100, },
             { name: 'sZY', width: 100, },
    ];
}
function ToggleHiddenPanelCustomer(elementCurrent) {
    if ($("#" + elementCurrent + "").parent()[0].id = "SearchResult") {
        $("#SearchResult .maininput").not("#" + elementCurrent + "_Hidden").slideUp();
        switch (elementCurrent) {
            case "ZHMX": iSEARCHMODE = 0;
                break;
            case "ZHCLMX": iSEARCHMODE = 1;
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
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    if (iSEARCHMODE == 0)
        MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    return arrayObj;
}

function AddCustomerCondition(Obj) {
    Obj.iSEARCHMODE = iSEARCHMODE;
    Obj.iHYID = iHYID;
    Obj.iYHQID = iYHQID;
    Obj.dJSRQ = dJSRQ;
    Obj.iCXID = iCXID;
    Obj.sMDFWDM = sMDFWDM
}

function DBClickRow() {
    ;
}

function GetYHQZHCLMX() {
    $('#list_ZHCLMX').datagrid('loadData', { total: 0, rows: [] });
    $("#SearchResult .maininput").not("#ZHCLMX_Hidden").slideUp();
    $("#ZHCLMX_Hidden").slideDown();
    iSEARCHMODE = 1
    SearchData(undefined, undefined, undefined, undefined, "list_ZHCLMX");

}

function OnClickRow(rowIndex, rowData) {
    if (this.id == "list") {
        iHYID = rowData.iHYID;
        iYHQID = rowData.iYHQID;
        dJSRQ = rowData.dJSRQ;
        iCXID = rowData.iCXID;
        sMDFWDM = rowData.sMDFWDM;
        GetYHQZHCLMX();
    }
}

function SearchClick() {
    if (!IsValidSearch())
        return;
    if (iSEARCHMODE == 0) {
        $("#SearchResult .maininput").not("#ZHMX_Hidden").slideUp();
        $("#ZHMX_Hidden").slideDown();
        SearchData();
    }
    if (iSEARCHMODE == 1) {
        $("#SearchResult .maininput").not("#ZHCLMX_Hidden").slideUp();
        $("#ZHCLMX_Hidden").slideDown();
        SearchData(undefined, undefined, undefined, undefined, "list_ZHCLMX");
    }

    SetControlBaseState();
};