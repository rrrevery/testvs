vUrl = "../HYKGL.ashx";
vCaption = "优惠券账户明细";
var vJFMXColumnNames;
var vJFMXColumnModel;
var iSEARCHMODE = 0;
var iHYID = 0;
var iMDID = 0;
var iXFJLID = 0;
var sMDFWDM = "";
var vSPMXColumnNames;
var vSPMXColumnModel;
var HYKNO = GetUrlParam("HYKNO");

function InitGrid() {
    vColumnNames = ["门店名称", 'HYK_NO', "HYID", 'MDID', "未处理积分", "消费金额", "折扣金额", "本期积分", "累计积分", '累计消费金额', '累计折扣金额', '本年积分'];
    vColumnModel = [
            { name: 'sMDMC', width: 100 },
             { name: 'sHYK_NO', hidden: true },
             { name: 'iHYID', hidden: true },
            { name: 'iMDID', hidden: true },
             { name: 'fWCLJF', hidden: true },
             { name: 'fXFJE', width: 70, },
            { name: 'fZKJE', width: 70, },
            { name: 'fBQJF', width: 70 },
            { name: 'fLJJF', width: 70 },
            { name: 'fLJXFJE', width: 70, },
            { name: 'fLJZKJE', width: 70 },
            { name: 'fBNLJJF', width: 70, },
    ];
    vJFMXColumnNames = ['iXFJLID', '消费时间', '门店名称', '款台号', 'MDID', 'HYID', '小票号', '消费金额', '积分', '特定倍数积分', '收款员代码', '陪购员'];
    vJFMXColumnModel = [
               { name: 'iXFJLID', hidden: true },
             { name: 'dXFSJ', width: 130, },
             { name: 'sMDMC', width: 100, },
             { name: 'sSKTNO', width: 70, },
             { name: 'iMDID', hidden: true },
              { name: 'iHYID', hidden: true },
             { name: 'iJLBH', width: 70, },
             { name: 'fJE', width: 70, },
             { name: 'fJF', width: 70, },
             { name: 'fJFBS', width: 100, },
             { name: 'sSKYDM', hidden: true },
             { name: 'iPGRYID', width: 70, },
    ];
    vSPMXColumnNames = ['iXFJLID', '部门代码', '消费部门', '商品代码', '商品名称', 'HYID', 'MDID', '销售数量', '销售金额', '折扣金额', '会员折扣', '积分', ];
    vSPMXColumnModel = [
              { name: 'iXFJLID', hidden: true },
             { name: 'sBMDM', width: 70, },
             { name: 'sBMMC', width: 100, },
             { name: 'sSPDM', width: 70, },
              { name: 'sSPMC', width: 120, },
             { name: 'iMDID', hidden: true },
              { name: 'iHYID', hidden: true },
             { name: 'fXSSL', width: 70, },
             { name: 'fXSJE', width: 70, },
             { name: 'fZKJF', width: 70, },
             { name: 'fZKJE_HY', width: 70, },
             { name: 'fJF', hidden: true },

    ];
}

$(document).ready(function () {
    if (HYKNO) {
        $("#TB_HYK_NO").val(HYKNO);
        window.setTimeout(function ()
        { SearchData(); }, 100);
    }
    SetControlStatus();
    DrawGrid("list_JFMX", vJFMXColumnNames, vJFMXColumnModel);
    DrawGrid("list_SPMX", vSPMXColumnNames, vSPMXColumnModel);
    $("#SearchResult .maininput").not("#MDMX_Hidden").css("display", "none");

    RefreshButtonSep();
});

function ToggleHiddenPanelCustomer(elementCurrent) {
    if ($("#" + elementCurrent + "").parent()[0].id = "SearchResult") {
        $("#SearchResult .maininput").not("#" + elementCurrent + "_Hidden").slideUp();      
        switch (elementCurrent) {
            case "MDMX": iSEARCHMODE = 0;
                break;
            case "JFMX": iSEARCHMODE = 1;
                break;
            case "SPMX": iSEARCHMODE = 2;
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
    if (iSEARCHMODE == 1)
    {
        MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
        MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    }   
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    return arrayObj;
}

function AddCustomerCondition(Obj) {
    Obj.iSEARCHMODE = iSEARCHMODE;
    Obj.iHYID = iHYID;
    Obj.iMDID = iMDID;
    Obj.iXFJLID = iXFJLID;
}

function DBClickRow() {
    ;
}

function GetJFXFMX() {
    $('#list_JFMX').datagrid('loadData', { total: 0, rows: [] });
    $('#list_SPMX').datagrid('loadData', { total: 0, rows: [] });
    $("#SearchResult .maininput").not("#JFMX_Hidden").slideUp();
    $("#JFMX_Hidden").slideDown();
    SearchData(undefined, undefined, undefined, undefined, "list_JFMX");

}

function GetSPXFMX() {
    $('#list_SPMX').datagrid('loadData', { total: 0, rows: [] });
    $("#SearchResult .maininput").not("#SPMX_Hidden").slideUp();
    $("#SPMX_Hidden").slideDown();
    SearchData(undefined, undefined, undefined, undefined, "list_SPMX");

}

function OnClickRow(rowIndex, rowData) {
    var id = this.id;
    switch (id) {
        case "list":
            iSEARCHMODE = 1;
            iHYID = rowData.iHYID;
            iMDID = rowData.iMDID;
            GetJFXFMX();
            break;
        case "list_JFMX":
            iSEARCHMODE = 2;
            iHYID = rowData.iHYID;
            iMDID = rowData.iMDID;
            iXFJLID = rowData.iXFJLID;
            GetSPXFMX();
            break;
        default:
            iSEARCHMODE = 0;
    }
}


function SearchClick() {
    if (!IsValidSearch())
        return;
    $("#SearchResult .maininput").not("#MDMX_Hidden").slideUp();
    $("#MDMX_Hidden").slideDown();
    iSEARCHMODE = 0;
    SearchData();
    SetControlBaseState();
};