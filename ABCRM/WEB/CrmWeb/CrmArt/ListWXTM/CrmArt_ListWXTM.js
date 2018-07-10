vUrl = "../../GTPT/GTPT.ashx";
vCaption = "微题库信息";
var vDialogName = "ListTM";

function InitGrid() {
    vColumns = [
        { field: 'iID', title: '微题库ID', hidden: true },
        { field: 'sMC', title: '微题库名称', width: 100 },

    ];
    vIdField = "iID";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_ID", "iJLBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_MC", "sMC", "like", true);
    return arrayObj;
};