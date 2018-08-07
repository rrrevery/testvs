vUrl = "../../KFPT/KFPT.ashx";
vCaption = "客服经理信息";
var vDialogName = "ListKFJL";
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");

function InitGrid() {
    vColumns = [
        { field: 'iKFRYID', title: '人员ID', hidden: true },
        { field: 'sRYMC', title: '人员名称', width: 100 },
        { field: 'sRYDM', title: '人员代码', width: 100 },
        { field: 'sPYM', title: '拼音码', width: 100 },
    ];
    vIdField = "iKFRYID";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_RYDM", "sRYDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_RYMC", "sRYMC", "like", true);
    return arrayObj;
};