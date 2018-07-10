vUrl = "../../HYKGL/HYKGL.ashx";
vCaption = "顾客信息";
var vDialogName = "ListGKXX";
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '顾客ID', width: 100 },
        { field: 'sHY_NAME', title: '顾客名称', width: 100 },
    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
   
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_GKID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GKMC", "sHY_NAME", "like", true);
    return arrayObj;
};