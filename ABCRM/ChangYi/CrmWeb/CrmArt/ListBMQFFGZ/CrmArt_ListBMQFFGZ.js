vUrl = "../../GTPT/GTPT.ashx";
vCaption = "编码券发放规则名称";
var vDialogName = "ListWXBMQFFGZ";

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '编码券发放规则ID', hidden: true },
        { field: 'sGZMC', title: '编码券发放规则名称', width: 100 },

    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_BMQFFGZID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sGZMC", "like", true);
    return arrayObj;
};