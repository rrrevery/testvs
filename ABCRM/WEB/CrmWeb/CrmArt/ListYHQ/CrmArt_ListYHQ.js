vUrl = "../../CRMGL/CRMGL.ashx";
vCaption = "优惠券信息";

var vDialogName = "ListYHQ";
var condData = $.dialog.data("DialogCondition");
if (condData) {
    condData = JSON.parse(condData);
}
function InitGrid() {
    vColumns = [
        { field: 'iYHQID', title: '优惠券ID', width: 100 },
        { field: 'sYHQMC', title: '优惠券名称', width: 100 },
    ];
    vIdField = "iYHQID";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_YHQID", "iYHQID", "=", false);
    MakeSrchCondition(arrayObj, "TB_YHQMC", "sYHQMC", "like", true);
    MakeSrchCondition2(arrayObj, condData.iBJCODED, "iBJCODED", "=", false);
    MakeSrchCondition2(arrayObj, condData.iBJ_TS, "iBJ_TS", "=", false);
    return arrayObj;
};