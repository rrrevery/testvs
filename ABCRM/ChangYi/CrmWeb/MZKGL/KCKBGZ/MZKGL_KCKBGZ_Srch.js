//vPageMsgID = CM_HYKGL_HYKBGZBB;
vUrl = "../MZKGL.ashx";
vCZK = GetUrlParam("czk");
var vOLDDB = GetUrlParam("old");

function InitGrid() {
    vColumnNames = ["日期", "卡类型", "保管地点", "卡类型", "BGDDDM", "面值金额", "期初数量", "期初金额", "建卡数量", "建卡金额", "写卡数量", "写卡金额", "拨入数量", "拨入金额", "拨出数量", "拨出金额", "换卡数量", "换卡金额", "调整数量", "调整金额", "发售数量", "发售金额", "作废数量", "作废金额", "结存数量", "结存金额", "年月", ],
    vColumnModel = [
        { name: 'dRQ', width: 100 },
        { name: 'iHYKTYPE', width: 80, hidden: true, },
        { name: 'sBGDDMC', width: 80 },
        { name: 'sHYKNAME', width: 100 },
        { name: 'sBGDDDM', hidden: true, },
        { name: 'fMZJE', width: 100 },
        { name: 'iQCSL', width: 80 },
        { name: 'fQCJE', width: 100 },
        { name: 'iJKSL', width: 80 },
        { name: 'fJKJE', width: 100 },
        { name: 'iXKSL', width: 80 },
        { name: 'fXKJE', width: 100 },
        { name: 'iBRSL', width: 80 },
        { name: 'fBRJE', width: 100 },
        { name: 'fBCSL', width: 80 },
        { name: 'iBCJE', width: 100 },
        { name: 'iHKSL', width: 80 },
        { name: 'fHKJE', width: 100 },
        { name: 'iTZSL', width: 80 },
        { name: 'fTZJE', width: 100 },
        { name: 'iFSSL', width: 80 },
        { name: 'fFSJE', width: 100 },
        { name: 'iZFSL', width: 80 },
        { name: 'fZFJE', width: 100 },
        { name: 'iJCSL', width: 80 },
        { name: 'fJCJE', width: 100 },
        { name: 'iYEARMONTH', },
    ];
}

//$(document).ready(function () {
//    $("#list").jqGrid("setGridParam", {
//        ondblClickRow: function (rowid) {
//            var rowData = $("#list").getRowData(rowid);
//            MakeNewTab("CrmWeb/HYKGL/HYKLQ/HYKGL_HYKLQ.aspx?jlbh=" + rowData.iJLBH + "&djlx=" + vDJLX, vCaption, vPageMsgID);
//        },
//    });
//});

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    BFButtonClick("TB_BGDDDM", function () {
        SelectBGDD("TB_BGDDDM", "HF_BGDDDM", "zHF_BGDDDM", false);
    });
    BFButtonClick("TB_HYKNAME", function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });
    RefreshButtonSep();
});





function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_SJ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_SJ2", "dRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};