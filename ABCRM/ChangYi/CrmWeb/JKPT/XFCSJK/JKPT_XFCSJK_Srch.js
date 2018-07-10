vUrl = "../JKPT.ashx";
vCaption = "消费次数监控";
var iSEARCHMODE = GetUrlParam("DZLX");
var rq = GetUrlParam("rq");
var mdid = GetUrlParam("mdid");
var max_xfcs = GetUrlParam("max_xfcs");
var bmdm = GetUrlParam("bmdm");
var sktno = GetUrlParam("sktno");
var hyk_no;
var vXFMXColumnNames;
var vXFMXColumnModel
function InitGrid() {
    vColumnNames = ['会员ID', '会员卡号', '姓名', 'MDID', '门店名称', '会员卡类型', '消费次数', '退货次数', '消费金额', '积分', '最大金额', '最小金额'];
    vColumnModel = [
             { name: 'iHYID', width: 80, hidden: true },
             { name: 'sHYK_NO', width: 100, },
             { name: 'sHY_NAME', width: 100, },
             { name: 'iMDID', width: 80, hidden: true, },
             { name: 'sMDMC', width: 100, },
             { name: 'sHYKNAME', width: 100, },
             { name: 'iXFCS', width: 100, },
             { name: 'iTHCS', width: 100, },
             { name: 'fXFJE', width: 100, },
             { name: 'fJF', width: 100, },
             { name: 'fZDJE', width: 100, hidden: (iSEARCHMODE == 2 || iSEARCHMODE == 3) ? true : false },
             { name: 'fZXJE', width: 100, hidden: (iSEARCHMODE == 2 || iSEARCHMODE == 3) ? true : false },
    ];
    vXFMXColumnNames = ['门店名称', '收款台', '小票号', '消费时间', '消费金额', '折扣金额', '积分', '积分倍数', '收款员代码', '补刷卡标记', '消费记录编号', '卡号', ];
    vXFMXColumnModel = [
            { name: 'sMDMC', width: 200, },
            { name: 'sSKTNO', width: 80, },
            { name: 'iXPH', width: 80, },
            { name: 'dXFSJ', width: 165, },
            { name: 'fJE', width: 80, },
            { name: 'fZK', width: 80, },
            { name: 'fJF', width: 80, },
            { name: 'fJFBS', width: 80, },
            { name: 'sSKYDM', hidden: true, },
            { name: 'iBJ_HTBSK', width: 100, },
            { name: 'iXFJLID', width: 80, },
            { name: 'sHYKNO', width: 100, },
    ];

}

$(document).ready(function () {
    DrawGrid("list_XF", vXFMXColumnNames, vXFMXColumnModel);

    $("#B_Search").hide();
    if (iSEARCHMODE)
    {
        window.setTimeout("SearchClick()", 500);
    }
    //$("#B_Search").trigger("click");  //执行报错
    

    //$("#list").jqGrid("setGridParam", {
    //    ondblClickRow: function (rowid) {
    //        var rowData = $("#list").getRowData(rowid);
    //        hyk_no = rowData.sHYK_NO;
    //        //MakeNewTab("CrmWeb/KFPT/DYHYFX/KFPT_DYHYFX.aspx?hykno=" + rowData.sHYK_NO, "单一会员分析", vPageMsgID_DYHYFX);
    //    },
    //    onCellSelect: function (rowid) {
    //        var rowData = $("#list").getRowData(rowid);
    //        hyk_no = rowData.sHYK_NO;
    //        init_xf();
    //    },
    //});

    var mydate = new Date();
    var today = mydate.getFullYear() + "-" + Appendzero(mydate.getMonth() + 1) + "-" + Appendzero(mydate.getDate());
    var lastdate = new Date(mydate.getTime() - 30 * 24 * 60 * 60 * 1000);
    var lasttoday = lastdate.getFullYear() + "-" + Appendzero(lastdate.getMonth() + 1) + "-" + Appendzero(lastdate.getDate());
    $("#TB_XFRQ1").val(lasttoday);
    $("#TB_XFRQ2").val(today);

    //$("#list_xf").jqGrid({
    //    async: false,
    //    //        url: Url + "?mode=Search&SearchMode=2&func=" + vPageMsgID_DYHYFX,
    //    datatype: "json",
    //    //        postData: { 'afterFirst': JSON.stringify(arrayobj) },
    //    colNames: ['门店名称', '收款台', '小票号', '消费时间', '消费金额', '折扣金额', '积分', '积分倍数', '收款员代码', '补刷卡标记', '消费记录编号', '卡号', ],
    //    colModel: [
    //        { name: 'sMDMC', width: 200, },
    //        { name: 'sSKTNO', width: 80, },
    //        { name: 'iXPH', width: 80, },
    //        { name: 'dXFSJ', width: 165, },
    //        { name: 'fJE', width: 80, },
    //        { name: 'fZK', width: 80, },
    //        { name: 'fJF', width: 80, },
    //        { name: 'fJFBS', width: 80, },
    //        { name: 'sSKYDM', hidden: true, },
    //        { name: 'iBJ_HTBSK', width: 100, },
    //        { name: 'iXFJLID', width: 80, },
    //        { name: 'sHYKNO', width: 100, },
    //    ],
    //    sortable: true,
    //    sortorder: "asc",
    //    sortname: 'sMDMC',
    //    shrinkToFit: false,
    //    autoScroll: false,
    //    rownumbers: true,
    //    altRows: true,
    //    width: 800,
    //    height: 200,
    //    rowNum: 15,
    //    pager: '#pager_xf',
    //    viewrecords: true,
    //});
})


function Appendzero(obj) {
    if (obj < 10) return "0" + obj; else return obj;
}


function init_xf() {

    var arrayObj = new Array();
    MakeSrchCondition2(arrayObj, hyk_no, "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_XFRQ1", "dXFRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_XFRQ2", "dXFRQ", "<=", true);

    Url = "../../KFPT/KFPT.ashx";
    SearchGridReload("list_xf", 2, arrayObj);
}


function IsValidSearch() {
    return true;
}



function AddCustomerCondition(Obj) {
    Obj.iSEARCHMODE = iSEARCHMODE;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition2(arrayObj, rq, "dRQ", "=", (iSEARCHMODE == 0 || iSEARCHMODE == 2 || iSEARCHMODE == 4) ? true : false);
    MakeSrchCondition2(arrayObj, mdid, "iMDID", "=", false);
    MakeSrchCondition2(arrayObj, max_xfcs, "iXFCS", "=", false);
    if (iSEARCHMODE == 2 || iSEARCHMODE == 3)
        MakeSrchCondition2(arrayObj, bmdm, "sBMDM", "=", true);
    if (iSEARCHMODE == 4 || iSEARCHMODE == 5)
        MakeSrchCondition2(arrayObj, sktno, "sSKTNO", "=", true);

    return arrayObj;
};

//function DoSearch() {


//    //var zfklx = $("[name='RD_ZFKLX']:checked").val();
//    //if (zfklx != 0) {
//    //    MakeSrchCondition2(arrayObj, zfklx, "iFXDW", "=", false);
//    //}
//    SearchGridReload("list_xf", 2, arrayObj);
//}

Url = "../../KFPT/KFPT.ashx";
function SearchGridReload(listName, SearchMode, arrayobj) {
    $("#" + listName + "").jqGrid('setGridParam', {
        url: Url + "?mode=Search&SearchMode=" + SearchMode + "&func=" + vPageMsgID_DYHYFX,
        postData: { 'afterFirst': JSON.stringify(arrayobj) },
        page: 1
    }).trigger("reloadGrid");
}