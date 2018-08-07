vUrl = "../../GTPT/GTPT.ashx";
vCaption = "礼包";
var vDialogName = "ListLB";

function InitGrid() {
    vColumns = [
        { field: 'iLBID', title: '礼包ID', hidden: true },
        { field: 'sLBMC', title: '礼包名称', width: 100 },

    ];
    vIdField = "iLBID";
}

$(document).ready(function () {
    //pass
});
function AddCustomerCondition(Obj) {
    Obj.iBJ_WZJ = "999";
}
function MakeSearchCondition() {
    var arrayObj = new Array();
    //MakeSrchCondition2(arrayObj, tp_wzj, "iBJ_WZJ", "=", true);

    MakeSrchCondition(arrayObj, "TB_LBID", "iJLBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_LBMC", "sLBMC", "like", true);
    return arrayObj;
};