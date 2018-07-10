vUrl = "../HYKGL.ashx";
vCaption = "管辖商户积分消费明细";
var vSHMDJFColumnNames;
var vSHMDJFColumnModel;
var vMDXFMXColumnNames;
var vMDXFMXColumnModel;
var vSPXFMXColumnNames;
var vSPXFMXColumnModel;


var MOBILE_DCDYD = 0;
var iHYID = GetUrlParam("HYID");;
var iMDID = 0;
var iXFJLID = 0;
var sSHDM = "";

var HYKNO = GetUrlParam("HYKNO");
var iSEARCHMODE = 0;

function InitGrid() {
    vColumnNames = ["管辖商户名称", "SHDM", "可用积分", "消费金额", "折扣金额", '本期积分', "累计积分", "累计消费金额", "累计折扣金额", "本年累计积分", ];
    vColumnModel = [
            { name: 'sSHMC', width: 70 },
            { name: 'sSHDM', hidden: true },
            { name: 'fWCLJF', width: 70 },
            { name: 'fXFJE', width: 70 },
            { name: 'fZKJE', width: 70 },
            { name: 'fBQJF', width: 70, },//sortable默认为true width默认150
            { name: 'fLJJF', width: 70 },
            { name: 'fLJXFJE', width: 70 },
            { name: 'fLJZKJE', width: 70 },
            { name: 'fBNLJJF', width: 70 },
    ];
    vSHMDJFColumnNames = ['门店名称', "MDID", "可用积分", "消费金额", "折扣金额", "本期积分", '累计积分', "累计消费金额", "累计折扣金额", "本年累计积分", ];
    vSHMDJFColumnModel = [
            { name: 'sMDMC', width: 70, },
            { name: 'iMDID', hidden: true },
             { name: 'fWCLJF', width: 70 },
            { name: 'fXFJE', width: 70 },
            { name: 'fZKJE', width: 70 },
            { name: 'fBQJF', width: 70, },//sortable默认为true width默认150
            { name: 'fLJJF', width: 70 },
            { name: 'fLJXFJE', width: 70 },
            { name: 'fLJZKJE', width: 70 },
            { name: 'fBNLJJF', width: 70 },
    ];
    vMDXFMXColumnNames = ['消费时间', "门店名称", "分店号", "款台号", "小票号", '消费金额', "积分", "储值金额", "消费记录ID", "积分倍数", ];
    vMDXFMXColumnModel = [
            { name: 'dXFSJ', width: 70 },
            { name: 'sMDMC', width: 70, },
            { name: 'iFDBH', width: 70 },
            { name: 'sSKTNO', width: 70 },
            { name: 'iXPH', width: 70, },//sortable默认为true width默认150
            { name: 'fJE', width: 70 },
            { name: 'fJF', width: 70 },
            { name: 'fCZJE', width: 70 },
            { name: 'iXFJLID', width: 70 },
            { name: 'fJFBS', width: 70 },
    ];
    vSPXFMXColumnNames = ['商品代码', "商品名称", "销售数量", "销售金额", "折扣金额", '会员折扣金额', "积分", "积分定义单编号", "积分基数", "会员特定积分", ];
    vSPXFMXColumnModel = [
            { name: 'sSPDM', width: 70, },
            { name: 'sSPMC', width: 70, },
            { name: 'fXSSL', width: 70, },
            { name: 'fXSJE', width: 70, },
            { name: 'fZKJE', width: 70, },//sortable默认为true width默认150
            { name: 'fZKJE_HY', width: 70, },
            { name: 'fJF', width: 70 },
            { name: 'iJFDYDBH', width: 70 },
            { name: 'fJFJS', width: 70 },
            { name: 'iBJ_JFBS', width: 50, formatter: "checkbox" },
    ];
}

$(document).ready(function () {
    if (HYKNO) {
        $("#TB_HYK_NO").val(HYKNO);
        window.setTimeout(function ()
        { SearchData(); }, 500);
    }
    SetControlStatus();
    DrawGrid("list_SHMDJF", vSHMDJFColumnNames, vSHMDJFColumnModel);
    DrawGrid("list_MDXFMX", vMDXFMXColumnNames, vMDXFMXColumnModel);
    DrawGrid("list_SPXFMX", vSPXFMXColumnNames, vSPXFMXColumnModel);
    $("#SearchResult .maininput").not("#GXSHJF_Hidden").css("display", "none");

    RefreshButtonSep();
});

function ToggleHiddenPanelCustomer(elementCurrent) {
    if ($("#" + elementCurrent + "").parent()[0].id = "SearchResult") {
        $("#SearchResult .maininput").not("#" + elementCurrent + "_Hidden").slideUp();
        switch (elementCurrent) {
            case "GXSHJF": MOBILE_DCDYD = 0;
                break;
            case "SHMDJF": MOBILE_DCDYD = 1;
                break;
            case "MDXFMX": iSEARCHMODE = 2;
                break;
            case "SPXFMX": iSEARCHMODE = 3;
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
    if (iSEARCHMODE == 2)
    {
        MakeSrchCondition(arrayObj, "TB_JSRQ1", "dRQ", ">=", true);
        MakeSrchCondition(arrayObj, "TB_JSRQ2", "dRQ", "<=", true);
    }
    //MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    return arrayObj;
}

function AddCustomerCondition(Obj) {
    Obj.iSEARCHMODE = iSEARCHMODE;
    Obj.iHYID = iHYID;
    Obj.iMDID = iMDID;
    Obj.iXFJLID = iXFJLID;
    Obj.sSHDM = sSHDM;
}

function DBClickRow() {
    ;
}

function GetSHMDJF() {
    $('#list_SHMDJF').datagrid('loadData', { total: 0, rows: [] });
    $('#list_MDXFMX').datagrid('loadData', { total: 0, rows: [] });
    $('#list_SPXFMX').datagrid('loadData', { total: 0, rows: [] });
    $("#SearchResult .maininput").not("#SHMDJF_Hidden").slideUp();
    $("#SHMDJF_Hidden").slideDown();
    SearchData(undefined, undefined, undefined, undefined, "list_SHMDJF");

}

function GetMDXFMX() {
    $('#list_MDXFMX').datagrid('loadData', { total: 0, rows: [] });
    $('#list_SPXFMX').datagrid('loadData', { total: 0, rows: [] });
    $("#SearchResult .maininput").not("#MDXFMX_Hidden").slideUp();
    $("#MDXFMX_Hidden").slideDown();
    SearchData(undefined, undefined, undefined, undefined, "list_MDXFMX");

}

function GetSPXFMX() {
    $('#list_SPXFMX').datagrid('loadData', { total: 0, rows: [] });
    $("#SearchResult .maininput").not("#SPXFMX_Hidden").slideUp();
    $("#SPXFMX_Hidden").slideDown();
    SearchData(undefined, undefined, undefined, undefined, "list_SPXFMX");

}

function OnClickRow(rowIndex, rowData) {
    var id = this.id;
    switch (id) {
        case "list":
            iSEARCHMODE = 1;
            //iHYID = rowData.iHYID;
            sSHDM = rowData.sSHDM;
            GetSHMDJF();
            break;
        case "list_SHMDJF":
            iSEARCHMODE = 2;
            //iHYID = rowData.iHYID;
            iMDID = rowData.iMDID;
            GetMDXFMX();
            break;
        case "list_MDXFMX":
            iSEARCHMODE = 3;
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
    $("#SearchResult .maininput").not("#GXSHJF_Hidden").slideUp();
    $("#GXSHJF_Hidden").slideDown();
    iSEARCHMODE = 0;
    SearchData();
    SetControlBaseState();
};
