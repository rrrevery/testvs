vUrl = "../GTPT.ashx";
var vmdid, vsbid;
vCaption = "楼层商标定义";

function InitGrid() {
    vColumnNames = ['记录编号', '楼层名称', '门店名称', 'MDID', 'SBID', '商标名称'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },
            { name: 'sNAME', width: 120, },
            { name: 'sMDMC', width: 120, },
            { name: 'iMDID', hidden: true },
            { name: 'iSBID', hidden: true },
            { name: 'sSBMC', width: 120, },
    ];
}
$(document).ready(function () {

    $("#B_Update").hide();
    BFButtonClick("TB_SBMC", function () {
        SelectWXSBMC("TB_SBMC", "HF_SBID", "zHF_SBID", false);
    });
    BFButtonClick("TB_LCMC", function () {
        SelectWXLC("TB_LCMC", "HF_LCID", "zHF_LCID",false);
    });

    BFButtonClick("TB_MDMC", function () {
        SelectWXMD("TB_MDMC", "HF_MDID", "zHF_MDID",false);
    });
    RefreshButtonSep();
});

function DBClickRow(rowIndex, rowData) {
    if ($("#B_Insert #B_Update").css("display") != "none") {
        var sDbCurrentPath = "";
        sDbCurrentPath = sCurrentPath + "?jlbh=" + rowData.iJLBH;
        ConbinDataArry["mdid"] = rowData.iMDID;
        ConbinDataArry["sbid"] = rowData.iSBID;
        sDbCurrentPath = ConbinPath(sDbCurrentPath);
        MakeNewTab(sDbCurrentPath, vCaption, vPageMsgID);
    }
};
function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_SBID", "iSBID", "in", false);
    MakeSrchCondition(arrayObj, "HF_LCID", "iJLBH", "in", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}





