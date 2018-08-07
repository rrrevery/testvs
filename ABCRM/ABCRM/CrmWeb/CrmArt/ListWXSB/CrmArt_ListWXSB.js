vUrl = "../../GTPT/GTPT.ashx";
vCaption = "微信品牌商标";
var vDialogName = "ListWXSB";

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '商标ID', hidden: true },
        { field: 'sSBMC', title: '商标名称', width: 100 },

    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_SBID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_SBMC", "sSBMC", "like", true);
    return arrayObj;
};