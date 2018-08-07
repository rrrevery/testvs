vUrl = "../../GTPT/GTPT.ashx";
vCaption = "微信编码券礼包";
var vDialogName = "ListBMQLB";

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '礼包ID', hidden: true },
        { field: 'sLBMC', title: '礼包名称', width: 100 },

    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_LBID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_LBMC", "sLBMC", "like", true);
    return arrayObj;
};