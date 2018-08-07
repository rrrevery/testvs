vUrl = "../../GTPT/GTPT.ashx";
vCaption = "城市定义";
var vDialogName = "ListCS";

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '城市ID', hidden: true },
        { field: 'sCSMC', title: '城市名称', width: 100 },

    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_CSID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_CSMC", "sCSMC", "like", true);
    return arrayObj;
};