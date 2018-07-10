vUrl = "../../GTPT/GTPT.ashx";
vCaption = "积分兑换礼品规则";
var vDialogName = "ListWXJFDHLPGZ";

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '积分兑换礼品规则ID', hidden: true },
        { field: 'sGZMC', title: '积分兑换礼品规则名称', width: 100 },

    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_GZID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sGZMC", "like", true);
    return arrayObj;
};